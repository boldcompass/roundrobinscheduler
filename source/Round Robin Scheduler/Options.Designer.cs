namespace SomeTechie.RoundRobinScheduler
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.accessCodeMessageGrp = new System.Windows.Forms.GroupBox();
            this.accessCodeMessageMakeBoldChk = new System.Windows.Forms.CheckBox();
            this.accessCodeStringFormatMessageLbl = new System.Windows.Forms.Label();
            this.accessCodeMessageLine2Txt = new System.Windows.Forms.TextBox();
            this.accessCodeMessageLine1Txt = new System.Windows.Forms.TextBox();
            this.accessCodeMessageLine2Lbl = new System.Windows.Forms.Label();
            this.accessCodeMessageLine1Lbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.oKBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.lblNumCourts = new System.Windows.Forms.Label();
            this.upDwnNumCourts = new System.Windows.Forms.NumericUpDown();
            this.accessCodeMessageGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDwnNumCourts)).BeginInit();
            this.SuspendLayout();
            // 
            // accessCodeMessageGrp
            // 
            this.accessCodeMessageGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeMessageMakeBoldChk);
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeStringFormatMessageLbl);
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeMessageLine2Txt);
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeMessageLine1Txt);
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeMessageLine2Lbl);
            this.accessCodeMessageGrp.Controls.Add(this.accessCodeMessageLine1Lbl);
            this.accessCodeMessageGrp.Location = new System.Drawing.Point(12, 12);
            this.accessCodeMessageGrp.Name = "accessCodeMessageGrp";
            this.accessCodeMessageGrp.Size = new System.Drawing.Size(285, 167);
            this.accessCodeMessageGrp.TabIndex = 0;
            this.accessCodeMessageGrp.TabStop = false;
            this.accessCodeMessageGrp.Text = "Access Code Message";
            // 
            // accessCodeMessageMakeBoldChk
            // 
            this.accessCodeMessageMakeBoldChk.AutoSize = true;
            this.accessCodeMessageMakeBoldChk.Checked = global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.accessCodeMessageMakeBold;
            this.accessCodeMessageMakeBoldChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.accessCodeMessageMakeBoldChk.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default, "accessCodeMessageMakeBold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.accessCodeMessageMakeBoldChk.Location = new System.Drawing.Point(10, 141);
            this.accessCodeMessageMakeBoldChk.Name = "accessCodeMessageMakeBoldChk";
            this.accessCodeMessageMakeBoldChk.Size = new System.Drawing.Size(140, 17);
            this.accessCodeMessageMakeBoldChk.TabIndex = 5;
            this.accessCodeMessageMakeBoldChk.Text = "Make access code bold";
            this.accessCodeMessageMakeBoldChk.UseVisualStyleBackColor = true;
            this.accessCodeMessageMakeBoldChk.CheckedChanged += new System.EventHandler(this.accessCodeMessageMakeBoldChk_CheckedChanged);
            // 
            // accessCodeStringFormatMessageLbl
            // 
            this.accessCodeStringFormatMessageLbl.AutoSize = true;
            this.accessCodeStringFormatMessageLbl.ForeColor = System.Drawing.SystemColors.GrayText;
            this.accessCodeStringFormatMessageLbl.Location = new System.Drawing.Point(6, 22);
            this.accessCodeStringFormatMessageLbl.Name = "accessCodeStringFormatMessageLbl";
            this.accessCodeStringFormatMessageLbl.Size = new System.Drawing.Size(271, 13);
            this.accessCodeStringFormatMessageLbl.TabIndex = 4;
            this.accessCodeStringFormatMessageLbl.Text = "{0} will be replaced with the scorekeepers access code.";
            // 
            // accessCodeMessageLine2Txt
            // 
            this.accessCodeMessageLine2Txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.accessCodeMessageLine2Txt.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default, "AccessCodeMessageLine2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.accessCodeMessageLine2Txt.Location = new System.Drawing.Point(6, 109);
            this.accessCodeMessageLine2Txt.Name = "accessCodeMessageLine2Txt";
            this.accessCodeMessageLine2Txt.Size = new System.Drawing.Size(273, 20);
            this.accessCodeMessageLine2Txt.TabIndex = 3;
            this.accessCodeMessageLine2Txt.Text = global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.AccessCodeMessageLine2;
            this.accessCodeMessageLine2Txt.TextChanged += new System.EventHandler(this.accessCodeMessageLine2Txt_TextChanged);
            // 
            // accessCodeMessageLine1Txt
            // 
            this.accessCodeMessageLine1Txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.accessCodeMessageLine1Txt.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default, "AccessCodeMessageLine1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.accessCodeMessageLine1Txt.Location = new System.Drawing.Point(6, 59);
            this.accessCodeMessageLine1Txt.Name = "accessCodeMessageLine1Txt";
            this.accessCodeMessageLine1Txt.Size = new System.Drawing.Size(273, 20);
            this.accessCodeMessageLine1Txt.TabIndex = 2;
            this.accessCodeMessageLine1Txt.Text = global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.AccessCodeMessageLine1;
            this.accessCodeMessageLine1Txt.TextChanged += new System.EventHandler(this.accessCodeMessageLine1Txt_TextChanged);
            // 
            // accessCodeMessageLine2Lbl
            // 
            this.accessCodeMessageLine2Lbl.AutoSize = true;
            this.accessCodeMessageLine2Lbl.Location = new System.Drawing.Point(6, 93);
            this.accessCodeMessageLine2Lbl.Name = "accessCodeMessageLine2Lbl";
            this.accessCodeMessageLine2Lbl.Size = new System.Drawing.Size(39, 13);
            this.accessCodeMessageLine2Lbl.TabIndex = 1;
            this.accessCodeMessageLine2Lbl.Text = "Line 2:";
            // 
            // accessCodeMessageLine1Lbl
            // 
            this.accessCodeMessageLine1Lbl.AutoSize = true;
            this.accessCodeMessageLine1Lbl.Location = new System.Drawing.Point(6, 43);
            this.accessCodeMessageLine1Lbl.Name = "accessCodeMessageLine1Lbl";
            this.accessCodeMessageLine1Lbl.Size = new System.Drawing.Size(39, 13);
            this.accessCodeMessageLine1Lbl.TabIndex = 0;
            this.accessCodeMessageLine1Lbl.Text = "Line 1:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(141, 221);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // oKBtn
            // 
            this.oKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.oKBtn.Location = new System.Drawing.Point(60, 221);
            this.oKBtn.Name = "oKBtn";
            this.oKBtn.Size = new System.Drawing.Size(75, 23);
            this.oKBtn.TabIndex = 2;
            this.oKBtn.Text = "&OK";
            this.oKBtn.UseVisualStyleBackColor = true;
            this.oKBtn.Click += new System.EventHandler(this.oKBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBtn.Location = new System.Drawing.Point(222, 221);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 4;
            this.applyBtn.Text = "&Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // lblNumCourts
            // 
            this.lblNumCourts.AutoSize = true;
            this.lblNumCourts.Location = new System.Drawing.Point(22, 195);
            this.lblNumCourts.Name = "lblNumCourts";
            this.lblNumCourts.Size = new System.Drawing.Size(65, 13);
            this.lblNumCourts.TabIndex = 5;
            this.lblNumCourts.Text = "Num Courts:";
            // 
            // upDwnNumCourts
            // 
            this.upDwnNumCourts.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default, "numCourts", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.upDwnNumCourts.Location = new System.Drawing.Point(93, 192);
            this.upDwnNumCourts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDwnNumCourts.Name = "upDwnNumCourts";
            this.upDwnNumCourts.Size = new System.Drawing.Size(58, 20);
            this.upDwnNumCourts.TabIndex = 6;
            this.upDwnNumCourts.Value = global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.numCourts;
            // 
            // Options
            // 
            this.AcceptButton = this.oKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(309, 256);
            this.Controls.Add(this.upDwnNumCourts);
            this.Controls.Add(this.lblNumCourts);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.oKBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.accessCodeMessageGrp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(325, 265);
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.accessCodeMessageGrp.ResumeLayout(false);
            this.accessCodeMessageGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDwnNumCourts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox accessCodeMessageGrp;
        private System.Windows.Forms.Label accessCodeMessageLine2Lbl;
        private System.Windows.Forms.Label accessCodeMessageLine1Lbl;
        private System.Windows.Forms.TextBox accessCodeMessageLine2Txt;
        private System.Windows.Forms.TextBox accessCodeMessageLine1Txt;
        private System.Windows.Forms.CheckBox accessCodeMessageMakeBoldChk;
        private System.Windows.Forms.Label accessCodeStringFormatMessageLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button oKBtn;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Label lblNumCourts;
        private System.Windows.Forms.NumericUpDown upDwnNumCourts;
    }
}