using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SomeTechie.RoundRobinScheduler.Properties;
using SomeTechie.RoundRobinScheduleGenerator;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;
namespace SomeTechie.RoundRobinScheduler
{
    public partial class MainForm : Form
    {
        private WebServer.TournamentWebServer webServer = new WebServer.TournamentWebServer();
        private WebServer.TournamentProxy proxy = null;
        private string[] args = Environment.GetCommandLineArgs();
        private List<EditableItemsManager> _divisionTeamsManagers = new List<EditableItemsManager>();
        TournamentPrintDocument document = new TournamentPrintDocument();
        private Controller Controller = Controller.GetController();
        private const double autoSaveDelay = 5000;
        private DateTime lastAutoSaveCopyTime = DateTime.MinValue;
        private string autoSaveCopyPath = Path.Combine(Directory.GetCurrentDirectory(), "AutoSaveCopies");
        private bool teamsRearrangeIsLocked = false;
        private bool isDirty = false;
        private bool loadingDefaultData = false;
        private string tournamentName = "Untitled Tournament";
        protected Tournament Tournament
        {
            get
            {
                return Controller.Tournament;
            }
            set
            {
                Controller.Tournament = value;
            }
        }
        private string _filePath;

        AsyncCallback PrintInBackgroundCompleteDelegate;

        public MainForm()
        {
            PrintInBackgroundCompleteDelegate = new AsyncCallback(PrintInBackgroundComplete);
            InitializeComponent();
            Controller.TournamentChanged += new EventHandler(Controller_TournamentChanged);
            Controller.GameResultChanged += new GameResultChangedEventHandler(Controller_GameResultChanged);
            Controller.ScoreKeeperNameChanged += new ScoreKeeperNameChangedEventHandler(Controller_ScoreKeeperNameChanged);
            Controller.ScoreKeepersAssignmentChanged += new EventHandler(Controller_ScoreKeepersAssignmentChanged);
            Controller.ScoreKeepersChanged += new EventHandler(Controller_ScoreKeepersChanged);
            Controller.TeamNameChanged += new TeamNameChangedEventHandler(Controller_TeamNameChanged);
            Controller.CourtRoundsIsActiveChanged += new EventHandler(Controller_CourtRoundsIsActiveChanged);
            Controller.CourtRoundsIsInProgressChanged += new EventHandler(Controller_CourtRoundsIsInProgressChanged);
            webServer.StateChanged += new EventHandler(webServer_StateChanged);
            autoSaveTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoSaveTimer_Elapsed);
        }

        void Controller_CourtRoundsIsInProgressChanged(object sender, EventArgs e)
        {
            this.refreshCurrentRoundDisplay();
        }

        void Controller_CourtRoundsIsActiveChanged(object sender, EventArgs e)
        {
            this.refreshCurrentRoundDisplay();
        }

        void refreshCurrentRoundDisplay()
        {
            bool visibility = false;
            if (Tournament != null)
            {
                if (Tournament.IsCompleted)
                {
                    currentRoundToolStripMenuItem.Text = Resources.TournamentComplete;
                    visibility = true;
                }
                else if (Tournament.IsInProgress)
                {
                    CourtRound activeCourtRound = Tournament.ActiveCourtRound;
                    if (activeCourtRound != null)
                    {
                        currentRoundToolStripMenuItem.Text = String.Format(Resources.CurrentRound, activeCourtRound.RoundNumber);
                        visibility = true;
                    }
                } 
            }

            currentRoundToolStripMenuItem.Visible = visibility;
        }

        void Controller_TeamNameChanged(object sender, TeamNameChangedEventArgs e)
        {
            this.handleDataChanged();
        }

        void Controller_ScoreKeepersChanged(object sender, EventArgs e)
        {
            this.handleDataChanged();
        }

        void Controller_ScoreKeepersAssignmentChanged(object sender, EventArgs e)
        {
            this.handleDataChanged();
        }

        void Controller_ScoreKeeperNameChanged(object sender, ScoreKeeperNameChangedEventArgs e)
        {
            this.handleDataChanged();
        }

        void Controller_GameResultChanged(object sender, GameResultChangedEventArgs e)
        {
            this.handleDataChanged();
            if (!this.teamsRearrangeIsLocked) this.lockTeamsRearrange();
        }

        void Controller_TournamentChanged(object sender, EventArgs e)
        {
            document.Tournament = Tournament;
            if (Tournament.IsInProgress)
            {
                lockTeamsRearrange();
                tabControl.SelectTab(tabPageSchedule);
            }
            else if (Tournament.IsCompleted)
            {
                lockTeamsRearrange();
                tabControl.SelectTab(tabPageSeeding);
            }
            else
            {
                unlockTeamsRearrange();
                tabControl.SelectTab(tabPageTeams);
            }
            this.refreshCurrentRoundDisplay();
            this.handleDataChanged();
        }

        private System.Timers.Timer autoSaveTimer = new System.Timers.Timer(autoSaveDelay);
        private void handleDataChanged()
        {
            if (loadingDefaultData)
            {
                // Loading the default data shouldn't set the dirty flag
                loadingDefaultData = false;
                return;
            }

            isDirty = true;
            if (autoSaveToolStripMenuItem.Checked)
            {
                if (!autoSaveTimer.Enabled) autoSaveTimer.Start();
            }
        }

