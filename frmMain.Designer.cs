namespace WavePlotter
{
    partial class frmMain
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
            this.ctlPlotter1 = new WavePlotter.ctlPlotter();
            this.ctlPlotter2 = new WavePlotter.ctlPlotter();
            this.scrScrollbar = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // ctlPlotter1
            // 
            this.ctlPlotter1.Location = new System.Drawing.Point(12, 12);
            this.ctlPlotter1.Name = "ctlPlotter1";
            this.ctlPlotter1.Size = new System.Drawing.Size(1192, 313);
            this.ctlPlotter1.TabIndex = 0;
            this.ctlPlotter1.UnitWidth = 0;
            // 
            // ctlPlotter2
            // 
            this.ctlPlotter2.Location = new System.Drawing.Point(12, 325);
            this.ctlPlotter2.Name = "ctlPlotter2";
            this.ctlPlotter2.Size = new System.Drawing.Size(1192, 313);
            this.ctlPlotter2.TabIndex = 1;
            this.ctlPlotter2.UnitWidth = 0;
            // 
            // scrScrollbar
            // 
            this.scrScrollbar.Location = new System.Drawing.Point(21, 317);
            this.scrScrollbar.Name = "scrScrollbar";
            this.scrScrollbar.Size = new System.Drawing.Size(1176, 17);
            this.scrScrollbar.TabIndex = 2;
            this.scrScrollbar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrScrollbar_Scroll);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 693);
            this.Controls.Add(this.scrScrollbar);
            this.Controls.Add(this.ctlPlotter2);
            this.Controls.Add(this.ctlPlotter1);
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wave Plotter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ctlPlotter ctlPlotter1;
        private ctlPlotter ctlPlotter2;
        private System.Windows.Forms.HScrollBar scrScrollbar;
    }
}

