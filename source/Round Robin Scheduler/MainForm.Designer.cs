namespace SomeTechie.RoundRobinScheduler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTeams = new System.Windows.Forms.TabPage();
            this.lblTeamsLocked = new System.Windows.Forms.Label();
            this.panelDivisions = new System.Windows.Forms.TableLayoutPanel();
            this.grpCollege = new System.Windows.Forms.GroupBox();
            this.dtmCollege = new SomeTechie.RoundRobinScheduler.EditableItemsManager();
            this.grpRookie = new System.Windows.Forms.GroupBox();
            this.dtmRookie = new SomeTechie.RoundRobinScheduler.EditableItemsManager();
            this.grpPro = new System.Windows.Forms.GroupBox();
            this.dtmPro = new SomeTechie.RoundRobinScheduler.EditableItemsManager();
            this.tabPageScoreKeepers = new System.Windows.Forms.TabPage();
            this.serverTabControl = new System.Windows.Forms.TabControl();
            this.serverTabPageWiFi = new System.Windows.Forms.TabPage();
            this.lnkLaunchWebInterface = new System.Windows.Forms.LinkLabel();
            this.cmbWebInterfaceURLs = new System.Windows.Forms.ComboBox();
            this.lblWebServerStatusText = new System.Windows.Forms.Label();
            this.lblWebInterfaceURLs = new System.Windows.Forms.Label();
            this.lblWebServerStatusLabel = new System.Windows.Forms.Label();
            this.btnReloadWebInterfaceURLs = new System.Windows.Forms.Button();
            this.webInterfaceUrlQrCodePanel = new System.Windows.Forms.Panel();
            this.serverTabPage3G4G = new System.Windows.Forms.TabPage();
            this.btnStopProxy = new System.Windows.Forms.Button();
            this.btnStartProxy = new System.Windows.Forms.Button();
            this.txtProxySharedKey = new System.Windows.Forms.TextBox();
            this.lblProxySharedKey = new System.Windows.Forms.Label();
            this.imgProxyWaiting = new System.Windows.Forms.PictureBox();
            this.txtProxyUrl = new System.Windows.Forms.TextBox();
            this.lblProxyStatusLabel = new System.Windows.Forms.Label();
            this.lnkLaunchProxiedInterface = new System.Windows.Forms.LinkLabel();
            this.lblProxyStatusText = new System.Windows.Forms.Label();
            this.lblProxyUrl = new System.Windows.Forms.Label();
            this.proxyUrlQrCodePanel = new System.Windows.Forms.Panel();
            this.scorekeepersManager = new SomeTechie.RoundRobinScheduler.ScorekeepersManager();
            this.tabPageScoreKeepersSchedule = new System.Windows.Forms.TabPage();
            this.scoreKeepersScheduleDisplay1 = new SomeTechie.RoundRobinScheduler.ScoreKeepersScheduleDisplay();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.scheduleDisplay = new SomeTechie.RoundRobinScheduler.ScheduleDisplay();
            this.tabPageSeeding = new System.Windows.Forms.TabPage();
            this.seedingDisplay = new SomeTechie.RoundRobinScheduler.SeedingDisplay();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveErrorLogAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.lockTournamentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableScheduleEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.autoSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.launchWebInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseResumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webServerStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSaveFailedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentRoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveLogDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.tabControl.SuspendLayout();
            this.tabPageTeams.SuspendLayout();
            this.panelDivisions.SuspendLayout();
            this.grpCollege.SuspendLayout();
            this.grpRookie.SuspendLayout();
            this.grpPro.SuspendLayout();
            this.tabPageScoreKeepers.SuspendLayout();
            this.serverTabControl.SuspendLayout();
            this.serverTabPageWiFi.SuspendLayout();
            this.serverTabPage3G4G.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProxyWaiting)).BeginInit();
            this.tabPageScoreKeepersSchedule.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.tabPageSeeding.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageTeams);
            this.tabControl.Controls.Add(this.tabPageScoreKeepers);
            this.tabControl.Controls.Add(this.tabPageScoreKeepersSchedule);
            this.tabControl.Controls.Add(this.tabPageSchedule);
            this.tabControl.Controls.Add(this.tabPageSeeding);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 38);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1113, 462);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageTeams
            // 
            this.tabPageTeams.Controls.Add(this.lblTeamsLocked);
            this.tabPageTeams.Controls.Add(this.panelDivisions);
            this.tabPageTeams.Location = new System.Drawing.Point(4, 38);
            this.tabPageTeams.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageTeams.Name = "tabPageTeams";
            this.tabPageTeams.Size = new System.Drawing.Size(1105, 420);
            this.tabPageTeams.TabIndex = 1;
            this.tabPageTeams.Text = "Teams";
            this.tabPageTeams.UseVisualStyleBackColor = true;
            // 
            // lblTeamsLocked
            // 
            this.lblTeamsLocked.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTeamsLocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTeamsLocked.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTeamsLocked.Location = new System.Drawing.Point(0, 375);
            this.lblTeamsLocked.Name = "lblTeamsLocked";
            this.lblTeamsLocked.Size = new System.Drawing.Size(1105, 45);
            this.lblTeamsLocked.TabIndex = 4;
            this.lblTeamsLocked.Text = "Teams cannot be added, removed, or moved once the tournament has started.";
            this.lblTeamsLocked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTeamsLocked.Visible = false;
            // 
            // panelDivisions
            // 
            this.panelDivisions.ColumnCount = 3;
            this.panelDivisions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelDivisions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelDivisions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelDivisions.Controls.Add(this.grpCollege, 0, 0);
            this.panelDivisions.Controls.Add(this.grpRookie, 1, 0);
            this.panelDivisions.Controls.Add(this.grpPro, 0, 0);
            this.panelDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDivisions.Location = new System.Drawing.Point(0, 0);
            this.panelDivisions.Margin = new System.Windows.Forms.Padding(4);
            this.panelDivisions.Name = "panelDivisions";
            this.panelDivisions.RowCount = 1;
            this.panelDivisions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelDivisions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 439F));
            this.panelDivisions.Size = new System.Drawing.Size(1105, 420);
            this.panelDivisions.TabIndex = 3;
            // 
            // grpCollege
            // 
            this.grpCollege.Controls.Add(this.dtmCollege);
            this.grpCollege.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCollege.Location = new System.Drawing.Point(372, 4);
            this.grpCollege.Margin = new System.Windows.Forms.Padding(4);
            this.grpCollege.Name = "grpCollege";
            this.grpCollege.Padding = new System.Windows.Forms.Padding(4);
            this.grpCollege.Size = new System.Drawing.Size(360, 412);
            this.grpCollege.TabIndex = 4;
            this.grpCollege.TabStop = false;
            this.grpCollege.Text = "College";
            // 
            // dtmCollege
            // 
            this.dtmCollege.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtmCollege.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtmCollege.ItemsList = ((System.Collections.Generic.List<string>)(resources.GetObject("dtmCollege.ItemsList")));
            this.dtmCollege.ItemsText = "Team Names:";
            this.dtmCollege.ItemStringFormat = "C{0}";
            this.dtmCollege.Location = new System.Drawing.Point(4, 31);
            this.dtmCollege.LockRearrange = false;
            this.dtmCollege.Margin = new System.Windows.Forms.Padding(4);
            this.dtmCollege.Name = "dtmCollege";
            this.dtmCollege.NumItemsText = "Number of teams";
            this.dtmCollege.Size = new System.Drawing.Size(352, 377);
            this.dtmCollege.TabIndex = 1;
            // 
            // grpRookie
            // 
            this.grpRookie.Controls.Add(this.dtmRookie);
            this.grpRookie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRookie.Location = new System.Drawing.Point(740, 4);
            this.grpRookie.Margin = new System.Windows.Forms.Padding(4);
            this.grpRookie.Name = "grpRookie";
            this.grpRookie.Padding = new System.Windows.Forms.Padding(4);
            this.grpRookie.Size = new System.Drawing.Size(361, 412);
            this.grpRookie.TabIndex = 2;
            this.grpRookie.TabStop = false;
            this.grpRookie.Text = "Rookie";
            // 
            // dtmRookie
            // 
            this.dtmRookie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtmRookie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtmRookie.ItemsList = ((System.Collections.Generic.List<string>)(resources.GetObject("dtmRookie.ItemsList")));
            this.dtmRookie.ItemsText = "Team Names:";
            this.dtmRookie.ItemStringFormat = "R{0}";
            this.dtmRookie.Location = new System.Drawing.Point(4, 31);
            this.dtmRookie.LockRearrange = false;
            this.dtmRookie.Margin = new System.Windows.Forms.Padding(4);
            this.dtmRookie.Name = "dtmRookie";
            this.dtmRookie.NumItemsText = "Number of teams";
            this.dtmRookie.Size = new System.Drawing.Size(353, 377);
            this.dtmRookie.TabIndex = 2;
            // 
            // grpPro
            // 
            this.grpPro.Controls.Add(this.dtmPro);
            this.grpPro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPro.Location = new System.Drawing.Point(4, 4);
            this.grpPro.Margin = new System.Windows.Forms.Padding(4);
            this.grpPro.Name = "grpPro";
            this.grpPro.Padding = new System.Windows.Forms.Padding(4);
            this.grpPro.Size = new System.Drawing.Size(360, 412);
            this.grpPro.TabIndex = 1;
            this.grpPro.TabStop = false;
            this.grpPro.Text = "Pro";
            // 
            // dtmPro
            // 
            this.dtmPro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtmPro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtmPro.ItemsList = ((System.Collections.Generic.List<string>)(resources.GetObject("dtmPro.ItemsList")));
            this.dtmPro.ItemsText = "Team Names:";
            this.dtmPro.ItemStringFormat = "P{0}";
            this.dtmPro.Location = new System.Drawing.Point(4, 31);
            this.dtmPro.LockRearrange = false;
            this.dtmPro.Margin = new System.Windows.Forms.Padding(4);
            this.dtmPro.Name = "dtmPro";
            this.dtmPro.NumItemsText = "Number of teams";
            this.dtmPro.Size = new System.Drawing.Size(352, 377);
            this.dtmPro.TabIndex = 0;
            // 
            // tabPageScoreKeepers
            // 
            this.tabPageScoreKeepers.BackColor = System.Drawing.Color.White;
            this.tabPageScoreKeepers.Controls.Add(this.serverTabControl);
            this.tabPageScoreKeepers.Controls.Add(this.scorekeepersManager);
            this.tabPageScoreKeepers.Location = new System.Drawing.Point(4, 33);
            this.tabPageScoreKeepers.Name = "tabPageScoreKeepers";
            this.tabPageScoreKeepers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScoreKeepers.Size = new System.Drawing.Size(192, 63);
            this.tabPageScoreKeepers.TabIndex = 3;
            this.tabPageScoreKeepers.Text = "Score Keepers";
            // 
            // serverTabControl
            // 
            this.serverTabControl.Controls.Add(this.serverTabPageWiFi);
            this.serverTabControl.Controls.Add(this.serverTabPage3G4G);
            this.serverTabControl.Location = new System.Drawing.Point(631, 3);
            this.serverTabControl.Name = "serverTabControl";
            this.serverTabControl.SelectedIndex = 0;
            this.serverTabControl.Size = new System.Drawing.Size(468, 436);
            this.serverTabControl.TabIndex = 2;
            // 
            // serverTabPageWiFi
            // 
            this.serverTabPageWiFi.Controls.Add(this.lnkLaunchWebInterface);
            this.serverTabPageWiFi.Controls.Add(this.cmbWebInterfaceURLs);
            this.serverTabPageWiFi.Controls.Add(this.lblWebServerStatusText);
            this.serverTabPageWiFi.Controls.Add(this.lblWebInterfaceURLs);
            this.serverTabPageWiFi.Controls.Add(this.lblWebServerStatusLabel);
            this.serverTabPageWiFi.Controls.Add(this.btnReloadWebInterfaceURLs);
            this.serverTabPageWiFi.Controls.Add(this.webInterfaceUrlQrCodePanel);
            this.serverTabPageWiFi.Location = new System.Drawing.Point(4, 38);
            this.serverTabPageWiFi.Name = "serverTabPageWiFi";
            this.serverTabPageWiFi.Padding = new System.Windows.Forms.Padding(3);
            this.serverTabPageWiFi.Size = new System.Drawing.Size(460, 394);
            this.serverTabPageWiFi.TabIndex = 0;
            this.serverTabPageWiFi.Text = "WiFi";
            this.serverTabPageWiFi.UseVisualStyleBackColor = true;
            // 
            // lnkLaunchWebInterface
            // 
            this.lnkLaunchWebInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkLaunchWebInterface.AutoSize = true;
            this.lnkLaunchWebInterface.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lnkLaunchWebInterface.Location = new System.Drawing.Point(6, 198);
            this.lnkLaunchWebInterface.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lnkLaunchWebInterface.Name = "lnkLaunchWebInterface";
            this.lnkLaunchWebInterface.Size = new System.Drawing.Size(480, 54);
            this.lnkLaunchWebInterface.TabIndex = 15;
            this.lnkLaunchWebInterface.TabStop = true;
            this.lnkLaunchWebInterface.Text = "Launch Web Interface";
            this.lnkLaunchWebInterface.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLaunchWebInterface_LinkClicked);
            // 
            // cmbWebInterfaceURLs
            // 
            this.cmbWebInterfaceURLs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWebInterfaceURLs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWebInterfaceURLs.FormattingEnabled = true;
            this.cmbWebInterfaceURLs.Location = new System.Drawing.Point(67, 23);
            this.cmbWebInterfaceURLs.Name = "cmbWebInterfaceURLs";
            this.cmbWebInterfaceURLs.Size = new System.Drawing.Size(349, 37);
            this.cmbWebInterfaceURLs.TabIndex = 10;
            this.cmbWebInterfaceURLs.SelectedValueChanged += new System.EventHandler(this.cmbIPAddress_SelectedValueChanged);
            this.cmbWebInterfaceURLs.Click += new System.EventHandler(this.cmbIPAddress_SelectedValueChanged);
            // 
            // lblWebServerStatusText
            // 
            this.lblWebServerStatusText.AutoSize = true;
            this.lblWebServerStatusText.Location = new System.Drawing.Point(64, 3);
            this.lblWebServerStatusText.Name = "lblWebServerStatusText";
            this.lblWebServerStatusText.Size = new System.Drawing.Size(79, 29);
            this.lblWebServerStatusText.TabIndex = 14;
            this.lblWebServerStatusText.Text = "Status";
            // 
            // lblWebInterfaceURLs
            // 
            this.lblWebInterfaceURLs.AutoSize = true;
            this.lblWebInterfaceURLs.Location = new System.Drawing.Point(6, 26);
            this.lblWebInterfaceURLs.Name = "lblWebInterfaceURLs";
            this.lblWebInterfaceURLs.Size = new System.Drawing.Size(66, 29);
            this.lblWebInterfaceURLs.TabIndex = 9;
            this.lblWebInterfaceURLs.Text = "URL:";
            // 
            // lblWebServerStatusLabel
            // 
            this.lblWebServerStatusLabel.AutoSize = true;
            this.lblWebServerStatusLabel.Location = new System.Drawing.Point(6, 3);
            this.lblWebServerStatusLabel.Name = "lblWebServerStatusLabel";
            this.lblWebServerStatusLabel.Size = new System.Drawing.Size(85, 29);
            this.lblWebServerStatusLabel.TabIndex = 13;
            this.lblWebServerStatusLabel.Text = "Status:";
            // 
            // btnReloadWebInterfaceURLs
            // 
            this.btnReloadWebInterfaceURLs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadWebInterfaceURLs.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.Refresh;
            this.btnReloadWebInterfaceURLs.Location = new System.Drawing.Point(423, 19);
            this.btnReloadWebInterfaceURLs.Margin = new System.Windows.Forms.Padding(4);
            this.btnReloadWebInterfaceURLs.Name = "btnReloadWebInterfaceURLs";
            this.btnReloadWebInterfaceURLs.Size = new System.Drawing.Size(30, 30);
            this.btnReloadWebInterfaceURLs.TabIndex = 11;
            this.btnReloadWebInterfaceURLs.UseVisualStyleBackColor = true;
            this.btnReloadWebInterfaceURLs.Click += new System.EventHandler(this.btnReloadURLs_Click);
            // 
            // webInterfaceUrlQrCodePanel
            // 
            this.webInterfaceUrlQrCodePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webInterfaceUrlQrCodePanel.Location = new System.Drawing.Point(9, 57);
            this.webInterfaceUrlQrCodePanel.Name = "webInterfaceUrlQrCodePanel";
            this.webInterfaceUrlQrCodePanel.Size = new System.Drawing.Size(442, 138);
            this.webInterfaceUrlQrCodePanel.TabIndex = 12;
            this.webInterfaceUrlQrCodePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.webInterfaceUrlQrCodePanel_Paint);
            // 
            // serverTabPage3G4G
            // 
            this.serverTabPage3G4G.Controls.Add(this.btnStopProxy);
            this.serverTabPage3G4G.Controls.Add(this.btnStartProxy);
            this.serverTabPage3G4G.Controls.Add(this.txtProxySharedKey);
            this.serverTabPage3G4G.Controls.Add(this.lblProxySharedKey);
            this.serverTabPage3G4G.Controls.Add(this.imgProxyWaiting);
            this.serverTabPage3G4G.Controls.Add(this.txtProxyUrl);
            this.serverTabPage3G4G.Controls.Add(this.lblProxyStatusLabel);
            this.serverTabPage3G4G.Controls.Add(this.lnkLaunchProxiedInterface);
            this.serverTabPage3G4G.Controls.Add(this.lblProxyStatusText);
            this.serverTabPage3G4G.Controls.Add(this.lblProxyUrl);
            this.serverTabPage3G4G.Controls.Add(this.proxyUrlQrCodePanel);
            this.serverTabPage3G4G.Location = new System.Drawing.Point(4, 33);
            this.serverTabPage3G4G.Name = "serverTabPage3G4G";
            this.serverTabPage3G4G.Padding = new System.Windows.Forms.Padding(3);
            this.serverTabPage3G4G.Size = new System.Drawing.Size(460, 399);
            this.serverTabPage3G4G.TabIndex = 1;
            this.serverTabPage3G4G.Text = "3G/4G";
            this.serverTabPage3G4G.UseVisualStyleBackColor = true;
            // 
            // btnStopProxy
            // 
            this.btnStopProxy.Enabled = false;
            this.btnStopProxy.Location = new System.Drawing.Point(221, 81);
            this.btnStopProxy.Name = "btnStopProxy";
            this.btnStopProxy.Size = new System.Drawing.Size(75, 25);
            this.btnStopProxy.TabIndex = 28;
            this.btnStopProxy.Text = "Stop";
            this.btnStopProxy.UseVisualStyleBackColor = true;
            this.btnStopProxy.Click += new System.EventHandler(this.btnStopProxy_Click);
            // 
            // btnStartProxy
            // 
            this.btnStartProxy.Location = new System.Drawing.Point(140, 81);
            this.btnStartProxy.Name = "btnStartProxy";
            this.btnStartProxy.Size = new System.Drawing.Size(75, 25);
            this.btnStartProxy.TabIndex = 27;
            this.btnStartProxy.Text = "Start";
            this.btnStartProxy.UseVisualStyleBackColor = true;
            this.btnStartProxy.Click += new System.EventHandler(this.btnStartProxy_Click);
            // 
            // txtProxySharedKey
            // 
            this.txtProxySharedKey.Location = new System.Drawing.Point(140, 52);
            this.txtProxySharedKey.Name = "txtProxySharedKey";
            this.txtProxySharedKey.Size = new System.Drawing.Size(299, 34);
            this.txtProxySharedKey.TabIndex = 26;
            // 
            // lblProxySharedKey
            // 
            this.lblProxySharedKey.AutoSize = true;
            this.lblProxySharedKey.Location = new System.Drawing.Point(6, 55);
            this.lblProxySharedKey.Name = "lblProxySharedKey";
            this.lblProxySharedKey.Size = new System.Drawing.Size(173, 29);
            this.lblProxySharedKey.TabIndex = 25;
            this.lblProxySharedKey.Text = "Shared Secret:";
            // 
            // imgProxyWaiting
            // 
            this.imgProxyWaiting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgProxyWaiting.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.proxywaiting;
            this.imgProxyWaiting.InitialImage = global::SomeTechie.RoundRobinScheduler.Properties.Resources.proxywaiting;
            this.imgProxyWaiting.Location = new System.Drawing.Point(23, 135);
            this.imgProxyWaiting.Name = "imgProxyWaiting";
            this.imgProxyWaiting.Size = new System.Drawing.Size(66, 66);
            this.imgProxyWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgProxyWaiting.TabIndex = 0;
            this.imgProxyWaiting.TabStop = false;
            this.imgProxyWaiting.Visible = false;
            // 
            // txtProxyUrl
            // 
            this.txtProxyUrl.Location = new System.Drawing.Point(140, 23);
            this.txtProxyUrl.Name = "txtProxyUrl";
            this.txtProxyUrl.Size = new System.Drawing.Size(299, 34);
            this.txtProxyUrl.TabIndex = 23;
            // 
            // lblProxyStatusLabel
            // 
            this.lblProxyStatusLabel.AutoSize = true;
            this.lblProxyStatusLabel.Location = new System.Drawing.Point(6, 3);
            this.lblProxyStatusLabel.Name = "lblProxyStatusLabel";
            this.lblProxyStatusLabel.Size = new System.Drawing.Size(85, 29);
            this.lblProxyStatusLabel.TabIndex = 20;
            this.lblProxyStatusLabel.Text = "Status:";
            // 
            // lnkLaunchProxiedInterface
            // 
            this.lnkLaunchProxiedInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkLaunchProxiedInterface.AutoSize = true;
            this.lnkLaunchProxiedInterface.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lnkLaunchProxiedInterface.Location = new System.Drawing.Point(6, 311);
            this.lnkLaunchProxiedInterface.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lnkLaunchProxiedInterface.Name = "lnkLaunchProxiedInterface";
            this.lnkLaunchProxiedInterface.Size = new System.Drawing.Size(505, 54);
            this.lnkLaunchProxiedInterface.TabIndex = 22;
            this.lnkLaunchProxiedInterface.TabStop = true;
            this.lnkLaunchProxiedInterface.Text = "Launch Proxy Interface";
            this.lnkLaunchProxiedInterface.Visible = false;
            this.lnkLaunchProxiedInterface.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLaunchProxiedInterface_LinkClicked);
            // 
            // lblProxyStatusText
            // 
            this.lblProxyStatusText.AutoSize = true;
            this.lblProxyStatusText.ForeColor = System.Drawing.Color.Red;
            this.lblProxyStatusText.Location = new System.Drawing.Point(140, 3);
            this.lblProxyStatusText.Name = "lblProxyStatusText";
            this.lblProxyStatusText.Size = new System.Drawing.Size(105, 29);
            this.lblProxyStatusText.TabIndex = 21;
            this.lblProxyStatusText.Text = "Stopped";
            // 
            // lblProxyUrl
            // 
            this.lblProxyUrl.AutoSize = true;
            this.lblProxyUrl.Location = new System.Drawing.Point(6, 26);
            this.lblProxyUrl.Name = "lblProxyUrl";
            this.lblProxyUrl.Size = new System.Drawing.Size(66, 29);
            this.lblProxyUrl.TabIndex = 16;
            this.lblProxyUrl.Text = "URL:";
            // 
            // proxyUrlQrCodePanel
            // 
            this.proxyUrlQrCodePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyUrlQrCodePanel.Location = new System.Drawing.Point(9, 126);
            this.proxyUrlQrCodePanel.Name = "proxyUrlQrCodePanel";
            this.proxyUrlQrCodePanel.Size = new System.Drawing.Size(442, 182);
            this.proxyUrlQrCodePanel.TabIndex = 19;
            this.proxyUrlQrCodePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.proxyUrlQrCodePanel_Paint);
            // 
            // scorekeepersManager
            // 
            this.scorekeepersManager.Location = new System.Drawing.Point(3, 3);
            this.scorekeepersManager.Margin = new System.Windows.Forms.Padding(4);
            this.scorekeepersManager.Name = "scorekeepersManager";
            this.scorekeepersManager.Size = new System.Drawing.Size(621, 428);
            this.scorekeepersManager.TabIndex = 0;
            // 
            // tabPageScoreKeepersSchedule
            // 
            this.tabPageScoreKeepersSchedule.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageScoreKeepersSchedule.Controls.Add(this.scoreKeepersScheduleDisplay1);
            this.tabPageScoreKeepersSchedule.Location = new System.Drawing.Point(4, 33);
            this.tabPageScoreKeepersSchedule.Name = "tabPageScoreKeepersSchedule";
            this.tabPageScoreKeepersSchedule.Size = new System.Drawing.Size(192, 63);
            this.tabPageScoreKeepersSchedule.TabIndex = 4;
            this.tabPageScoreKeepersSchedule.Text = "Score Keepers Schedule";
            // 
            // scoreKeepersScheduleDisplay1
            // 
            this.scoreKeepersScheduleDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoreKeepersScheduleDisplay1.Location = new System.Drawing.Point(0, 0);
            this.scoreKeepersScheduleDisplay1.Margin = new System.Windows.Forms.Padding(0);
            this.scoreKeepersScheduleDisplay1.Name = "scoreKeepersScheduleDisplay1";
            this.scoreKeepersScheduleDisplay1.NumRounds = 1;
            this.scoreKeepersScheduleDisplay1.Size = new System.Drawing.Size(192, 63);
            this.scoreKeepersScheduleDisplay1.TabIndex = 0;
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSchedule.Controls.Add(this.scheduleDisplay);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 38);
            this.tabPageSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Size = new System.Drawing.Size(1105, 420);
            this.tabPageSchedule.TabIndex = 0;
            this.tabPageSchedule.Text = "Schedule";
            // 
            // scheduleDisplay
            // 
            this.scheduleDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.scheduleDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleDisplay.EnableEditing = false;
            this.scheduleDisplay.Location = new System.Drawing.Point(0, 0);
            this.scheduleDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.scheduleDisplay.Name = "scheduleDisplay";
            this.scheduleDisplay.Size = new System.Drawing.Size(1105, 420);
            this.scheduleDisplay.TabIndex = 2;
            // 
            // tabPageSeeding
            // 
            this.tabPageSeeding.BackColor = System.Drawing.Color.White;
            this.tabPageSeeding.Controls.Add(this.seedingDisplay);
            this.tabPageSeeding.Location = new System.Drawing.Point(4, 33);
            this.tabPageSeeding.Name = "tabPageSeeding";
            this.tabPageSeeding.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSeeding.Size = new System.Drawing.Size(192, 63);
            this.tabPageSeeding.TabIndex = 2;
            this.tabPageSeeding.Text = "Seeding";
            // 
            // seedingDisplay
            // 
            this.seedingDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seedingDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.seedingDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.seedingDisplay.Location = new System.Drawing.Point(3, 3);
            this.seedingDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.seedingDisplay.Name = "seedingDisplay";
            this.seedingDisplay.Size = new System.Drawing.Size(186, 57);
            this.seedingDisplay.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.webServerToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.webServerStatusToolStripMenuItem,
            this.proxyStatusToolStripMenuItem,
            this.autoSavingToolStripMenuItem,
            this.AutoSaveFailedToolStripMenuItem,
            this.currentRoundToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1113, 38);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.pageSetupToolStripMenuItem,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator8,
            this.newToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(62, 34);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(341, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(341, 6);
            // 
            // pageSetupToolStripMenuItem
            // 
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.pageSetupToolStripMenuItem.Text = "Page Setup...";
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.pageSetupToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.printToolStripMenuItem.Text = "&Print...";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.printPreviewToolStripMenuItem.Text = "Print Preview...";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(341, 6);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(344, 40);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolStripSeparator5,
            this.logToolStripMenuItem,
            this.toolStripSeparator6,
            this.lockTournamentToolStripMenuItem,
            this.unlockToolStripMenuItem,
            this.enableScheduleEditToolStripMenuItem,
            this.toolStripSeparator7,
            this.autoSaveToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(78, 34);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(321, 6);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearErrorLogToolStripMenuItem,
            this.saveErrorLogAsToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.logToolStripMenuItem.Text = "&Log";
            // 
            // clearErrorLogToolStripMenuItem
            // 
            this.clearErrorLogToolStripMenuItem.Name = "clearErrorLogToolStripMenuItem";
            this.clearErrorLogToolStripMenuItem.Size = new System.Drawing.Size(308, 40);
            this.clearErrorLogToolStripMenuItem.Text = "&Clear Error Log";
            this.clearErrorLogToolStripMenuItem.Click += new System.EventHandler(this.clearErrorLogToolStripMenuItem_Click);
            // 
            // saveErrorLogAsToolStripMenuItem
            // 
            this.saveErrorLogAsToolStripMenuItem.Name = "saveErrorLogAsToolStripMenuItem";
            this.saveErrorLogAsToolStripMenuItem.Size = new System.Drawing.Size(308, 40);
            this.saveErrorLogAsToolStripMenuItem.Text = "&Save Error Log As...";
            this.saveErrorLogAsToolStripMenuItem.Click += new System.EventHandler(this.saveErrorLogAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(321, 6);
            // 
            // lockTournamentToolStripMenuItem
            // 
            this.lockTournamentToolStripMenuItem.Name = "lockTournamentToolStripMenuItem";
            this.lockTournamentToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.lockTournamentToolStripMenuItem.Text = "&Lock tournament";
            this.lockTournamentToolStripMenuItem.Click += new System.EventHandler(this.lockTournamentToolStripMenuItem_Click);
            // 
            // unlockToolStripMenuItem
            // 
            this.unlockToolStripMenuItem.Name = "unlockToolStripMenuItem";
            this.unlockToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.unlockToolStripMenuItem.Text = "Unlock tournament";
            this.unlockToolStripMenuItem.Visible = false;
            this.unlockToolStripMenuItem.Click += new System.EventHandler(this.unlockToolStripMenuItem_Click);
            // 
            // enableScheduleEditToolStripMenuItem
            // 
            this.enableScheduleEditToolStripMenuItem.Name = "enableScheduleEditToolStripMenuItem";
            this.enableScheduleEditToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.enableScheduleEditToolStripMenuItem.Text = "Enable Schedule Edit";
            this.enableScheduleEditToolStripMenuItem.Click += new System.EventHandler(this.enableScheduleEditToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(321, 6);
            // 
            // autoSaveToolStripMenuItem
            // 
            this.autoSaveToolStripMenuItem.Checked = true;
            this.autoSaveToolStripMenuItem.CheckOnClick = true;
            this.autoSaveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSaveToolStripMenuItem.Name = global::SomeTechie.RoundRobinScheduler.Properties.Settings.Default.AutoSave;
            this.autoSaveToolStripMenuItem.Size = new System.Drawing.Size(324, 40);
            this.autoSaveToolStripMenuItem.Text = "Auto Save";
            // 
            // webServerToolStripMenuItem
            // 
            this.webServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.launchWebInterfaceToolStripMenuItem,
            this.toolStripSeparator3,
            this.restartToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.pauseResumeToolStripMenuItem});
            this.webServerToolStripMenuItem.Name = "webServerToolStripMenuItem";
            this.webServerToolStripMenuItem.Size = new System.Drawing.Size(136, 34);
            this.webServerToolStripMenuItem.Text = "Web &Server";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(331, 6);
            // 
            // launchWebInterfaceToolStripMenuItem
            // 
            this.launchWebInterfaceToolStripMenuItem.Name = "launchWebInterfaceToolStripMenuItem";
            this.launchWebInterfaceToolStripMenuItem.Size = new System.Drawing.Size(334, 40);
            this.launchWebInterfaceToolStripMenuItem.Text = "&Launch Web Interface";
            this.launchWebInterfaceToolStripMenuItem.Click += new System.EventHandler(this.launchWebInterfaceToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(331, 6);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(334, 40);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(334, 40);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(334, 40);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // pauseResumeToolStripMenuItem
            // 
            this.pauseResumeToolStripMenuItem.Name = "pauseResumeToolStripMenuItem";
            this.pauseResumeToolStripMenuItem.Size = new System.Drawing.Size(334, 40);
            this.pauseResumeToolStripMenuItem.Text = "Pause";
            this.pauseResumeToolStripMenuItem.Visible = false;
            this.pauseResumeToolStripMenuItem.Click += new System.EventHandler(this.pauseResumeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(74, 34);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(188, 40);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // webServerStatusToolStripMenuItem
            // 
            this.webServerStatusToolStripMenuItem.Name = "webServerStatusToolStripMenuItem";
            this.webServerStatusToolStripMenuItem.Size = new System.Drawing.Size(155, 34);
            this.webServerStatusToolStripMenuItem.Text = "Server Status:";
            this.webServerStatusToolStripMenuItem.Visible = false;
            // 
            // proxyStatusToolStripMenuItem
            // 
            this.proxyStatusToolStripMenuItem.Name = "proxyStatusToolStripMenuItem";
            this.proxyStatusToolStripMenuItem.Size = new System.Drawing.Size(149, 34);
            this.proxyStatusToolStripMenuItem.Text = "Proxy Status:";
            this.proxyStatusToolStripMenuItem.Visible = false;
            // 
            // autoSavingToolStripMenuItem
            // 
            this.autoSavingToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.autoSavingToolStripMenuItem.Name = "autoSavingToolStripMenuItem";
            this.autoSavingToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.autoSavingToolStripMenuItem.Text = "Auto Saving...";
            this.autoSavingToolStripMenuItem.Visible = false;
            // 
            // AutoSaveFailedToolStripMenuItem
            // 
            this.AutoSaveFailedToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.AutoSaveFailedToolStripMenuItem.Name = "AutoSaveFailedToolStripMenuItem";
            this.AutoSaveFailedToolStripMenuItem.Size = new System.Drawing.Size(184, 34);
            this.AutoSaveFailedToolStripMenuItem.Text = "Auto Save Failed";
            this.AutoSaveFailedToolStripMenuItem.Visible = false;
            // 
            // currentRoundToolStripMenuItem
            // 
            this.currentRoundToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.currentRoundToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.currentRoundToolStripMenuItem.Name = "currentRoundToolStripMenuItem";
            this.currentRoundToolStripMenuItem.Size = new System.Drawing.Size(96, 34);
            this.currentRoundToolStripMenuItem.Text = "Round:";
            this.currentRoundToolStripMenuItem.Visible = false;
            this.currentRoundToolStripMenuItem.Click += new System.EventHandler(this.currentRoundToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "3 on 3 Schedule files|*.3o3|All Files|*.*|XML Files|*.xml";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "3 on 3 Schedule files|*.3o3|All Files|*.*|XML Files|*.xml";
            // 
            // saveLogDialog
            // 
            this.saveLogDialog.FileName = "3 on 3 Scheduler Log.txt";
            this.saveLogDialog.Filter = "Text Files|*.txt|Log Files|*.log|All Files|*.*";
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.ShowIcon = false;
            this.printPreviewDialog.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1113, 500);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(725, 45);
            this.Name = "MainForm";
            this.Text = "Round Robin Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabPageTeams.ResumeLayout(false);
            this.panelDivisions.ResumeLayout(false);
            this.grpCollege.ResumeLayout(false);
            this.grpRookie.ResumeLayout(false);
            this.grpPro.ResumeLayout(false);
            this.tabPageScoreKeepers.ResumeLayout(false);
            this.serverTabControl.ResumeLayout(false);
            this.serverTabPageWiFi.ResumeLayout(false);
            this.serverTabPageWiFi.PerformLayout();
            this.serverTabPage3G4G.ResumeLayout(false);
            this.serverTabPage3G4G.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProxyWaiting)).EndInit();
            this.tabPageScoreKeepersSchedule.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageSeeding.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageTeams;
        private System.Windows.Forms.TableLayoutPanel panelDivisions;
        private System.Windows.Forms.GroupBox grpCollege;
        private System.Windows.Forms.GroupBox grpRookie;
        private System.Windows.Forms.GroupBox grpPro;
        private EditableItemsManager dtmPro;
        private EditableItemsManager dtmCollege;
        private EditableItemsManager dtmRookie;
        private ScheduleDisplay scheduleDisplay;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.SaveFileDialog saveLogDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.TabPage tabPageSeeding;
        private SeedingDisplay seedingDisplay;
        private System.Windows.Forms.TabPage tabPageScoreKeepers;
        private ScorekeepersManager scorekeepersManager;
        private System.Windows.Forms.TabPage tabPageScoreKeepersSchedule;
        private ScoreKeepersScheduleDisplay scoreKeepersScheduleDisplay1;
        private System.Windows.Forms.ComboBox cmbWebInterfaceURLs;
        private System.Windows.Forms.Label lblWebInterfaceURLs;
        private System.Windows.Forms.Button btnReloadWebInterfaceURLs;
        private System.Windows.Forms.Panel webInterfaceUrlQrCodePanel;
        private System.Windows.Forms.ToolStripMenuItem webServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchWebInterfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Label lblWebServerStatusText;
        private System.Windows.Forms.Label lblWebServerStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem webServerStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseResumeToolStripMenuItem;
        private System.Windows.Forms.LinkLabel lnkLaunchWebInterface;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearErrorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveErrorLogAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem autoSavingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockTournamentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Label lblTeamsLocked;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem autoSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoSaveFailedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.TabControl serverTabControl;
        private System.Windows.Forms.TabPage serverTabPageWiFi;
        private System.Windows.Forms.TabPage serverTabPage3G4G;
        private System.Windows.Forms.Label lblProxyStatusLabel;
        private System.Windows.Forms.LinkLabel lnkLaunchProxiedInterface;
        private System.Windows.Forms.Label lblProxyStatusText;
        private System.Windows.Forms.Label lblProxyUrl;
        private System.Windows.Forms.Panel proxyUrlQrCodePanel;
        private System.Windows.Forms.TextBox txtProxyUrl;
        private System.Windows.Forms.PictureBox imgProxyWaiting;
        private System.Windows.Forms.ToolStripMenuItem proxyStatusToolStripMenuItem;
        private System.Windows.Forms.TextBox txtProxySharedKey;
        private System.Windows.Forms.Label lblProxySharedKey;
        private System.Windows.Forms.Button btnStopProxy;
        private System.Windows.Forms.Button btnStartProxy;
        private System.Windows.Forms.ToolStripMenuItem currentRoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableScheduleEditToolStripMenuItem;

    }
}

