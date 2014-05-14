namespace SomeTechie.RoundRobinScheduler
{
    partial class SeatingDisplay
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
            this.scrollingPanel = new System.Windows.Forms.Panel();
            this.seatingPanel = new System.Windows.Forms.Panel();
            this.scrollingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollingPanel
            // 
            this.scrollingPanel.AutoScroll = true;
            this.scrollingPanel.Controls.Add(this.seatingPanel);
            this.scrollingPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scrollingPanel.Name = "scrollingPanel";
            this.scrollingPanel.Size = new System.Drawing.Size(148, 133);
            this.scrollingPanel.TabIndex = 1;
            // 
            // seatingPanel
            // 
            this.seatingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seatingPanel.Location = new System.Drawing.Point(0, 0);
            this.seatingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.seatingPanel.Name = "seatingPanel";
            this.seatingPanel.Size = new System.Drawing.Size(148, 123);
            this.seatingPanel.TabIndex = 0;
            this.seatingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.seatingPanel_Paint);
            // 
            // SeatingDisplay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.scrollingPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SeatingDisplay";
            this.Size = new System.Drawing.Size(200, 185);
            this.Load += new System.EventHandler(this.SeatingDisplay_Load);
            this.Resize += new System.EventHandler(this.SeatingDisplay_Resize);
            this.scrollingPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollingPanel;
        private System.Windows.Forms.Panel seatingPanel;
    }
}