        private void autoSaveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            autoSaveTimer.Stop();
            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(autoSave);
            System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            thread.Start();
        }
        private void autoSave()
        {
            try
            {
                if (_filePath != null)
                {
                    this.autoSavingToolStripMenuItem.Visible = true;
                    if (DateTime.Now.Subtract(lastAutoSaveCopyTime).TotalMinutes >= 1)
                    {
                        try
                        {

                            if (!Directory.Exists(autoSaveCopyPath)) Directory.CreateDirectory(autoSaveCopyPath);
                            //Save a copy of this file
                            File.Copy(_filePath, Path.Combine(autoSaveCopyPath, new FileInfo(_filePath).Name) + "." + DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss_tt") + ".3o3");
                            lastAutoSaveCopyTime = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            Program.logException(ex, Resources.AutoSaveCopyFailed);
                        }
                    }
                    save(false, true);
                    System.Threading.Thread.Sleep(500);
                    this.autoSavingToolStripMenuItem.Visible = false;
                }
                AutoSaveFailedToolStripMenuItem.Visible = false;
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.AutoSaveFailed);
                AutoSaveFailedToolStripMenuItem.Visible = true;
            }
        }

        private string[] findAutoSaveCopies()
        {
            List<string> autoSaveCopies = new List<string>();
            foreach (string file in Directory.GetFiles(autoSaveCopyPath, ".3o3", SearchOption.TopDirectoryOnly))
            {
                autoSaveCopies.Add(file);
            }
            return autoSaveCopies.ToArray();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _divisionTeamsManagers.Add(dtmPro);
            _divisionTeamsManagers.Add(dtmCollege);
            _divisionTeamsManagers.Add(dtmRookie);

            foreach (EditableItemsManager divisionTeamManager in _divisionTeamsManagers)
            {

                divisionTeamManager.ItemsRemoved += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.dtm_TeamRemoved);
                divisionTeamManager.ItemsAdded += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.dtm_TeamAdded);
                divisionTeamManager.ItemChanged += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.dtm_TeamChanged);
                divisionTeamManager.ItemMoved += new SomeTechie.RoundRobinScheduler.ItemMovedEventHandler(this.dtm_TeamMoved);
            }

            //Load file
            string loadFilePath = null;
            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length >= 1)
            {
                loadFilePath = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                loadFilePath = new Uri(loadFilePath).LocalPath;
            }
            else if (args.Length >= 2)
            {
                loadFilePath = args[1];
            }
            if (loadFilePath != null)
            {
                try
                {
                    if (File.Exists(loadFilePath))
                    {
                        loadFile(loadFilePath);
                    }
                }
                catch (Exception ex)
                {
                    Program.logException(ex, Resources.TournamentLoadFailedTitle);
                    loadDefaultData();
                    MessageBox.Show(Resources.TournamentLoadFailedMessage, Resources.TournamentLoadFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //Use default data
            else
            {
                loadDefaultData();
            }

#if AUTOSERVER
            startWebServer();
#else
            updateWebServerStatus();
#endif
            //Preload proxy fields
            txtProxyUrl.Text = Settings.Default.ProxyUrl;
            txtProxySharedKey.Text = Settings.Default.ProxySharedKey;

            refreshSizing();
        }

        private void loadDefaultData()
        {
            loadingDefaultData = true;
            Controller.NumCourts = (int)Settings.Default.numCourts;
            List<Division> Divisions = new List<Division>();
            Divisions.Add(new Division("Pro", "P"));
            Divisions.Add(new Division("College", "C"));
            Divisions.Add(new Division("Rookie", "R"));
            for (int i = 0; i < Divisions.Count && i < _divisionTeamsManagers.Count; i++)
            {
                Divisions[i].Teams = TeamNamesToTeams(_divisionTeamsManagers[i].ItemsList.ToArray());
            }
            Tournament = new Tournament(Divisions, Controller.NumCourts);
        }

        private List<Team> TeamNamesToTeams(string[] teamNames)
        {
            List<Team> Teams = new List<Team>();
            for (int i = 0; i < teamNames.Length; i++)
            {
                Teams.Add(new Team(teamNames[i], i + 1));
            }
            return Teams;
        }

        private void dtm_TeamAdded(object sender, ItemsChangedEventArgs e)
        {
            if (!(sender is EditableItemsManager)) return;
            int divisionIndex = _divisionTeamsManagers.IndexOf((EditableItemsManager)sender);
            if (divisionIndex < 0 || divisionIndex >= Tournament.Divisions.Count) return;

            if (!e.IsComputerGenerated) setDivisionTeams(Tournament.Divisions[divisionIndex], _divisionTeamsManagers[divisionIndex].ItemsList.ToArray());
        }

        private void dtm_TeamRemoved(object sender, ItemsChangedEventArgs e)
        {
            if (!(sender is EditableItemsManager)) return;
            int divisionIndex = _divisionTeamsManagers.IndexOf((EditableItemsManager)sender);
            if (divisionIndex < 0 || divisionIndex >= Tournament.Divisions.Count) return;

            if (!e.IsComputerGenerated) setDivisionTeams(Tournament.Divisions[divisionIndex], _divisionTeamsManagers[divisionIndex].ItemsList.ToArray());
        }

        private void dtm_TeamChanged(object sender, ItemsChangedEventArgs e)
        {
            if (!(sender is EditableItemsManager)) return;
            int divisionIndex = _divisionTeamsManagers.IndexOf((EditableItemsManager)sender);
            if (divisionIndex < 0 || divisionIndex >= Tournament.Divisions.Count) return;

            foreach (int index in e.Indices)
            {
                Tournament.Divisions[divisionIndex].Teams[index].Name = e.Items[e.Indices.IndexOf(index)].ToString();
            }
        }

        private void dtm_TeamMoved(object sender, ItemMovedEventArgs e)
        {
            if (!(sender is EditableItemsManager)) return;
            int divisionIndex = _divisionTeamsManagers.IndexOf((EditableItemsManager)sender);
            if (divisionIndex < 0 || divisionIndex >= Tournament.Divisions.Count) return;

            setDivisionTeams(Tournament.Divisions[divisionIndex], _divisionTeamsManagers[divisionIndex].ItemsList.ToArray());
        }

        private void setDivisionTeams(Division division, string[] teamNames)
        {
            division.Teams = TeamNamesToTeams(teamNames);
            Tournament = new Tournament(Tournament.Divisions, Controller.NumCourts);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().Show();
        }

        /*
         * Begin open files
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!handleDataClear()) return;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
#if !DEBUG
                try
                {
#endif
                loadFile(openFileDialog.FileName);
                Controller.NumCourts = Tournament.NumCourts;
                if (Tournament.ScoreKeepers != null) Controller.ScoreKeepers = new List<ScoreKeeper>(Tournament.ScoreKeepers);
                else Controller.ScoreKeepers = null;
                Controller.ScoreKeepersAssignment = Tournament.ScoreKeepersAssignment;
#if !DEBUG
                }
                catch (Exception ex)
                {
                    Program.logException(ex, Resources.TournamentLoadFailedTitle);
                    MessageBox.Show(Resources.TournamentLoadFailedMessage, Resources.TournamentLoadFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#endif
            }
        }

        private void loadFile(string loadPath)
        {
            _filePath = loadPath;

            Tournament tournament = tournamentFromFile(loadPath);
            if (!String.IsNullOrEmpty(Tournament.Name)) tournamentName = tournament.Name;
            else
            {
                tournamentName = Path.GetFileNameWithoutExtension(_filePath);
                tournament.Name = tournamentName;
            }
            Tournament = tournament;

            for (int i = 0; i < Tournament.Divisions.Count && i < _divisionTeamsManagers.Count; i++)
            {
                Division division = Tournament.Divisions[i];
                EditableItemsManager divisionTeamsManager = _divisionTeamsManagers[i];
                List<string> Teams = new List<string>();
                for (int j = 0; j < division.Teams.Count; j++)
                {
                    Team team = division.Teams[j];
                    Teams.Add(team.Name);
                }
                divisionTeamsManager.ItemsList = Teams;
            }
        }

        private Tournament tournamentFromFile(string loadPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tournament));

            XmlReader reader = XmlReader.Create(loadPath);
            Tournament tournament = (Tournament)serializer.Deserialize(reader);
            reader.Close();

            return tournament;
        }
        /*
         * End open files
         */

        /*
         * Begin save files
         */
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private bool save(bool forceDialog = false, bool preventDialog = false)
        {
            if (forceDialog || _filePath == null)
            {
                if (preventDialog) return false;

                //Set dialog file name
                if (String.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    if (_filePath != null) saveFileDialog.FileName = _filePath;
                    else saveFileDialog.FileName = tournamentName;
                }

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = saveFileDialog.FileName;
                    tournamentName = Path.GetFileNameWithoutExtension(_filePath);
                    Tournament.Name = tournamentName;
                }
                else
                {
                    return false;
                }
            }
            return saveFile(_filePath);
        }

        private bool saveFile(string savePath)
        {
#if !DEBUG
            try
            {
#endif
            tournamentToFile(Tournament, savePath);
            isDirty = false;
            return true;
#if !DEBUG
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.TournamentSaveFailedTitle);
                MessageBox.Show(Resources.TournamentSaveFailedMessage, Resources.TournamentSaveFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
#endif
        }

        private void tournamentToFile(Tournament tournament, string savePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tournament));
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = false;

            XmlWriter writer = XmlTextWriter.Create(savePath, xmlWriterSettings);
            serializer.Serialize(writer, tournament);
            writer.Close();
        }

        private DialogResult askShouldSaveChanges()
        {
            return MessageBox.Show(String.Format(Resources.SaveChangesMessage, tournamentName), Resources.SaveChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
        /*
         * End save files
         */

        private void clearErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.ClearErrorLogMessage, Properties.Resources.ClearErrorLogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Program.getLogExists())
                {
                    try
                    {
                        Program.clearLog();
                        MessageBox.Show(Properties.Resources.ErrorLogClearedMessage, Properties.Resources.ErrorLogClearedTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Program.logException(ex, Properties.Resources.ErrorLogClearFailedTitle);
                        MessageBox.Show(Properties.Resources.ErrorLogClearFailedMessage, Properties.Resources.ErrorLogClearFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show(Properties.Resources.NoErrorLogMessage, Properties.Resources.NoErrorLogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveErrorLogAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.getLogExists())
            {
                MessageBox.Show(Resources.NoErrorLogMessage, Resources.NoErrorLogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (saveLogDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (Program.getLogExists())
                            Program.copyLogTo(saveLogDialog.FileName);
                        else
                            MessageBox.Show(Resources.NoErrorLogMessage, Resources.NoErrorLogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Program.logException(ex, Resources.ErrorLogCopyFailedTitle);
                        MessageBox.Show(Resources.ErrorLogCopyFailedTitle, Resources.ErrorLogCopyFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog.Document = document;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                //Disable printing-related menu-items
                printToolStripMenuItem.Enabled = false;
                printPreviewToolStripMenuItem.Enabled = false;
                pageSetupToolStripMenuItem.Enabled = false;

                //Print the document   
                System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(PrintInBackground);
                threadStart.BeginInvoke(new AsyncCallback(PrintInBackgroundComplete), null);
                //System.Threading.Thread thread = new System.Threading.Thread(threadStart);
                //thread.Start();
            }

        }

        private void PrintInBackground()
        {
            try
            {
                document.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorPrintingScheduleMessage, Resources.ErrorPrintingScheduleTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Program.logException(ex, Resources.ErrorPrintingScheduleTitle);
            }
        }

        private void PrintInBackgroundComplete(IAsyncResult r)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(PrintInBackgroundCompleteDelegate, r);
                return;
            }

            printToolStripMenuItem.Enabled = true;
            printPreviewToolStripMenuItem.Enabled = true;
            pageSetupToolStripMenuItem.Enabled = true;
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = document;
            printPreviewDialog.ShowDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog.Document = document;
            pageSetupDialog.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                for (int i = 0; i < _divisionTeamsManagers.Count; i++)
                {
                    if (_divisionTeamsManagers[i] == ActiveControl)
                    {
                        if (_divisionTeamsManagers.Count > i + 1)
                        {
                            _divisionTeamsManagers[i + 1].Focus();
                            return true;
                        }
                        else
                        {
                            tabControl.Focus();
                            return true;
                        }
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void startStopWebServerBtn_Click(object sender, EventArgs e)
        {
            startWebServer();
        }

        private void startWebServer()
        {
            webServer.Start();
            reloadURLs();
        }

        private void reloadURLs()
        {
            System.Net.IPAddress oldIP = null;
            if (cmbWebInterfaceURLs.SelectedItem != null) oldIP = ((NamedData<System.Net.IPAddress>)cmbWebInterfaceURLs.SelectedItem).Data;

            System.Net.IPAddress[] iPAddressses = webServer.GetAccessibleIPs();
            cmbWebInterfaceURLs.Items.Clear();
            foreach (System.Net.IPAddress iPAddress in iPAddressses)
            {
                string url = String.Format(webServer.ListenPort == 80 ? "http://{0}/" : "http://{0}:{1}/", iPAddress, webServer.ListenPort);
                cmbWebInterfaceURLs.Items.Add(new NamedData<System.Net.IPAddress>(url, iPAddress));
            }
            if (oldIP != null && cmbWebInterfaceURLs.Items.Contains(oldIP)) cmbWebInterfaceURLs.SelectedItem = oldIP;
            else if (cmbWebInterfaceURLs.Items.Count > 0) cmbWebInterfaceURLs.SelectedIndex = 0;
        }

        sbyte[][] webInterfaceUrlQrData;
        sbyte[][] proxiedInterfaceUrlQrData;
        string webUrl;
        private void paintQrCode(sbyte[][] data, Graphics g, Size size)
        {
            paintQrCode(data, g, size.Width, size.Height);
        }
        private void paintQrCode(sbyte[][] data, Graphics g, int width, int height)
        {
            g.Clear(Color.White);

            if (data != null)
            {
                int numUnits = data.Length;
                if (numUnits > 0)
                {
                    int unitSize = Math.Min(width / numUnits, height / numUnits);
                    Point offset = new Point((width - (numUnits * unitSize)) / 2, (height - (numUnits * unitSize)) / 2);
                    for (int y = 0; y <= data.Length - 1; y++)
                    {
                        int drawY = y * unitSize + offset.Y;
                        for (int x = 0; x <= data[y].Length - 1; x++)
                        {
                            int drawX = x * unitSize + offset.X;
                            Brush brush = (data[y][x] == 0 ? Brushes.Black : Brushes.White);
                            g.FillRectangle(brush, drawX, drawY, unitSize, unitSize);
                        }
                    }
                }
            }
        }

        private void cmbIPAddress_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbWebInterfaceURLs.SelectedItem != null)
            {
                string url = ((NamedData<System.Net.IPAddress>)cmbWebInterfaceURLs.SelectedItem).Name;

                com.google.zxing.qrcode.QRCodeWriter writer = new com.google.zxing.qrcode.QRCodeWriter();
                webInterfaceUrlQrData = writer.encode(url, com.google.zxing.BarcodeFormat.QR_CODE, 0, 0).Array;

                if (url != webUrl && !webInterfaceUrlQrCodePanel.IsDisposed)
                {
                    paintQrCode(webInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);
                    webUrl = url;
                }
                lnkLaunchWebInterface.Text = url;
                refreshSizing();
            }
            else
            {
                webInterfaceUrlQrData = new ByteMatrix(0, 0).Array;
                paintQrCode(webInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);
            }
        }

        private void webInterfaceUrlQrCodePanel_Paint(object sender, PaintEventArgs e)
        {
            paintQrCode(webInterfaceUrlQrData, e.Graphics, webInterfaceUrlQrCodePanel.Size);
        }

        private void btnReloadURLs_Click(object sender, EventArgs e)
        {
            reloadURLs();
        }

        private void refreshSizing()
        {
            //Scorekeepers
            Size scorekeeperAreaSize = tabPageScoreKeepers.Size;
            int scorekeeperManagerWidth = (int)Math.Max(Math.Min(scorekeeperAreaSize.Width * .75, 150), Math.Min(scorekeeperAreaSize.Width * .55, 500));
            int scorekeeperWebServerWidth = scorekeeperAreaSize.Width - scorekeeperManagerWidth;

            scorekeepersManager.Width = scorekeeperManagerWidth - scorekeepersManager.Margin.Left - scorekeepersManager.Margin.Right;
            scorekeepersManager.Height = scorekeeperAreaSize.Height - scorekeepersManager.Margin.Top - scorekeepersManager.Margin.Bottom;

            serverTabControl.Width = scorekeeperWebServerWidth - scorekeepersManager.Margin.Left - scorekeepersManager.Margin.Right;
            serverTabControl.Height = scorekeeperAreaSize.Height - serverTabControl.Margin.Top - serverTabControl.Margin.Bottom;
            serverTabControl.Left = scorekeeperManagerWidth + serverTabControl.Margin.Left;

            //Web Server
            //Resize launch web interface link
            lnkLaunchWebInterface.Left = 0;
            float launchWebInterfaceMaxSize = (float)Math.Min(Math.Max(serverTabPageWiFi.Height * .1, 5), 100);
            while (lnkLaunchWebInterface.Right < serverTabPageWiFi.Width - lnkLaunchWebInterface.Margin.Right)
            {
                setLnkLaunchWebInterfaceFontSize(lnkLaunchWebInterface.Font.Size + .5F);
                if (lnkLaunchWebInterface.Font.Size >= launchWebInterfaceMaxSize) break;
            }
            while (lnkLaunchWebInterface.Right > serverTabPageWiFi.Width - lnkLaunchWebInterface.Margin.Right)
            {
                setLnkLaunchWebInterfaceFontSize(lnkLaunchWebInterface.Font.Size - .5F);
                if (lnkLaunchWebInterface.Font.Size <= 5) break;
            }
            lnkLaunchWebInterface.Top = serverTabPageWiFi.Height - lnkLaunchWebInterface.Height - lnkLaunchWebInterface.Margin.Bottom;
            lnkLaunchWebInterface.Left = (serverTabPageWiFi.Width - lnkLaunchWebInterface.Width) / 2;
            //Resize launch web interface QR Code
            webInterfaceUrlQrCodePanel.Height = lnkLaunchWebInterface.Top - lnkLaunchWebInterface.Margin.Top - webInterfaceUrlQrCodePanel.Top;

            //Proxy
            //Resize launch proxy link
            float launchProxyMaxSize = (float)Math.Min(Math.Max(serverTabPage3G4G.Height * .1, 5), 100);
            lnkLaunchProxiedInterface.Left = 0;
            while (lnkLaunchProxiedInterface.Right < serverTabPage3G4G.Width - lnkLaunchProxiedInterface.Margin.Right)
            {
                setLnkLaunchProxiedInterfaceFontSize(lnkLaunchProxiedInterface.Font.Size + .5F);
                if (lnkLaunchProxiedInterface.Font.Size >= launchWebInterfaceMaxSize) break;
            }
            while (lnkLaunchProxiedInterface.Right > serverTabPage3G4G.Width - lnkLaunchProxiedInterface.Margin.Right)
            {
                setLnkLaunchProxiedInterfaceFontSize(lnkLaunchProxiedInterface.Font.Size - .5F);
                if (lnkLaunchProxiedInterface.Font.Size <= 5) break;
            }
            lnkLaunchProxiedInterface.Top = serverTabPage3G4G.Height - lnkLaunchProxiedInterface.Height - lnkLaunchProxiedInterface.Margin.Bottom;
            lnkLaunchProxiedInterface.Left = (serverTabPage3G4G.Width - lnkLaunchProxiedInterface.Width) / 2;
            //Resize launch web interface QR Code
            proxyUrlQrCodePanel.Height = lnkLaunchProxiedInterface.Top - lnkLaunchProxiedInterface.Margin.Top - proxyUrlQrCodePanel.Top;
            //Rezize/position waiting image
            imgProxyWaiting.Top = proxyUrlQrCodePanel.Top + ((proxyUrlQrCodePanel.Height - imgProxyWaiting.Height) / 2);
            imgProxyWaiting.Left = proxyUrlQrCodePanel.Left + ((proxyUrlQrCodePanel.Width - imgProxyWaiting.Width) / 2);
        }
        private void setLnkLaunchWebInterfaceFontSize(float size)
        {
            lnkLaunchWebInterface.Font = new Font(
                    lnkLaunchWebInterface.Font.FontFamily,
                    size,
                    lnkLaunchWebInterface.Font.Style);
        }
        private void setLnkLaunchProxiedInterfaceFontSize(float size)
        {
            lnkLaunchProxiedInterface.Font = new Font(
                    lnkLaunchProxiedInterface.Font.FontFamily,
                    size,
                    lnkLaunchProxiedInterface.Font.Style);
        }

        private void launchWebInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchWebInterface();
        }

        private void launchWebInterface()
        {
            try
            {
                System.Diagnostics.Process.Start(webUrl);
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.LaunchWebInterfaceFailedMessage);
                MessageBox.Show(Resources.LaunchWebInterfaceFailedMessage, Resources.LaunchWebInterfaceFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                webServer.Restart();
            }
            catch (Exception ex)
            {
                string operation = Resources.WebServerRestart;
                string exMessage = String.Format(Resources.WebServerOperationFailedMessage, operation);
                string exTitle = String.Format(Resources.WebServerOperationFailedTitle, operation);

                Program.logException(ex, exMessage);
                MessageBox.Show(exMessage, exTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                webServer.Start();
            }
            catch (Exception ex)
            {
                string operation = Resources.WebServerStart;
                string exMessage = String.Format(Resources.WebServerOperationFailedMessage, operation);
                string exTitle = String.Format(Resources.WebServerOperationFailedTitle, operation);

                Program.logException(ex, exMessage);
                MessageBox.Show(exMessage, exTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string confirmMessage = Resources.WebServerConfirmStopMessage;
            string confirmTitle = Resources.WebServerConfirmStopTitle;
            if (MessageBox.Show(confirmMessage, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    webServer.Stop();
                }
                catch (Exception ex)
                {
                    string operation = Resources.WebServerStop;
                    string exMessage = String.Format(Resources.WebServerOperationFailedMessage, operation);
                    string exTitle = String.Format(Resources.WebServerOperationFailedTitle, operation);

                    Program.logException(ex, exMessage);
                    MessageBox.Show(exMessage, exTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pauseResumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webServer.State == WebServer.TournamentWebServerState.Started) webServer.Pause();
            else if (webServer.State == WebServer.TournamentWebServerState.Paused) webServer.Resume();
        }

        void webServer_StateChanged(object sender, EventArgs e)
        {
            updateWebServerStatus();
            reloadURLs();
        }

        void updateWebServerStatus()
        {
            WebServer.TournamentWebServerState webServerState = webServer.State;

            lblWebServerStatusText.Text = webServerState.ToString();
            webServerStatusToolStripMenuItem.Text = String.Format("Server Status: {0}", webServerState);

            Color statusColor;
            if (webServerState == WebServer.TournamentWebServerState.Started)
            {
                statusColor = Color.Green;
                webServerStatusToolStripMenuItem.Visible = false;
            }
            else
            {
                if (webServerState == WebServer.TournamentWebServerState.Paused) statusColor = Color.FromArgb(225, 200, 0);
                else if (webServerState == WebServer.TournamentWebServerState.Stopped) statusColor = Color.Red;
                else statusColor = Color.Black;
                webServerStatusToolStripMenuItem.Visible = true;
            }
            lblWebServerStatusText.ForeColor = statusColor;
            webServerStatusToolStripMenuItem.ForeColor = statusColor;

            restartToolStripMenuItem.Enabled = webServerState != WebServer.TournamentWebServerState.Stopped;
            startToolStripMenuItem.Enabled = webServerState != WebServer.TournamentWebServerState.Started;
            stopToolStripMenuItem.Enabled = webServerState != WebServer.TournamentWebServerState.Stopped;

            if (webServerState != WebServer.TournamentWebServerState.Stopped)
            {
                launchWebInterfaceToolStripMenuItem.Enabled = true;
                webInterfaceUrlQrCodePanel.Visible = true;
                lnkLaunchWebInterface.Visible = true;
                lblWebInterfaceURLs.Visible = true;
                cmbWebInterfaceURLs.Visible = true;
                btnReloadWebInterfaceURLs.Visible = true;
            }
            else
            {
                launchWebInterfaceToolStripMenuItem.Enabled = false;
                webInterfaceUrlQrCodePanel.Visible = false;
                lnkLaunchWebInterface.Visible = false;
                lblWebInterfaceURLs.Visible = false;
                cmbWebInterfaceURLs.Visible = false;
                btnReloadWebInterfaceURLs.Visible = false;
            }

            /*if (webServer.State == WebServer.TournamentWebServerState.Started || webServer.State == WebServer.TournamentWebServerState.Paused)
             {
                 pauseResumeToolStripMenuItem.Visible = true;
                 if (webServer.State == WebServer.TournamentWebServerState.Started) pauseResumeToolStripMenuItem.Text = "Pause";
                 else if (webServer.State == WebServer.TournamentWebServerState.Paused) pauseResumeToolStripMenuItem.Text = "Resume";
             }
             else pauseResumeToolStripMenuItem.Visible = false;*/
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void lnkLaunchWebInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            launchWebInterface();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog();
            Controller.NumCourts = (int)Settings.Default.numCourts;
        }

        private void lockTeamsRearrange()
        {
            teamsRearrangeIsLocked = true;
            unlockToolStripMenuItem.Visible = true;
            lblTeamsLocked.Visible = true;
            lockTournamentToolStripMenuItem.Visible = false;
            foreach (EditableItemsManager divisionTeamsManager in _divisionTeamsManagers)
            {
                divisionTeamsManager.LockRearrange = true;
            }
        }

        private void unlockTeamsRearrange()
        {
            teamsRearrangeIsLocked = false;
            lockTournamentToolStripMenuItem.Visible = true;
            unlockToolStripMenuItem.Visible = false;
            lblTeamsLocked.Visible = false;
            foreach (EditableItemsManager divisionTeamsManager in _divisionTeamsManagers)
            {
                divisionTeamsManager.LockRearrange = false;
            }
        }

        private void lockTournamentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockTeamsRearrange();
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.UnlockTeamsMessage, Resources.UnlockTeams, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                unlockTeamsRearrange();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!handleDataClear()) e.Cancel = true;
            }
        }

        private bool handleDataClear()
        {
            if (isDirty)
            {
                DialogResult response = askShouldSaveChanges();
                if (response == DialogResult.Yes) return save();
                else if (response == DialogResult.No) return true;
                else if (response == DialogResult.Cancel) return false;
            }
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.NewTournamentFailed);
                MessageBox.Show(Resources.NewTournamentFailedMessage, Resources.NewTournamentFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //
        //Begin Proxy Interface
        //
        private void startProxyAsync()
        {
            try
            {
                System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(startProxy);
                new System.Threading.Thread(threadStart).Start();
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.ProxySetUrlFailed);
                MessageBox.Show(Resources.ProxySetUrlFailed, Resources.ProxySetUrlFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void startProxy()
        {
            if (proxy != null && proxy.State != WebServer.TournamentProxyState.Stopped && proxy.State != WebServer.TournamentProxyState.Stopping)
                    return;

            string url = txtProxyUrl.Text;
            string sharedKey = txtProxySharedKey.Text;
              
            Uri uri = null;
            try
            {
                uri = new Uri(!url.StartsWith("http") ? "http://" + url : url, UriKind.Absolute);
            }
            catch (Exception ex)
            {
                Program.logException(ex, String.Format(Resources.ProxyInvalidUrl, url));
            }
            if (uri == null)
            {
                DialogResult result = MessageBox.Show(String.Format(Resources.ProxyInvalidUrl, url), Resources.ProxyInvaildUrlTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtProxyUrl.InvokeRequired)
                {
                    txtProxyUrl.Invoke((MethodInvoker)delegate()
                    {
                        txtProxyUrl.Focus();
                    });
                }
                else txtProxyUrl.Focus();
                return;
            }
            
            try
            {
                proxy = new WebServer.TournamentProxy(uri, sharedKey, webServer);
                proxy.StateChanged += new EventHandler(proxy_StateChanged);
                proxy.Start();
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.ProxyStartFailed);
                MessageBox.Show(Resources.ProxyStartFailed + "\r\n\r\n" + ex.Message, Resources.ProxyStartFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate() {
                            imgProxyWaiting.Visible = false;
                            //Update launching methods
                            proxiedInterfaceUrlQrData = new ByteMatrix(0, 0).Array;
                            paintQrCode(proxiedInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);
                            imgProxyWaiting.Visible = false;
                            proxyUrlQrCodePanel.Visible = false;
                            lnkLaunchProxiedInterface.Visible = false;
                        });
                }
                else
                {
                    imgProxyWaiting.Visible = false;
                    //Update launching methods
                    proxiedInterfaceUrlQrData = new ByteMatrix(0, 0).Array;
                    paintQrCode(proxiedInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);
                    imgProxyWaiting.Visible = false;
                    proxyUrlQrCodePanel.Visible = false;
                    lnkLaunchProxiedInterface.Visible = false;
                }
            }
        }

        private void stopProxyAsync()
        {
            try
            {
                System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(stopProxy);
                new System.Threading.Thread(threadStart).Start();
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.ProxyStopFailed);
            }
        }
        private void stopProxy()
        {
            if (proxy != null && proxy.State != WebServer.TournamentProxyState.Stopped && proxy.State != WebServer.TournamentProxyState.Stopping)
            {
                try
                {
                    proxy.Stop();
                }
                catch (Exception ex)
                {
                    Program.logException(ex, Resources.ProxyStopFailed);
                }
            }
        }

        void proxy_StateChanged(object sender, EventArgs e)
        {
            WebServer.TournamentProxyState proxyState = proxy.State;

            lblProxyStatusText.Text = proxyState.ToString();
            proxyStatusToolStripMenuItem.Text = String.Format("Proxy Status: {0}", proxyState);

            Color statusColor;
            if (proxyState == WebServer.TournamentProxyState.Started)
            {
                statusColor = Color.Green;
                proxyStatusToolStripMenuItem.Visible = false;
            }
            else
            {
                if (proxyState == WebServer.TournamentProxyState.Paused) statusColor = Color.FromArgb(225, 200, 0);
                else if (proxyState == WebServer.TournamentProxyState.Stopped || proxyState == WebServer.TournamentProxyState.Error) statusColor = Color.Red;
                else statusColor = Color.Black;
                proxyStatusToolStripMenuItem.Visible = true;
            }
            lblProxyStatusText.ForeColor = statusColor;
            proxyStatusToolStripMenuItem.ForeColor = statusColor;

            if (proxyState == WebServer.TournamentProxyState.Started)
            {
                string url = proxy.ProxyUrl.ToString();

                //Update QR code
                com.google.zxing.qrcode.QRCodeWriter writer = new com.google.zxing.qrcode.QRCodeWriter();
                proxiedInterfaceUrlQrData = writer.encode(url, com.google.zxing.BarcodeFormat.QR_CODE, 0, 0).Array;
                paintQrCode(proxiedInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);

                //Show QR code
                proxyUrlQrCodePanel.Visible = true;

                //Set launch interface text
                lnkLaunchProxiedInterface.Text = url;
                //Show launch interface link
                lnkLaunchProxiedInterface.Visible = true;

                refreshSizing();

                //Show QR code
                proxyUrlQrCodePanel.Visible = true;

                //Save configuration
                try
                {
                    Settings.Default.ProxyUrl = proxy.ProxyUrl.OriginalString;
                    Settings.Default.ProxySharedKey = proxy.SharedKey;
                    Settings.Default.Save();
                }
                catch { }
            }
            else
            {
                //Hide QR code
                proxyUrlQrCodePanel.Visible = false;
                
                //Clear QR code
                proxiedInterfaceUrlQrData = new ByteMatrix(0, 0).Array;
                paintQrCode(proxiedInterfaceUrlQrData, webInterfaceUrlQrCodePanel.CreateGraphics(), webInterfaceUrlQrCodePanel.Size);

                //Hide launch interface link
                lnkLaunchProxiedInterface.Visible = false;
            }

            //Hide and show waiting indicator
            if (proxyState == WebServer.TournamentProxyState.Starting) imgProxyWaiting.Visible = true;
            else imgProxyWaiting.Visible = false;

            if (proxyState == WebServer.TournamentProxyState.Stopped || proxyState == WebServer.TournamentProxyState.Stopping)
            {
                //Enable stop button
                btnStopProxy.Enabled = false;
                
                //Disable start button and config fields
                btnStartProxy.Enabled = true;
                txtProxyUrl.Enabled = true;
                txtProxySharedKey.Enabled = true;
            }
            else
            {
                //Disable stop button
                btnStopProxy.Enabled = true;

                //Enable start button and config fields
                btnStartProxy.Enabled = false;
                txtProxyUrl.Enabled = false;
                txtProxySharedKey.Enabled = false;
            }
        }

        private void proxyUrlQrCodePanel_Paint(object sender, PaintEventArgs e)
        {
            paintQrCode(proxiedInterfaceUrlQrData, e.Graphics, proxyUrlQrCodePanel.Size);
        }

        private void lnkLaunchProxiedInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            launchProxiedInterface();
        }

        private void launchProxiedInterface()
        {
            try
            {
                if (proxy != null) System.Diagnostics.Process.Start(proxy.ProxyUrl.ToString());
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.ProxyLaunchInterfaceFailed);
                MessageBox.Show(Resources.ProxyLaunchInterfaceFailedMessage, Resources.ProxyLaunchInterfaceFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartProxy_Click(object sender, EventArgs e)
        {
            startProxyAsync();
        }

        private void btnStopProxy_Click(object sender, EventArgs e)
        {
            stopProxyAsync();
        }

        private void currentRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tournament != null && Tournament.IsCompleted)
            {
                tabControl.SelectTab(tabPageSeeding);
            }
            else
            {
                tabControl.SelectTab(tabPageSchedule);
            }
        }

        private void enableScheduleEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableScheduleEditToolStripMenuItem.Checked)
            {
                enableScheduleEditToolStripMenuItem.Checked = false;
                scheduleDisplay.EnableEditing = false;
            }
            else
            {
                enableScheduleEditToolStripMenuItem.Checked = true;
                scheduleDisplay.EnableEditing = true;

            }
        }
        //End Proxy Interface
    }
}