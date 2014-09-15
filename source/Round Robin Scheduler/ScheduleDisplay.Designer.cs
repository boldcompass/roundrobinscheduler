namespace SomeTechie.RoundRobinScheduler
{
    partial class ScheduleDisplay
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
            this.scoreEditor = new SomeTechie.RoundRobinScheduler.ScoreEditor();
            this.courtRoundsPaintable = new SomeTechie.RoundRobinScheduler.Paintable();
            this.btnMoreRounds = new System.Windows.Forms.Button();
            this.scrollingPanel.SuspendLayout();
            this.courtRoundsPaintable.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollingPanel
            // 
            this.scrollingPanel.AutoScroll = true;
            this.scrollingPanel.Controls.Add(this.scoreEditor);
            this.scrollingPanel.Controls.Add(this.courtRoundsPaintable);
            this.scrollingPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scrollingPanel.Name = "scrollingPanel";
            this.scrollingPanel.Size = new System.Drawing.Size(278, 157);
            this.scrollingPanel.TabIndex = 0;
            // 
            // scoreEditor
            // 
            this.scoreEditor.AutoSize = true;
            this.scoreEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(107)))), ((int)(((byte)(0)))));
            this.scoreEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreEditor.Location = new System.Drawing.Point(0, 0);
            this.scoreEditor.Margin = new System.Windows.Forms.Padding(4);
            this.scoreEditor.Name = "scoreEditor";
            this.scoreEditor.Size = new System.Drawing.Size(175, 72);
            this.scoreEditor.TabIndex = 1;
            this.scoreEditor.Visible = false;
            this.scoreEditor.scoreEditorClosed += new SomeTechie.RoundRobinScheduler.ScoreEditorClosedEventHandler(this.scoreEditor_scoreEditorClosed);
            this.scoreEditor.scoreEditorClosing += new SomeTechie.RoundRobinScheduler.ScoreEditorClosingEventHandler(this.scoreEditor_scoreEditorClosing);
            // 
            // courtRoundsPaintable
            // 
            this.courtRoundsPaintable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.courtRoundsPaintable.Controls.Add(this.btnMoreRounds);
            this.courtRoundsPaintable.Location = new System.Drawing.Point(0, 0);
            this.courtRoundsPaintable.Margin = new System.Windows.Forms.Padding(0);
            this.courtRoundsPaintable.Name = "courtRoundsPaintable";
            this.courtRoundsPaintable.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.courtRoundsPaintable.Size = new System.Drawing.Size(379, 100);
            this.courtRoundsPaintable.TabIndex = 0;
            this.courtRoundsPaintable.Paint += new System.Windows.Forms.PaintEventHandler(this.courtRoundsPanel_Paint);
            this.courtRoundsPaintable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.courtRoundsPaintable_KeyPress);
            this.courtRoundsPaintable.Leave += new System.EventHandler(this.courtRoundsPanel_Leave);
            this.courtRoundsPaintable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseClick);
            this.courtRoundsPaintable.MouseLeave += new System.EventHandler(this.courtRoundsPanel_MouseLeave);
            this.courtRoundsPaintable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseMove);
            this.courtRoundsPaintable.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.courtRoundsPaintable_PreviewKeyDown);
            // 
            // btnMoreRounds
            // 
            this.btnMoreRounds.AutoSize = true;
            this.btnMoreRounds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMoreRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnMoreRounds.Location = new System.Drawing.Point(228, 72);
            this.btnMoreRounds.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.btnMoreRounds.Name = "btnMoreRounds";
            this.btnMoreRounds.Size = new System.Drawing.Size(103, 23);
            this.btnMoreRounds.TabIndex = 1;
            this.btnMoreRounds.TabStop = false;
            this.btnMoreRounds.Text = "Add More Rounds";
            this.btnMoreRounds.UseVisualStyleBackColor = true;
            this.btnMoreRounds.Click += new System.EventHandler(this.btnMoreRounds_Click);
            // 
            // ScheduleDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollingPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScheduleDisplay";
            this.Size = new System.Drawing.Size(342, 191);
            this.Load += new System.EventHandler(this.ScheduleDisplay_Load);
            this.Resize += new System.EventHandler(this.ScheduleDisplay_Resize);
            this.scrollingPanel.ResumeLayout(false);
            this.scrollingPanel.PerformLayout();
            this.courtRoundsPaintable.ResumeLayout(false);
            this.courtRoundsPaintable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollingPanel;
        private Paintable courtRoundsPaintable;
        private ScoreEditor scoreEditor;
        private System.Windows.Forms.Button btnMoreRounds;

    }
}
