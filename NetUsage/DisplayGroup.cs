using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetUsage
{
    public partial class DisplayGroup : UserControl
    {
        public const double SCALE = 2;
        public DisplayGroup()
        {
            InitializeComponent();
        }

        public void DisplayData(string header, History history)
        {
            HistoryDiff diff = history.Diff();
            displayLabelHeader.Text = header;
            displayLabelIncoming.Text = MainClass.FormatSizestr(diff.Empty ? 0 : diff.Current.Received)+"/s";
            displayLabelOutgoing.Text = MainClass.FormatSizestr(diff.Empty ? 0 : diff.Current.Sent) + "/s";
            double maxr = diff.Empty ? 0 : (diff.OrderByDescending(hdi => hdi.Received)).First().Received;
            double maxs = diff.Empty ? 0 : (diff.OrderByDescending(hdi => hdi.Sent)).First().Sent;
            string str = "Peak in:  "+MainClass.FormatSizestr(maxr)+"/s\n";
            str += "Peak out: " + MainClass.FormatSizestr(maxs) + "/s\n\n";
            str += "Received: " + MainClass.FormatSizestr(history.Empty ? 0 : history.Current.Received) + "\n";
            str += "Sent:     " + MainClass.FormatSizestr(history.Empty ? 0 : history.Current.Sent);
            displayLabelFooter.Text = str;
            Image i = displayImage.Image;
            displayImage.Image = Graph.GraphSpeeds(diff, (int)(SCALE*(displayImage.Width - 10)), (int)(SCALE*(displayImage.Height - 10)), Color.LimeGreen, Color.Red, Color.Black, 3);
            i.Dispose();
        }
    }
}
