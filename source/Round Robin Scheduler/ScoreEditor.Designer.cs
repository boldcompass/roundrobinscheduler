namespace SomeTechie.RoundRobinScheduler
{
    partial class ScoreEditor
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
            this.lblWinner = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblFouls = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTeam1FoulsNum = new SomeTechie.RoundRobinScheduler.NumericTextBox();
            this.txtTeam2FoulsNum = new SomeTechie.RoundRobinScheduler.NumericTextBox();
            this.txtTeam2PointsNum = new SomeTechie.RoundRobinScheduler.NumericTextBox();
            this.txtTeam1PointsNum = new SomeTechie.RoundRobinScheduler.NumericTextBox();
            this.chkConfirmed = new System.Windows.Forms.CheckBox();
            this.panelTeam2 = new System.Windows.Forms.Panel();
            this.chkTeam2Winner = new System.Windows.Forms.CheckBox();
            this.comboBoxTeam2 = new System.Windows.Forms.ComboBox();
            this.panelTeam1 = new System.Windows.Forms.Panel();
            this.chkTeam1Winner = new System.Windows.Forms.CheckBox();
            this.comboBoxTeam1 = new System.Windows.Forms.ComboBox();
            this.layoutPanel.SuspendLayout();
            this.panelTeam2.SuspendLayout();
            this.panelTeam1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 3;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.layoutPanel.Controls.Add(this.lblWinner, 0, 0);
            this.layoutPanel.Controls.Add(this.lblScore, 1, 0);
            this.layoutPanel.Controls.Add(this.lblFouls, 2, 0);
            this.layoutPanel.Controls.Add(this.btnOk, 1, 3);
            this.layoutPanel.Controls.Add(this.btnCancel, 2, 3);
            this.layoutPanel.Controls.Add(this.txtTeam1FoulsNum, 2, 1);
            this.layoutPanel.Controls.Add(this.txtTeam2FoulsNum, 2, 2);
            this.layoutPanel.Controls.Add(this.txtTeam2PointsNum, 1, 2);
            this.layoutPanel.Controls.Add(this.txtTeam1PointsNum, 1, 1);
            this.layoutPanel.Controls.Add(this.chkConfirmed, 0, 3);
            this.layoutPanel.Controls.Add(this.panelTeam2, 0, 2);
            this.layoutPanel.Controls.Add(this.panelTeam1, 0, 1);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 4;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutPanel.Size = new System.Drawing.Size(260, 95);
            this.layoutPanel.TabIndex = 0;
            // 
            // lblWinner
            // 
            this.lblWinner.AutoSize = true;
            this.lblWinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWinner.Location = new System.Drawing.Point(3, 0);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new System.Drawing.Size(144, 13);
            this.lblWinner.TabIndex = 15;
            this.lblWinner.Text = "Winner:";
            this.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScore.Location = new System.Drawing.Point(153, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(49, 13);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Score:";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFouls
            // 
            this.lblFouls.AutoSize = true;
            this.lblFouls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFouls.Location = new System.Drawing.Point(208, 0);
            this.lblFouls.Name = "lblFouls";
            this.lblFouls.Size = new System.Drawing.Size(49, 13);
            this.lblFouls.TabIndex = 3;
            this.lblFouls.Text = "Fouls:";
            this.lblFouls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(153, 72);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(49, 20);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(208, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // txtTeam1FoulsNum
            // 
            this.txtTeam1FoulsNum.AllowDecimal = false;
            this.txtTeam1FoulsNum.AllowNegative = false;
            this.txtTeam1FoulsNum.AllowSpace = false;
            this.txtTeam1FoulsNum.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTeam1FoulsNum.IntValue = 0;
            this.txtTeam1FoulsNum.Location = new System.Drawing.Point(208, 16);
            this.txtTeam1FoulsNum.Name = "txtTeam1FoulsNum";
            this.txtTeam1FoulsNum.Size = new System.Drawing.Size(49, 20);
            this.txtTeam1FoulsNum.TabIndex = 5;
            this.txtTeam1FoulsNum.Text = "0";
            this.txtTeam1FoulsNum.Enter += new System.EventHandler(this.txtNum_Enter);
            this.txtTeam1FoulsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            // 
            // txtTeam2FoulsNum
            // 
            this.txtTeam2FoulsNum.AllowDecimal = false;
            this.txtTeam2FoulsNum.AllowNegative = false;
            this.txtTeam2FoulsNum.AllowSpace = false;
            this.txtTeam2FoulsNum.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTeam2FoulsNum.IntValue = 0;
            this.txtTeam2FoulsNum.Location = new System.Drawing.Point(208, 42);
            this.txtTeam2FoulsNum.Name = "txtTeam2FoulsNum";
            this.txtTeam2FoulsNum.Size = new System.Drawing.Size(49, 20);
            this.txtTeam2FoulsNum.TabIndex = 6;
            this.txtTeam2FoulsNum.Text = "0";
            this.txtTeam2FoulsNum.Enter += new System.EventHandler(this.txtNum_Enter);
            this.txtTeam2FoulsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            // 
            // txtTeam2PointsNum
            // 
            this.txtTeam2PointsNum.AllowDecimal = false;
            this.txtTeam2PointsNum.AllowNegative = false;
            this.txtTeam2PointsNum.AllowSpace = false;
            this.txtTeam2PointsNum.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTeam2PointsNum.IntValue = 0;
            this.txtTeam2PointsNum.Location = new System.Drawing.Point(153, 42);
            this.txtTeam2PointsNum.Name = "txtTeam2PointsNum";
            this.txtTeam2PointsNum.Size = new System.Drawing.Size(49, 20);
            this.txtTeam2PointsNum.TabIndex = 4;
            this.txtTeam2PointsNum.Text = "0";
            this.txtTeam2PointsNum.Enter += new System.EventHandler(this.txtNum_Enter);
            this.txtTeam2PointsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            // 
            // txtTeam1PointsNum
            // 
            this.txtTeam1PointsNum.AllowDecimal = false;
            this.txtTeam1PointsNum.AllowNegative = false;
            this.txtTeam1PointsNum.AllowSpace = false;
            this.txtTeam1PointsNum.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTeam1PointsNum.IntValue = 0;
            this.txtTeam1PointsNum.Location = new System.Drawing.Point(153, 16);
            this.txtTeam1PointsNum.Name = "txtTeam1PointsNum";
            this.txtTeam1PointsNum.Size = new System.Drawing.Size(49, 20);
            this.txtTeam1PointsNum.TabIndex = 3;
            this.txtTeam1PointsNum.Text = "0";
            this.txtTeam1PointsNum.Enter += new System.EventHandler(this.txtNum_Enter);
            this.txtTeam1PointsNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            // 
            // chkConfirmed
            // 
            this.chkConfirmed.AutoEllipsis = true;
            this.chkConfirmed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chkConfirmed.Location = new System.Drawing.Point(3, 75);
            this.chkConfirmed.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.chkConfirmed.Name = "chkConfirmed";
            this.chkConfirmed.Size = new System.Drawing.Size(144, 17);
            this.chkConfirmed.TabIndex = 7;
            this.chkConfirmed.Text = "O&fficial";
            this.chkConfirmed.UseVisualStyleBackColor = true;
            this.chkConfirmed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            // 
            // panelTeam2
            // 
            this.panelTeam2.Controls.Add(this.chkTeam2Winner);
            this.panelTeam2.Controls.Add(this.comboBoxTeam2);
            this.panelTeam2.Location = new System.Drawing.Point(0, 39);
            this.panelTeam2.Margin = new System.Windows.Forms.Padding(0);
            this.panelTeam2.Name = "panelTeam2";
            this.panelTeam2.Size = new System.Drawing.Size(150, 26);
            this.panelTeam2.TabIndex = 18;
            // 
            // chkTeam2Winner
            // 
            this.chkTeam2Winner.AutoSize = true;
            this.chkTeam2Winner.Location = new System.Drawing.Point(3, 6);
            this.chkTeam2Winner.Name = "chkTeam2Winner";
            this.chkTeam2Winner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkTeam2Winner.Size = new System.Drawing.Size(15, 14);
            this.chkTeam2Winner.TabIndex = 3;
            this.chkTeam2Winner.UseVisualStyleBackColor = true;
            this.chkTeam2Winner.CheckedChanged += new System.EventHandler(this.team2WinnerChk_CheckedChanged);
            // 
            // comboBoxTeam2
            // 
            this.comboBoxTeam2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTeam2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeam2.FormattingEnabled = true;
            this.comboBoxTeam2.Location = new System.Drawing.Point(21, 3);
            this.comboBoxTeam2.Name = "comboBoxTeam2";
            this.comboBoxTeam2.Size = new System.Drawing.Size(129, 21);
            this.comboBoxTeam2.TabIndex = 4;
            this.comboBoxTeam2.Visible = false;
            this.comboBoxTeam2.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeam2_SelectedIndexChanged);
            // 
            // panelTeam1
            // 
            this.panelTeam1.Controls.Add(this.chkTeam1Winner);
            this.panelTeam1.Controls.Add(this.comboBoxTeam1);
            this.panelTeam1.Location = new System.Drawing.Point(0, 13);
            this.panelTeam1.Margin = new System.Windows.Forms.Padding(0);
            this.panelTeam1.Name = "panelTeam1";
            this.panelTeam1.Size = new System.Drawing.Size(150, 26);
            this.panelTeam1.TabIndex = 19;
            // 
            // chkTeam1Winner
            // 
            this.chkTeam1Winner.AutoSize = true;
            this.chkTeam1Winner.Location = new System.Drawing.Point(3, 6);
            this.chkTeam1Winner.Name = "chkTeam1Winner";
            this.chkTeam1Winner.Size = new System.Drawing.Size(15, 14);
            this.chkTeam1Winner.TabIndex = 2;
            this.chkTeam1Winner.UseVisualStyleBackColor = true;
            this.chkTeam1Winner.CheckedChanged += new System.EventHandler(this.team1WinnerChk_CheckedChanged);
            // 
            // comboBoxTeam1
            // 
            this.comboBoxTeam1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTeam1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeam1.FormattingEnabled = true;
            this.comboBoxTeam1.Location = new System.Drawing.Point(21, 3);
            this.comboBoxTeam1.Name = "comboBoxTeam1";
            this.comboBoxTeam1.Size = new System.Drawing.Size(129, 21);
            this.comboBoxTeam1.TabIndex = 3;
            this.comboBoxTeam1.Visible = false;
            this.comboBoxTeam1.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeam1_SelectedIndexChanged);
            // 
            // ScoreEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutPanel);
            this.Name = "ScoreEditor";
            this.Size = new System.Drawing.Size(260, 95);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScoreEditor_KeyPress);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.panelTeam2.ResumeLayout(false);
            this.panelTeam2.PerformLayout();
            this.panelTeam1.ResumeLayout(false);
            this.panelTeam1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Label lblScore;
        private NumericTextBox txtTeam2PointsNum;
        private NumericTextBox txtTeam1PointsNum;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblWinner;
        private System.Windows.Forms.Label lblFouls;
        private NumericTextBox txtTeam1FoulsNum;
        private NumericTextBox txtTeam2FoulsNum;
        private System.Windows.Forms.CheckBox chkConfirmed;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkTeam1Winner;
        private System.Windows.Forms.CheckBox chkTeam2Winner;
        private System.Windows.Forms.ComboBox comboBoxTeam1;
        private System.Windows.Forms.ComboBox comboBoxTeam2;
        private System.Windows.Forms.Panel panelTeam2;
        private System.Windows.Forms.Panel panelTeam1;
    }
}
