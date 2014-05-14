namespace SomeTechie.RoundRobinScheduler
{
    partial class ScoreKeepersScheduleDisplay
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
            this.courtRoundsPaintable = new Paintable();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.numRoundsUpDwn = new System.Windows.Forms.NumericUpDown();
            this.assignScoreKeeperLbl = new System.Windows.Forms.Label();
            this.assignScoreKeeperCmb = new System.Windows.Forms.ComboBox();
            this.numRoundsLbl = new System.Windows.Forms.Label();
            this.scrollingPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundsUpDwn)).BeginInit();
            this.SuspendLayout();
            // 
            // scrollingPanel
            // 
            this.scrollingPanel.AutoScroll = true;
            this.scrollingPanel.Controls.Add(this.courtRoundsPaintable);
            this.scrollingPanel.Location = new System.Drawing.Point(0, 33);
            this.scrollingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scrollingPanel.Name = "scrollingPanel";
            this.scrollingPanel.Size = new System.Drawing.Size(371, 193);
            this.scrollingPanel.TabIndex = 0;
            // 
            // courtRoundsPanel
            // 
            this.courtRoundsPaintable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.courtRoundsPaintable.Location = new System.Drawing.Point(0, 0);
            this.courtRoundsPaintable.Margin = new System.Windows.Forms.Padding(0);
            this.courtRoundsPaintable.Name = "courtRoundsPanel";
            this.courtRoundsPaintable.Size = new System.Drawing.Size(371, 123);
            this.courtRoundsPaintable.TabIndex = 0;
            this.courtRoundsPaintable.Paint += new System.Windows.Forms.PaintEventHandler(this.courtRoundsPanel_Paint);
            this.courtRoundsPaintable.Leave += new System.EventHandler(this.courtRoundsPanel_Leave);
            this.courtRoundsPaintable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseClick);
            this.courtRoundsPaintable.MouseLeave += new System.EventHandler(this.courtRoundsPanel_MouseLeave);
            this.courtRoundsPaintable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseMove);
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.numRoundsUpDwn);
            this.controlsPanel.Controls.Add(this.assignScoreKeeperLbl);
            this.controlsPanel.Controls.Add(this.assignScoreKeeperCmb);
            this.controlsPanel.Controls.Add(this.numRoundsLbl);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Margin = new System.Windows.Forms.Padding(4);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(925, 30);
            this.controlsPanel.TabIndex = 1;
            this.controlsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controlsPanel_Paint);
            // 
            // numRoundsUpDwn
            // 
            this.numRoundsUpDwn.Dock = System.Windows.Forms.DockStyle.Left;
            this.numRoundsUpDwn.Location = new System.Drawing.Point(133, 0);
            this.numRoundsUpDwn.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.numRoundsUpDwn.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numRoundsUpDwn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoundsUpDwn.Name = "numRoundsUpDwn";
            this.numRoundsUpDwn.Size = new System.Drawing.Size(73, 22);
            this.numRoundsUpDwn.TabIndex = 1;
            this.numRoundsUpDwn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoundsUpDwn.ValueChanged += new System.EventHandler(this.numRoundsUpDwn_ValueChanged);
            // 
            // assignScoreKeeperLbl
            // 
            this.assignScoreKeeperLbl.AutoSize = true;
            this.assignScoreKeeperLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.assignScoreKeeperLbl.Location = new System.Drawing.Point(470, 0);
            this.assignScoreKeeperLbl.Margin = new System.Windows.Forms.Padding(0);
            this.assignScoreKeeperLbl.Name = "assignScoreKeeperLbl";
            this.assignScoreKeeperLbl.Padding = new System.Windows.Forms.Padding(1);
            this.assignScoreKeeperLbl.Size = new System.Drawing.Size(147, 19);
            this.assignScoreKeeperLbl.TabIndex = 3;
            this.assignScoreKeeperLbl.Text = "Assign Score Keeper:";
            // 
            // assignScoreKeeperCmb
            // 
            this.assignScoreKeeperCmb.Dock = System.Windows.Forms.DockStyle.Right;
            this.assignScoreKeeperCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.assignScoreKeeperCmb.FormattingEnabled = true;
            this.assignScoreKeeperCmb.Location = new System.Drawing.Point(617, 0);
            this.assignScoreKeeperCmb.Margin = new System.Windows.Forms.Padding(4);
            this.assignScoreKeeperCmb.Name = "assignScoreKeeperCmb";
            this.assignScoreKeeperCmb.Size = new System.Drawing.Size(308, 24);
            this.assignScoreKeeperCmb.TabIndex = 2;
            // 
            // numRoundsLbl
            // 
            this.numRoundsLbl.AutoSize = true;
            this.numRoundsLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.numRoundsLbl.Location = new System.Drawing.Point(0, 0);
            this.numRoundsLbl.Margin = new System.Windows.Forms.Padding(0);
            this.numRoundsLbl.Name = "numRoundsLbl";
            this.numRoundsLbl.Padding = new System.Windows.Forms.Padding(1);
            this.numRoundsLbl.Size = new System.Drawing.Size(133, 19);
            this.numRoundsLbl.TabIndex = 0;
            this.numRoundsLbl.Text = "Number of Rounds:";
            // 
            // ScoreKeepersScheduleDisplay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.scrollingPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScoreKeepersScheduleDisplay";
            this.Size = new System.Drawing.Size(925, 235);
            this.Load += new System.EventHandler(this.ScheduleDisplay_Load);
            this.Resize += new System.EventHandler(this.ScheduleDisplay_Resize);
            this.scrollingPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundsUpDwn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollingPanel;
        private Paintable courtRoundsPaintable;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.NumericUpDown numRoundsUpDwn;
        private System.Windows.Forms.Label assignScoreKeeperLbl;
        private System.Windows.Forms.ComboBox assignScoreKeeperCmb;
        protected System.Windows.Forms.Label numRoundsLbl;

    }
}
