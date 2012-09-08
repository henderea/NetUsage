using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming
namespace NetUsage
{
    public partial class MainClass : Form
    {
        public MainClass()
        {
            InitializeComponent();
            try
            {
                histories = new Dictionary<string, History>(0);
                history = new History(5);
                activeAdapters = new Dictionary<string, bool>(0);
                groups = new Dictionary<string, DisplayGroup>(0);
                headers = new Dictionary<string, string>(0);
                TopMost = Properties.Settings.Default.AlwaysOnTop;
                aotItem.Checked = Properties.Settings.Default.AlwaysOnTop;
                //Opacity = Properties.Settings.Default.Transparent ? 0.75 : 1;
                transparentItem.Checked = Properties.Settings.Default.Transparent;
                Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - Width, Screen.PrimaryScreen.WorkingArea.Bottom - Height);
                updateTimer.Start();
                notifyIcon.Icon = Icon.FromHandle(Graph.GraphSpeeds(history.Diff(), 100, 100, Color.LimeGreen, Color.Red, Color.Black, 3).GetHicon());
            }
            catch {}
        }

        private readonly Dictionary<string, History> histories;
        private readonly History history;
        private readonly Dictionary<string, bool> activeAdapters;
        private readonly Dictionary<string, DisplayGroup> groups;
        private readonly Dictionary<string, string> headers; 

        private void UpdateInfo()
        {
            try
            {
                if (activeAdapters.Count > 0)
                {
                    List<string> keys = new List<string>(activeAdapters.Keys);
                    foreach (string key in keys)
                    {
                        activeAdapters[key] = false;
                    }
                }
                if (!NetworkInterface.GetIsNetworkAvailable())
                    return;
                NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
                DateTime cur = DateTime.Now;
                long brt = 0;
                long bst = 0;
                foreach (NetworkInterface ni in nis)
                {
                    try
                    {
                        if (ni.OperationalStatus != OperationalStatus.Up || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                        string ip = "";
                        UnicastIPAddressInformationCollection ua = ni.GetIPProperties().UnicastAddresses;
                        if (ua.Count > 0)
                            ip = "; " + ua[0].Address;
                        headers[ni.Id] = ni.Name + " (" + ni.NetworkInterfaceType + ip + ")";
                        IPv4InterfaceStatistics st = ni.GetIPv4Statistics();
                        long br = st.BytesReceived;
                        long bs = st.BytesSent;
                        brt += br;
                        bst += bs;
                        if (!histories.ContainsKey(ni.Id))
                            histories[ni.Id] = new History();
                        histories[ni.Id].Add(cur, br, bs);
                        activeAdapters[ni.Id] = true;
                    }
                    catch {}
                }
                history.Add(cur, brt, bst);
            }
            catch {}
        }

        private static readonly string[] sizeEndings = new[] { "B", "KB", "MB", "GB", "TB" };

        public static string FormatSizestr(double size)
        {
            if (size <= 0) return "0B";
            int i = 0;
            while (size >= 1024 && i < sizeEndings.Length - 1)
            {
                i++;
                size = size / 1024.0;
            }
            return size.ToString("#,##0.###") + sizeEndings[i];
        }

        private void UpdateWindow()
        {
            //try
            //{
                if (activeAdapters.Count <= 0)
                {
                    displayLabel.Visible = true;
                    return;
                }
                List<string> keys = new List<string>(activeAdapters.Keys);
                keys.Sort();
                bool hasActiveAdapter = false;
                int y = 0;
                foreach (string key in keys)
                {
                    try
                    {
                        if (!groups.ContainsKey(key))
                        {
                            groups[key] = new DisplayGroup();
                            Controls.Add(groups[key]);
                        }
                        groups[key].Visible = activeAdapters[key];
                        if (!activeAdapters[key]) continue;
                        hasActiveAdapter = true;
                        groups[key].DisplayData(headers[key], histories[key]);
                        groups[key].Location = new Point(0, y);
                        y += groups[key].Height;
                    }
                    catch {}
                }
                displayLabel.Visible = !hasActiveAdapter;
                Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - Width, Screen.PrimaryScreen.WorkingArea.Bottom - Height);
                notifyIcon.Icon = Icon.FromHandle(Graph.GraphSpeeds(history.Diff(), 100, 100, Color.LimeGreen, Color.Red, Color.Black, 3).GetHicon());
            //}
            //catch {}
            UpdateImage();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateInfo();
            UpdateWindow();
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*// P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_BYPOSITION = 0x400;
        private const int MF_STRING = 0x0;
        private const int MF_CHECK = 0x8;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ModifyMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);


        private const int SYSMENU_AOT_ID = 0x1;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(Handle, false);

            int mopt = MF_STRING | MF_BYPOSITION;
            if (Properties.Settings.Default.AlwaysOnTop)
                mopt |= MF_CHECK;

            // Add the menu item
            InsertMenu(hSysMenu, 0, mopt, SYSMENU_AOT_ID, "&Always On Top");

