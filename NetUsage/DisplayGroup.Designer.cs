namespace NetUsage
{
    partial class DisplayGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayLabelHeader = new System.Windows.Forms.Label();
            this.displayImage = new System.Windows.Forms.PictureBox();
            this.displayLabelIncoming = new System.Windows.Forms.Label();
            this.displayLabelOutgoing = new System.Windows.Forms.Label();
            this.displayLabelFooter = new System.Windows.Forms.Label();
            this.incomingLabel = new System.Windows.Forms.Label();
            this.outgoingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).BeginInit();
            this.SuspendLayout();
            // 
            // displayLabelHeader
            // 
            this.displayLabelHeader.AutoSize = true;
            this.displayLabelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.displayLabelHeader.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabelHeader.Location = new System.Drawing.Point(0, 60);
            this.displayLabelHeader.Name = "displayLabelHeader";
            this.displayLabelHeader.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.displayLabelHeader.Size = new System.Drawing.Size(59, 19);
            this.displayLabelHeader.TabIndex = 0;
            this.displayLabelHeader.Text = "Header";
            // 
            // displayImage
            // 
            this.displayImage.BackColor = System.Drawing.SystemColors.Control;
            this.displayImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.displayImage.Location = new System.Drawing.Point(0, 0);
            this.displayImage.Name = "displayImage";
            this.displayImage.Padding = new System.Windows.Forms.Padding(5);
            this.displayImage.Size = new System.Drawing.Size(146, 60);
            this.displayImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.displayImage.TabIndex = 1;
            this.displayImage.TabStop = false;
            // 
            // displayLabelIncoming
            // 
            this.displayLabelIncoming.AutoSize = true;
            this.displayLabelIncoming.BackColor = System.Drawing.SystemColors.Control;
            this.displayLabelIncoming.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabelIncoming.ForeColor = System.Drawing.Color.LimeGreen;
            this.displayLabelIncoming.Location = new System.Drawing.Point(75, 90);
            this.displayLabelIncoming.Name = "displayLabelIncoming";
            this.displayLabelIncoming.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.displayLabelIncoming.Size = new System.Drawing.Size(68, 14);
            this.displayLabelIncoming.TabIndex = 2;
            this.displayLabelIncoming.Text = "Incoming";
            // 
            // displayLabelOutgoing
            // 
            this.displayLabelOutgoing.AutoSize = true;
            this.displayLabelOutgoing.BackColor = System.Drawing.SystemColors.Control;
            this.displayLabelOutgoing.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabelOutgoing.ForeColor = System.Drawing.Color.Red;
            this.displayLabelOutgoing.Location = new System.Drawing.Point(75, 105);
            this.displayLabelOutgoing.Name = "displayLabelOutgoing";
            this.displayLabelOutgoing.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.displayLabelOutgoing.Size = new System.Drawing.Size(68, 14);
            this.displayLabelOutgoing.TabIndex = 3;
            this.displayLabelOutgoing.Text = "Outgoing";
            // 
            // displayLabelFooter
            // 
            this.displayLabelFooter.AutoSize = true;
            this.displayLabelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.displayLabelFooter.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabelFooter.Location = new System.Drawing.Point(0, 125);
            this.displayLabelFooter.Name = "displayLabelFooter";
            this.displayLabelFooter.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.displayLabelFooter.Size = new System.Drawing.Size(52, 19);
            this.displayLabelFooter.TabIndex = 4;
            this.displayLabelFooter.Text = "Total";
            // 
            // incomingLabel
            // 
            this.incomingLabel.AutoSize = true;
            this.incomingLabel.BackColor = System.Drawing.SystemColors.Control;
            this.incomingLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incomingLabel.Location = new System.Drawing.Point(0, 90);
            this.incomingLabel.Name = "incomingLabel";
            this.incomingLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.incomingLabel.Size = new System.Drawing.Size(75, 14);
            this.incomingLabel.TabIndex = 5;
            this.incomingLabel.Text = "Incoming:";
            // 
            // outgoingLabel
            // 
            this.outgoingLabel.AutoSize = true;
            this.outgoingLabel.BackColor = System.Drawing.SystemColors.Control;
            this.outgoingLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outgoingLabel.Location = new System.Drawing.Point(0, 105);
            this.outgoingLabel.Name = "outgoingLabel";
            this.outgoingLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.outgoingLabel.Size = new System.Drawing.Size(68, 14);
            this.outgoingLabel.TabIndex = 6;
            this.outgoingLabel.Text = "Outgoing";
            // 
            // DisplayGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.outgoingLabel);
            this.Controls.Add(this.incomingLabel);
            this.Controls.Add(this.displayLabelFooter);
            this.Controls.Add(this.displayLabelOutgoing);
            this.Controls.Add(this.displayLabelIncoming);
            this.Controls.Add(this.displayImage);
            this.Controls.Add(this.displayLabelHeader);
            this.Name = "DisplayGroup";
            this.Size = new System.Drawing.Size(146, 144);
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label displayLabelHeader;
        private System.Windows.Forms.PictureBox displayImage;
        private System.Windows.Forms.Label displayLabelIncoming;
        private System.Windows.Forms.Label displayLabelOutgoing;
        private System.Windows.Forms.Label displayLabelFooter;
        private System.Windows.Forms.Label incomingLabel;
        private System.Windows.Forms.Label outgoingLabel;
    }
}
