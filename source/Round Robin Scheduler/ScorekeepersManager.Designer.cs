namespace SomeTechie.RoundRobinScheduler
{
    partial class ScorekeepersManager
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
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.scoreKeeperAccessCodeHeaderLbl = new System.Windows.Forms.Label();
            this.scoreKeeperNameHeaderLbl = new System.Windows.Forms.Label();
            this.checkBoxHeader = new System.Windows.Forms.CheckBox();
            this.btnRemoveScoreKeeper = new System.Windows.Forms.Button();
            this.btnAddScoreKeeper = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 3;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutPanel.Controls.Add(this.scoreKeeperAccessCodeHeaderLbl, 2, 0);
            this.layoutPanel.Controls.Add(this.scoreKeeperNameHeaderLbl, 1, 0);
            this.layoutPanel.Controls.Add(this.checkBoxHeader, 0, 0);
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 2;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutPanel.Size = new System.Drawing.Size(238, 119);
            this.layoutPanel.TabIndex = 9;
            // 
            // scoreKeeperAccessCodeHeaderLbl
            // 
            this.scoreKeeperAccessCodeHeaderLbl.AutoSize = true;
            this.scoreKeeperAccessCodeHeaderLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoreKeeperAccessCodeHeaderLbl.Location = new System.Drawing.Point(141, 0);
            this.scoreKeeperAccessCodeHeaderLbl.Name = "scoreKeeperAccessCodeHeaderLbl";
            this.scoreKeeperAccessCodeHeaderLbl.Size = new System.Drawing.Size(94, 25);
            this.scoreKeeperAccessCodeHeaderLbl.TabIndex = 1;
            this.scoreKeeperAccessCodeHeaderLbl.Text = "Access Code";
            this.scoreKeeperAccessCodeHeaderLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreKeeperNameHeaderLbl
            // 
            this.scoreKeeperNameHeaderLbl.AutoSize = true;
            this.scoreKeeperNameHeaderLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoreKeeperNameHeaderLbl.Location = new System.Drawing.Point(28, 0);
            this.scoreKeeperNameHeaderLbl.Name = "scoreKeeperNameHeaderLbl";
            this.scoreKeeperNameHeaderLbl.Size = new System.Drawing.Size(107, 25);
            this.scoreKeeperNameHeaderLbl.TabIndex = 0;
            this.scoreKeeperNameHeaderLbl.Text = "Name";
            this.scoreKeeperNameHeaderLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxHeader
            // 
            this.checkBoxHeader.AutoSize = true;
            this.checkBoxHeader.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxHeader.Location = new System.Drawing.Point(3, 3);
            this.checkBoxHeader.Name = "checkBoxHeader";
            this.checkBoxHeader.Size = new System.Drawing.Size(19, 19);
            this.checkBoxHeader.TabIndex = 2;
            this.checkBoxHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxHeader.UseVisualStyleBackColor = true;
            this.checkBoxHeader.CheckedChanged += new System.EventHandler(this.checkBoxHeader_CheckedChanged);
            // 
            // btnRemoveScoreKeeper
            // 
            this.btnRemoveScoreKeeper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveScoreKeeper.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.remove;
            this.btnRemoveScoreKeeper.Location = new System.Drawing.Point(48, 130);
            this.btnRemoveScoreKeeper.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveScoreKeeper.Name = "btnRemoveScoreKeeper";
            this.btnRemoveScoreKeeper.Size = new System.Drawing.Size(40, 37);
            this.btnRemoveScoreKeeper.TabIndex = 8;
            this.btnRemoveScoreKeeper.UseVisualStyleBackColor = true;
            this.btnRemoveScoreKeeper.Click += new System.EventHandler(this.btnRemoveScoreKeeper_Click);
            // 
            // btnAddScoreKeeper
            // 
            this.btnAddScoreKeeper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddScoreKeeper.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.add;
            this.btnAddScoreKeeper.Location = new System.Drawing.Point(0, 130);
            this.btnAddScoreKeeper.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddScoreKeeper.Name = "btnAddScoreKeeper";
            this.btnAddScoreKeeper.Size = new System.Drawing.Size(40, 37);
            this.btnAddScoreKeeper.TabIndex = 7;
            this.btnAddScoreKeeper.UseVisualStyleBackColor = true;
            this.btnAddScoreKeeper.Click += new System.EventHandler(this.btnAddScoreKeeper_Click);
            // 
            // ScorekeepersManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.layoutPanel);
            this.Controls.Add(this.btnRemoveScoreKeeper);
            this.Controls.Add(this.btnAddScoreKeeper);
            this.Name = "ScorekeepersManager";
            this.Size = new System.Drawing.Size(241, 167);
            this.Load += new System.EventHandler(this.ScorekeepersManager_Load);
            this.Resize += new System.EventHandler(this.ScorekeepersManager_Resize);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Label scoreKeeperAccessCodeHeaderLbl;
        private System.Windows.Forms.Label scoreKeeperNameHeaderLbl;
        private System.Windows.Forms.Button btnRemoveScoreKeeper;
        private System.Windows.Forms.Button btnAddScoreKeeper;
        private System.Windows.Forms.CheckBox checkBoxHeader;
    }
}