            // Add a separator
            InsertMenu(hSysMenu, 1, MF_SEPARATOR | MF_BYPOSITION, 0, string.Empty);
        }*/

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            /*if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_AOT_ID))
            {
                Properties.Settings.Default.AlwaysOnTop = !Properties.Settings.Default.AlwaysOnTop;
                Properties.Settings.Default.Save();

                TopMost = Properties.Settings.Default.AlwaysOnTop;
                aotItem.Checked = Properties.Settings.Default.AlwaysOnTop;

                // Get a handle to a copy of this form's system (window) menu
                IntPtr hSysMenu = GetSystemMenu(Handle, false);

                int mopt = MF_STRING | MF_BYPOSITION;
                if (Properties.Settings.Default.AlwaysOnTop)
                    mopt |= MF_CHECK;

                // Add the menu item
                ModifyMenu(hSysMenu, 0, mopt, SYSMENU_AOT_ID, "&Always On Top");
            }*/
            if (m.Msg == WindowsShell.WM_HOTKEY)
            {
                Visible = !Visible;
                if (Visible)
                {
                    BringToFront();
                    Activate();
                }
            }
        }

        private void MainClass_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            WindowsShell.UnregisterHotKey(this);
        }

        private void showItem_Click(object sender, EventArgs e)
        {
            Visible = !Visible;
            if (Visible)
            {
                BringToFront();
                Activate();
            }
        }

        private void aotItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AlwaysOnTop = aotItem.Checked;
            Properties.Settings.Default.Save();

            TopMost = Properties.Settings.Default.AlwaysOnTop;

            /*// Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(Handle, false);

            int mopt = MF_STRING | MF_BYPOSITION;
            if (Properties.Settings.Default.AlwaysOnTop)
                mopt |= MF_CHECK;

            // Add the menu item
            ModifyMenu(hSysMenu, 0, mopt, SYSMENU_AOT_ID, "&Always On Top");*/
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Visible = !Visible;
            if (Visible)
            {
                BringToFront();
                Activate();
            }
        }

        private void MainClass_Load(object sender, EventArgs e)
        {
            const Keys k = Keys.N | Keys.Control | Keys.Alt | Keys.Shift;
            WindowsShell.RegisterHotKey(this, k);
        }

        private void MainClass_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape && !e.Control && !e.Alt)
            {
                e.Handled = true;
                if(e.Shift)
                    Application.Exit();
                else
                    Hide();
            }
            else if(e.Control && !e.Shift && !e.Alt)
            {
                e.Handled = true;
                if(e.KeyCode == Keys.A)
                {
                    Properties.Settings.Default.AlwaysOnTop = !Properties.Settings.Default.AlwaysOnTop;
                    Properties.Settings.Default.Save();
                    TopMost = Properties.Settings.Default.AlwaysOnTop;
                    aotItem.Checked = Properties.Settings.Default.AlwaysOnTop;
                }
                else if (e.KeyCode == Keys.T)
                {
                    Properties.Settings.Default.Transparent = !Properties.Settings.Default.Transparent;
                    Properties.Settings.Default.Save();
                    //Opacity = Properties.Settings.Default.Transparent ? 0.75 : 1;
                    transparentItem.Checked = Properties.Settings.Default.Transparent;
                }
            }
        }

        private void transparentItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Transparent = transparentItem.Checked;
            Properties.Settings.Default.Save();
            //Opacity = Properties.Settings.Default.Transparent ? 0.75 : 1;
        }

        private void MainClass_Shown(object sender, EventArgs e)
        {
            BringToFront();
            Activate();
        }

        public void UpdateImage()
        {
            if (!Visible) return;
            Bitmap temp_bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(temp_bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.Clear(Color.LightGray);
            Rectangle b = new Rectangle(0, 0, Width, Height);
            foreach (Control ctrl in Controls)
            {
                if (Visible && ctrl.Visible && b.Contains(ctrl.Bounds))
                    ctrl.DrawToBitmap(temp_bmp, ctrl.Bounds);
            }
            temp_bmp.MakeTransparent();
            Bitmap img = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Graphics g2 = Graphics.FromImage(img);
            g2.CompositingQuality = CompositingQuality.HighQuality;
            g2.InterpolationMode = InterpolationMode.High;
            g2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g2.Clear(Color.FromArgb(Properties.Settings.Default.Transparent ? 190 : 255, Color.White));
            g2.DrawImage(temp_bmp, 0, 0);
            SetBitmap(img);
            Invalidate(new Rectangle(new Point(0, 0), Size), false);
        }

        public void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }

        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = Win32.SelectObject(memDc, hBitmap);
                Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.Point pointSource = new Win32.Point(0, 0);
                Win32.Point topPos = new Win32.Point(Left, Top);
                Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;
                Win32.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.DeleteDC(memDc);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                return cp;
            }
        }
    }

    public class WindowsShell
    {
        #region fields
        // ReSharper disable InconsistentNaming
        public static int MOD_ALT = 0x1;
        public static int MOD_CONTROL = 0x2;
        public static int MOD_SHIFT = 0x4;
        public static int MOD_WIN = 0x8;
        public static int WM_HOTKEY = 0x312;
        // ReSharper restore InconsistentNaming
        #endregion

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int keyId;
        public static void RegisterHotKey(Form f, Keys key)
        {
            int modifiers = 0;
            if ((key & Keys.Alt) == Keys.Alt)
                modifiers = modifiers | MOD_ALT;
            if ((key & Keys.Control) == Keys.Control)
                modifiers = modifiers | MOD_CONTROL;
            if ((key & Keys.Shift) == Keys.Shift)
                modifiers = modifiers | MOD_SHIFT;
            Keys k = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;
            keyId = f.GetHashCode(); // this should be a key unique ID, modify this if you want more than one hotkey
            RegisterHotKey(f.Handle, keyId, (uint)modifiers, (uint)k);
        }

        public static void UnregisterHotKey(Form f)
        {
            try
            {
                UnregisterHotKey(f.Handle, keyId); // modify this if you want more than one hotkey
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    class Win32
    {
        public enum Bool
        {
            False = 0,
            True
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        // ReSharper disable UnusedMember.Local
        struct ARGB
        // ReSharper restore UnusedMember.Local
        {
            // ReSharper disable FieldCanBeMadeReadOnly.Local
            // ReSharper disable MemberCanBePrivate.Local
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
            // ReSharper restore MemberCanBePrivate.Local
            // ReSharper restore FieldCanBeMadeReadOnly.Local
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }


        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);
    }
}
// ReSharper restore InconsistentNaming