using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Drawing;

namespace NetUsage
{
    public class Graph
    {
        private Graph() {}

        public static Bitmap GraphSpeeds(HistoryDiff diff, int width, int height, Color incoming, Color outgoing, Color background, float thickness)
        {
            Bitmap img = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            GC.Collect();
            Graphics g = Graphics.FromImage(img);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(background);
            if (diff.Count <= 1)
                return img;
            Pen incomingPen = new Pen(incoming, thickness);
            Pen outgoingPen = new Pen(outgoing, thickness);
            double maxr = (diff.OrderByDescending(hdi => hdi.Received)).First().Received;
            double maxs = (diff.OrderByDescending(hdi => hdi.Sent)).First().Sent;
            double max = Math.Max(maxr, maxs);
            if (max <= 0)
                return img;
            List<PointF> pointsR = new List<PointF>(0);
            List<PointF> pointsS = new List<PointF>(0);
            diff.Sort();
            foreach (HistoryDiffItem hdi in diff)
            {
                float x = (float) (((hdi.Time.TotalSeconds + (diff.Span - diff.Current.Time.TotalSeconds)) / diff.Span) * width);
                float yR = (float) (height - ((hdi.Received / max) * (height * 0.8)));
                pointsR.Add(new PointF(x, yR));
                float yS = (float) (height - ((hdi.Sent / max) * (height * 0.8)));
                pointsS.Add(new PointF(x, yS));
            }
            g.DrawLines(incomingPen, pointsR.ToArray());
            g.DrawLines(outgoingPen, pointsS.ToArray());
            incomingPen.Dispose();
            outgoingPen.Dispose();
            g.Dispose();
            return img;
        }
    }
}