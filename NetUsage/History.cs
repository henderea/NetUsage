using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetUsage
{
    public class History : IEnumerable<HistoryItem>
    {
        private readonly List<HistoryItem> history;
        private readonly int span;
        public const int DEFAULT_SPAN = 120;

        public int Count
        {
            get
            {
                return history.Count;
            }
        }

        public HistoryItem this[int i]
        {
            get
            {
                try
                {
                    return history[i];
                }
                catch {}
                return null;
            }
        }

        public HistoryItem Current
        {
            get
            {
                try
                {
                    return history.Count <= 0 ? null : history[history.Count - 1];
                }
                catch {}
                return null;
            }
        }

        public bool Empty
        {
            get
            {
                return history.Count <= 0;
            }
        }

        public int Span
        {
            get
            {
                return span;
            }
        }

        public History() : this(DEFAULT_SPAN) {}

        public History(int span)
        {
            this.span = (span <= 0) ? DEFAULT_SPAN : span;
            history = new List<HistoryItem>(0);
        }

        public void Sort()
        {
            history.Sort();
        }

        public void PurgeOld()
        {
            Sort();
            DateTime lmt = DateTime.Now.AddSeconds(-span);
            int cnt = history.Count(hi => hi.Time < lmt);
            if (cnt > 0)
                history.RemoveRange(0, cnt - 1);
        }

        public void Add(HistoryItem hi)
        {
            PurgeOld();
            if (!hi.IsDiff)
                history.Add(hi);
        }

        public void Add(DateTime time, long received, long sent)
        {
            Add(new HistoryItem(time, received, sent));
        }

        public void Clear()
        {
            history.Clear();
        }

        public HistoryDiff Diff()
        {
            return new HistoryDiff(this, span);
        }

        #region Implementation of IEnumerable

        public IEnumerator<HistoryItem> GetEnumerator()
        {
            Sort();
            return history.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class HistoryDiff : IEnumerable<HistoryDiffItem>
    {
        private readonly List<HistoryDiffItem> diffs;
        private readonly int span;

        public int Count
        {
            get
            {
                return diffs.Count;
            }
        }

        public HistoryDiffItem this[int i]
        {
            get
            {
                try
                {
                    return diffs[i];
                }
                catch {}
                return null;
            }
        }

        public HistoryDiffItem Current
        {
            get
            {
                try
                {
                    return diffs.Count <= 0 ? null : diffs[diffs.Count - 1];
                }
                catch { }
                return null;
            }
        }

        public bool Empty
        {
            get
            {
                return diffs.Count <= 0;
            }
        }

        public int Span
        {
            get
            {
                return span;
            }
        }

        internal HistoryDiff(History h, int s)
        {
            diffs = MakeDiffs(h);
            span = s;
        }

        private static List<HistoryDiffItem> MakeDiffs(History h)
        {
            h.PurgeOld();
            List<HistoryDiffItem> diffs = new List<HistoryDiffItem>(0);
            if (h.Count <= 1) return diffs;
            List<HistoryItem> tmp = new List<HistoryItem>(0);
            for (int i = 1; i < h.Count; i++)
            {
                tmp.Add(h[i] % h[i-1]);
            }
            diffs.AddRange(tmp.Select(hi => hi - tmp[0]));
            return diffs;
        }

        public void Sort()
        {
            diffs.Sort();
        }

        #region Implementation of IEnumerable

        public IEnumerator<HistoryDiffItem> GetEnumerator()
        {
            Sort();
            return diffs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class HistoryItem : IComparable<HistoryItem>
    {
        public DateTime Time { get; private set; }
        public double Received { get; private set; }
        public double Sent { get; private set; }
        public bool IsDiff { get; private set; }

        public HistoryItem(DateTime time, long received, long sent) : this(time, received, sent, false) {}

        private HistoryItem(DateTime time, double received, double sent, bool diff)
        {
            Time = time;
            Received = received;
            Sent = sent;
            IsDiff = diff;
        }

        public static HistoryItem operator %(HistoryItem o1, HistoryItem o2)
        {
            try
            {
                if (o1.IsDiff || o2.IsDiff) return null;
                double brd = o1.Received - o2.Received;
                double bsd = o1.Sent - o2.Sent;
                TimeSpan dtd = o1.Time - o2.Time;
                double brps = brd / dtd.TotalSeconds;
                double bsps = bsd / dtd.TotalSeconds;
                return new HistoryItem(o1.Time, brps, bsps, true);
            }
            catch {}
            return null;
        }

        public static HistoryDiffItem operator -(HistoryItem o1, HistoryItem o2)
        {
            try
            {
                if (!o1.IsDiff || !o2.IsDiff) return null;
                return new HistoryDiffItem(o1.Time - o2.Time, o1.Received, o1.Sent);
            }
            catch {}
            return null;
        }

        public int CompareTo(HistoryItem other)
        {
            if (other == null) return -1;
            return Time.CompareTo(other.Time);
        }
    }

    public class HistoryDiffItem : IComparable<HistoryDiffItem>
    {
        public TimeSpan Time { get; private set; }
        public double Received { get; private set; }
        public double Sent { get; private set; }

        internal HistoryDiffItem(TimeSpan time, double received, double sent)
        {
            Time = time;
            Received = received;
            Sent = sent;
        }

        public int CompareTo(HistoryDiffItem other)
        {
            if (other == null) return -1;
            return Time.CompareTo(other.Time);
        }
    }
}