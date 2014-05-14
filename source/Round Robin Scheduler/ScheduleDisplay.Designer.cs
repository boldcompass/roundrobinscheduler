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
            this.btnMoreRounds = new System.Windows.Forms.Button();
            this.courtRoundsPaintable = new SomeTechie.RoundRobinScheduler.Paintable();
            this.scoreEditor = new SomeTechie.RoundRobinScheduler.ScoreEditor();
            this.scrollingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollingPanel
            // 
            this.scrollingPanel.AutoScroll = true;
            this.scrollingPanel.Controls.Add(this.courtRoundsPaintable);
            this.scrollingPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scrollingPanel.Name = "scrollingPanel";
            this.scrollingPanel.Size = new System.Drawing.Size(371, 193);
            this.scrollingPanel.TabIndex = 0;
            // 
            // btnMoreRounds
            // 
            this.btnMoreRounds.AutoSize = true;
            this.btnMoreRounds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMoreRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoreRounds.Location = new System.Drawing.Point(304, 88);
            this.btnMoreRounds.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnMoreRounds.Name = "btnMoreRounds";
            this.btnMoreRounds.Size = new System.Drawing.Size(102, 23);
            this.btnMoreRounds.TabIndex = 1;
            this.btnMoreRounds.TabStop = false;
            this.btnMoreRounds.Text = "Add More Rounds";
            this.btnMoreRounds.UseVisualStyleBackColor = true;
            this.btnMoreRounds.Click += new System.EventHandler(this.btnMoreRounds_Click);
            // 
            // courtRoundsPaintable
            // 
            this.courtRoundsPaintable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.courtRoundsPaintable.Controls.Add(this.btnMoreRounds);
            this.courtRoundsPaintable.Location = new System.Drawing.Point(0, 0);
            this.courtRoundsPaintable.Margin = new System.Windows.Forms.Padding(0);
            this.courtRoundsPaintable.Name = "courtRoundsPaintable";
            this.courtRoundsPaintable.Size = new System.Drawing.Size(505, 123);
            this.courtRoundsPaintable.TabIndex = 0;
            this.courtRoundsPaintable.Paint += new System.Windows.Forms.PaintEventHandler(this.courtRoundsPanel_Paint);
            this.courtRoundsPaintable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.courtRoundsPaintable_KeyPress);
            this.courtRoundsPaintable.Leave += new System.EventHandler(this.courtRoundsPanel_Leave);
            this.courtRoundsPaintable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseClick);
            this.courtRoundsPaintable.MouseLeave += new System.EventHandler(this.courtRoundsPanel_MouseLeave);
            this.courtRoundsPaintable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.courtRoundsPanel_MouseMove);
            this.courtRoundsPaintable.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.courtRoundsPaintable_PreviewKeyDown);
            // 
            // scoreEditor
            // 
            this.scoreEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(107)))), ((int)(((byte)(0)))));
            this.scoreEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreEditor.Location = new System.Drawing.Point(0, 0);
            this.scoreEditor.Margin = new System.Windows.Forms.Padding(5);
            this.scoreEditor.Name = "scoreEditor";
            this.scoreEditor.Size = new System.Drawing.Size(233, 89);
            this.scoreEditor.TabIndex = 1;
            this.scoreEditor.Visible = false;
            this.scoreEditor.scoreEditorClosed += new SomeTechie.RoundRobinScheduler.ScoreEditorClosedEventHandler(this.scoreEditor_scoreEditorClosed);
            // 
            // ScheduleDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scoreEditor);
            this.Controls.Add(this.scrollingPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScheduleDisplay";
            this.Size = new System.Drawing.Size(456, 235);
            this.Load += new System.EventHandler(this.ScheduleDisplay_Load);
            this.Resize += new System.EventHandler(this.ScheduleDisplay_Resize);
            this.scrollingPanel.ResumeLayout(false);
            this.scrollingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollingPanel;
        private Paintable courtRoundsPaintable;
        private ScoreEditor scoreEditor;
        private System.Windows.Forms.Button btnMoreRounds;

    }
}
