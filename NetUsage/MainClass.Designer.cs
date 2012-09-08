namespace NetUsage
{
    partial class MainClass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainClass));
            this.displayLabel = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideShowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aotItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayLabel
            // 
            this.displayLabel.AutoSize = true;
            this.displayLabel.BackColor = System.Drawing.Color.LightGray;
            this.displayLabel.Location = new System.Drawing.Point(0, 0);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Padding = new System.Windows.Forms.Padding(5);
            this.displayLabel.Size = new System.Drawing.Size(109, 23);
            this.displayLabel.TabIndex = 0;
            this.displayLabel.Text = "No Active Adapters";
            this.displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "NetUsage";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideShowItem,
            this.aotItem,
            this.transparentItem,
            this.exitItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.ShowCheckMargin = true;
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(240, 92);
            // 
            // hideShowItem
            // 
            this.hideShowItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hideShowItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideShowItem.Name = "hideShowItem";
            this.hideShowItem.ShortcutKeyDisplayString = "Ctrl+Alt+Shift+N";
            this.hideShowItem.Size = new System.Drawing.Size(239, 22);
            this.hideShowItem.Text = "Hide/Show";
            this.hideShowItem.Click += new System.EventHandler(this.showItem_Click);
            // 
            // aotItem
            // 
            this.aotItem.Checked = true;
            this.aotItem.CheckOnClick = true;
            this.aotItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aotItem.Name = "aotItem";
            this.aotItem.Size = new System.Drawing.Size(239, 22);
            this.aotItem.Text = "Always On Top";
            this.aotItem.CheckedChanged += new System.EventHandler(this.aotItem_CheckedChanged);
            // 
            // transparentItem
            // 
            this.transparentItem.Checked = true;
            this.transparentItem.CheckOnClick = true;
            this.transparentItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transparentItem.Name = "transparentItem";
            this.transparentItem.Size = new System.Drawing.Size(239, 22);
            this.transparentItem.Text = "Transparent";
            this.transparentItem.CheckedChanged += new System.EventHandler(this.transparentItem_CheckedChanged);
            // 
            // exitItem
            // 
            this.exitItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(239, 22);
            this.exitItem.Text = "Exit";
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // MainClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.displayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainClass";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NetUsage";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainClass_FormClosing);
            this.Load += new System.EventHandler(this.MainClass_Load);
            this.Shown += new System.EventHandler(this.MainClass_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainClass_KeyUp);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.ToolStripMenuItem hideShowItem;
        private System.Windows.Forms.ToolStripMenuItem aotItem;
        private System.Windows.Forms.ToolStripMenuItem transparentItem;
    }
}

