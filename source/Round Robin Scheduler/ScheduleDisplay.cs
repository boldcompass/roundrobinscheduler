using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class ScheduleDisplay : UserControl
    {
        protected int defaultNumCourts = 5;
        protected int headerHeight = 20;
        protected const int defaultRoundColumnWidth = 55;
        protected int roundColumnWidth = 50;
        protected int courtRoundHeight = 50;
        protected int scoreKeeperNameHeight = 15;
        protected int unconfirmedTextHeight = 15;
        protected int courtColumnWidth;
        protected int drawWidth;
        protected Point tempScrollPosition;
        protected Padding teamIdMargin = new Padding(0, 0, 5, 0);
        protected Padding editScoreIconMargin = new Padding(5);
        protected Padding winnerIconMargin = new Padding(5);
        protected Padding roundNumberMargin = new Padding(5, 20, 5, 5);
        protected Padding scoreMargin = new Padding(5, 0, 0, 0);
        protected Padding foulsMargin = new Padding(0, 0, 0, 0);
        protected Padding gamePadding = new Padding(5);

        //Colors
        protected Brush normalTextBrush;
        protected Brush teamFoulsBrush;
        protected Brush winnerTextBrush;
        protected Brush winnerScoreBrush;
        protected Brush loserTextBrush;
        protected Brush loserScoreBrush;
        protected Brush scoreKeeperDuplicateTextBrush;
        protected Brush scoreKeeperNotAssignedTextBrush;
        protected Brush unconfirmedTextBrush;
        protected Pen headerHighlightPen;
        protected Pen headerShadowPen;
        protected Pen gamesSeparatorPen;
        protected Pen robinRoundSeparatorPen;
        protected Pen selectedGamePen;
        protected List<Pen> robinRoundSeparatorOutlinePens;
        protected Color oddGameNormalColor;
        protected Color evenGameNormalColor;
        protected Color illogicalGameNormalColor;// A game in which the loser scored more points than the winner
        protected Color oddGameHoverColor;
        protected Color evenGameHoverColor;
        protected Color illogicalGameHoverColor;// A game in which the loser scored more points than the winner

        //Fonts
        protected Font headerFont;
        protected Font roundNumberFont;
        protected Font normalTeamIdFont;
        protected Font normalTeamNameFont;
        protected Font teamScoreFont;
        protected Font teamFoulsFont;
        protected Font scoreKeeperNameFont;
        protected Font unconfirmedTextFont;        
        protected Font winnerTeamIdFont;
        protected Font winnerTeamNameFont;
        protected Font loserTeamIdFont;
        protected Font loserTeamNameFont;

        protected Controller Controller = Controller.GetController();

        protected List<CheckBox> _showHideRobinRoundControls = new List<CheckBox>();
        protected List<bool> _showHideRobinRoundValues = new List<bool>();
        protected RectangleF[,] gameRectangles = null;
        protected Dictionary<Game, RectangleF> gameRectanglesByGame = new Dictionary<Game,RectangleF>();

        protected Game currentHoveredGame = null;
        protected RectangleF currentHoveredGameRectangle = new RectangleF();

        protected int currentSelectedGameIndex = -1;
        protected Game currentSelectedGame = null;
        protected RectangleF currentSelectedGameRectangle = new RectangleF();

        public Tournament Tournament
        {
            get
            {
                return Controller.Tournament;
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                calcuateFonts();
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                calculateColors();
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                calculateColors();
            }
        }

        public ScheduleDisplay()
        {
            InitializeComponent();

            //Reduce flicker
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            calcuateFonts();
            calculateColors();

            Controller.NumCourtsChanged += new EventHandler(Controller_NumCourtsChanged);
            Controller.TournamentChanged += new EventHandler(Controller_TournamentChanged);
            Controller.GameResultChanged += new GameResultChangedEventHandler(Controller_GameResultChanged);
            Controller.TeamNameChanged += new TeamNameChangedEventHandler(Controller_TeamNameChanged);
            Controller.CourtRoundsIsActiveChanged += new EventHandler(Controller_CourtRoundsIsActiveChanged);
            Controller.CourtRoundsIsConfirmedChanged += new EventHandler(Controller_CourtRoundsIsConfirmedChanged);
            Controller.CourtRoundsIsInProgressChanged += new EventHandler(Controller_CourtRoundsIsInProgressChanged);
            courtRoundsPaintable.GotFocus += new EventHandler(courtRoundsPaintable_GotFocus);
        }

        void Controller_CourtRoundsIsInProgressChanged(object sender, EventArgs e)
        {
            courtRoundsPaintable.Invalidate();
        }

        void courtRoundsPaintable_GotFocus(object sender, EventArgs e)
        {
            if (this.currentSelectedGame == null)
            {
                this.selectGameByIndex((Control.ModifierKeys & Keys.Shift) == Keys.Shift ? Tournament.Games.Count - 1 : 0);
            }
        }

        void Controller_CourtRoundsIsConfirmedChanged(object sender, EventArgs e)
        {
            courtRoundsPaintable.Invalidate();
        }

        void Controller_NumCourtsChanged(object sender, EventArgs e)
        {
            courtRoundsPaintable.Invalidate();
        }

        void Controller_CourtRoundsIsActiveChanged(object sender, EventArgs e)
        {
            courtRoundsPaintable.Invalidate();
        }

        void Controller_TeamNameChanged(object sender, TeamNameChangedEventArgs e)
        {
            courtRoundsPaintable.Invalidate();
        }

        void Controller_GameResultChanged(object sender, GameResultChangedEventArgs e)
        {
            if (gameRectanglesByGame!=null && gameRectanglesByGame.ContainsKey(e.Game))
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(gameRectanglesByGame[e.Game]));
        }

        void Controller_TournamentChanged(object sender, EventArgs e)
        {
            if (Tournament != null)
            {
                _showHideRobinRoundValues.Clear();
                resetRobinRoundCheckboxes();
                refreshSizing();
                Refresh();
            }
        }

        protected void calcuateFonts()
        {
            headerFont = new Font(Font, FontStyle.Bold);
            roundNumberFont = new Font(Font, FontStyle.Bold);

            //Normal team font
            normalTeamIdFont = new Font(Font, FontStyle.Bold);
            normalTeamNameFont = new Font(Font, FontStyle.Regular);
            teamScoreFont = new Font(Font, FontStyle.Bold);
            teamFoulsFont = new Font(Font.FontFamily, Font.Size - 1.25F, FontStyle.Regular);
            scoreKeeperNameFont = new Font(Font, FontStyle.Regular);
            unconfirmedTextFont = new Font(Font.FontFamily, Font.Size - 1.25F, FontStyle.Regular);

            //Winner team font
            winnerTeamIdFont = (Font)normalTeamIdFont.Clone();
            winnerTeamNameFont = (Font)normalTeamNameFont.Clone();

            //Loser team font
            //loserTeamIdFont = new Font(normalTeamIdFont, ((FontStyle)(normalTeamIdFont.Style | FontStyle.Strikeout)));
            //loserTeamNameFont = new Font(normalTeamNameFont, ((FontStyle)(normalTeamNameFont.Style | FontStyle.Strikeout)));
            loserTeamIdFont = new Font(normalTeamIdFont, FontStyle.Regular);
            loserTeamNameFont = new Font(normalTeamNameFont, FontStyle.Regular);
        }

        protected void calculateColors()
        {
            //Brushes
            normalTextBrush = new SolidBrush(ForeColor);
            teamFoulsBrush = new SolidBrush(Color.FromArgb(175, 175, 175));
            winnerTextBrush = new SolidBrush(Color.Black);
            winnerScoreBrush = (Brush)normalTextBrush.Clone();
            loserTextBrush = new SolidBrush(Color.FromArgb(175, 175, 175));
            loserScoreBrush = (Brush)normalTextBrush.Clone();
            scoreKeeperDuplicateTextBrush = new SolidBrush(Color.Red);
            scoreKeeperNotAssignedTextBrush = new SolidBrush(Color.Red);
            unconfirmedTextBrush = new SolidBrush(ForeColor);

            //Pens
            headerHighlightPen = new Pen(SystemColors.ControlLightLight, 1);
            selectedGamePen = new Pen(Color.Black, 1);
            selectedGamePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            headerShadowPen = new Pen(SystemColors.ControlDark, 1);
            gamesSeparatorPen = new Pen(Color.FromArgb(245, 230, 205), 1);
            robinRoundSeparatorPen = new Pen(Color.DarkOrange, 3);
            robinRoundSeparatorOutlinePens = new List<Pen>(new Pen[] { new Pen(Color.OrangeRed, 1), new Pen(Color.White, 1) });

            //Colors
            oddGameNormalColor = Color.FromArgb(255, 240, 215);
            evenGameNormalColor = Color.White;
            illogicalGameNormalColor = Color.FromArgb(252, 173, 149);
            oddGameHoverColor = Color.FromArgb(255, 213, 166);
            evenGameHoverColor = Color.FromArgb(255, 219, 178);
            illogicalGameHoverColor = Color.FromArgb(250, 148, 117);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            int drawTop = 0;
            int drawLeft;

            //Header
            drawLeft = 0;
            StringFormat headerStringFormat = new StringFormat();
            headerStringFormat.Alignment = StringAlignment.Center;
            headerStringFormat.LineAlignment = StringAlignment.Center;

            //Round number header
            RectangleF roundHeaderRect =
                new RectangleF(
                    drawLeft,
                    drawTop,
                    roundColumnWidth,
                    headerHeight);
            e.Graphics.DrawString("Round", headerFont, normalTextBrush, roundHeaderRect, headerStringFormat);
            e.Graphics.DrawLine(headerHighlightPen, new PointF(roundHeaderRect.Left, roundHeaderRect.Top), new PointF(roundHeaderRect.Left, roundHeaderRect.Bottom - 1));
            e.Graphics.DrawLine(headerShadowPen, new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Top), new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Bottom - 1));
            drawLeft += roundColumnWidth;

            //Court headers
            int numCourts;
            if (Tournament != null) numCourts = Tournament.NumCourts;
            else numCourts = defaultNumCourts;

            for (int courtNum = 0; courtNum < numCourts; courtNum++)
            {
                string headerTitle = CourtRound.CourtNumToCourtName(courtNum + 1);
                RectangleF courtHeaderRect =
                new RectangleF(
                    drawLeft,
                    drawTop,
                    courtColumnWidth,
                    headerHeight);
                e.Graphics.DrawString(headerTitle, headerFont, normalTextBrush, courtHeaderRect, headerStringFormat);
                e.Graphics.DrawLine(headerHighlightPen, new PointF(courtHeaderRect.Left, courtHeaderRect.Top), new PointF(courtHeaderRect.Left, courtHeaderRect.Bottom - 1));
                e.Graphics.DrawLine(headerShadowPen, new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Top), new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Bottom - 1));
                drawLeft += courtColumnWidth;
            }
            e.Graphics.DrawLine(headerShadowPen, new PointF(0, roundHeaderRect.Bottom - 1), new PointF(drawWidth, roundHeaderRect.Bottom - 1));
        }

        private void refreshSizing()
        {
            scrollingPanel.Top = headerHeight;
            scrollingPanel.Width = Width;
            courtRoundsPaintable.Width = scrollingPanel.Width;
            scrollingPanel.Height = Height - headerHeight;
            this.PerformAutoScale();

            //Calculate draw width (width minus scrollbars if applicable)
            drawWidth = Width;
            if (scrollingPanel.VerticalScroll.Visible) drawWidth -= SystemInformation.VerticalScrollBarWidth;

            int numCourts;
            if (Tournament != null) numCourts = Tournament.NumCourts;
            else numCourts = defaultNumCourts;
            courtColumnWidth = (int)Math.Floor((double)((drawWidth - defaultRoundColumnWidth) / numCourts));
            roundColumnWidth = drawWidth - (courtColumnWidth * numCourts);

            refreshCheckboxSizing();
        }

        private void refreshCheckboxSizing()
        {

            courtRoundsPaintable.SuspendLayout();
            //Reposition show/hide robin round controls
            for (int robinRoundIndex = 1; robinRoundIndex < _showHideRobinRoundControls.Count; robinRoundIndex++)
            {
                if (Tournament.RobinRounds.Count < robinRoundIndex)
                {
                    break;
                }

                CheckBox showRobinRoundChk = _showHideRobinRoundControls[robinRoundIndex];

                RobinRound previousRobinRound = Tournament.RobinRounds[robinRoundIndex - 1];

                //The last game in the previous robin round
                Game lastGameInRobinRound = previousRobinRound.Games[previousRobinRound.Games.Count - 1];
                //The rectangle of the first game of the last court round of the robin round
                if (gameRectangles == null || gameRectangles.GetLength(0) < lastGameInRobinRound.CourtRoundNum) continue;
                RectangleF positioningGameRect = gameRectangles[lastGameInRobinRound.CourtRoundNum - 1, 0];

                int checkboxTopOffset = 4;
                showRobinRoundChk.Width = (int)positioningGameRect.Left - 3;
                showRobinRoundChk.Top = (int)positioningGameRect.Bottom + checkboxTopOffset;
                showRobinRoundChk.Left = 0;
            }
            courtRoundsPaintable.ResumeLayout();
        }

        private void calculateNumRobinRounds()
        {
            int robinRoundsToPlay = Tournament.TotalRobinRounds;
            for (int i = 0; i < _showHideRobinRoundValues.Count; i++)
            {
                if (_showHideRobinRoundValues[i] == false)
                {
                    robinRoundsToPlay = i;
                    break;
                }
            }
            Tournament.RobinRoundsToPlay = robinRoundsToPlay;

            btnMoreRounds.Visible = Tournament.RobinRoundsToPlay >= Tournament.TotalRobinRounds;
        }
        private void refreshCheckboxVisibility()
        {
            bool checkboxesShouldShow = true;
            for (int robinRoundIndex = 1; robinRoundIndex < Tournament.TotalRobinRounds; robinRoundIndex++)
            {
                if (_showHideRobinRoundControls.Count <= robinRoundIndex || _showHideRobinRoundControls[robinRoundIndex] == null || _showHideRobinRoundValues.Count <= robinRoundIndex) continue;

                _showHideRobinRoundControls[robinRoundIndex].Visible = checkboxesShouldShow;
                if (_showHideRobinRoundValues[robinRoundIndex] == false) checkboxesShouldShow = false;
            }
        }

        private void resetRobinRoundCheckboxes()
        {
            courtRoundsPaintable.SuspendLayout();
            //Remove old controls
            foreach (Control control in _showHideRobinRoundControls)
            {
                if (control == null) continue;
                control.Dispose();
                Controls.Remove(control);
            }
            _showHideRobinRoundControls.Clear();
            _showHideRobinRoundValues.Clear();
            while (_showHideRobinRoundValues.Count < Tournament.TotalRobinRounds)
            {
                if (_showHideRobinRoundValues.Count != Tournament.RobinRoundsToPlay)
                {
                    _showHideRobinRoundValues.Add(true);
                }
                else
                {
                    _showHideRobinRoundValues.Add(false);
                }
            }

            for (int robinRoundIndex = 0; robinRoundIndex < Tournament.TotalRobinRounds; robinRoundIndex++)
            {
                if (robinRoundIndex < 1)
                {
                    _showHideRobinRoundControls.Add(null);
                    continue;
                }

                CheckBox showRobinRoundChk = new CheckBox();
                showRobinRoundChk.Name = String.Format("robinRound{0}ShowRound", robinRoundIndex + 1);
                showRobinRoundChk.Text = "Stop";
                showRobinRoundChk.TabStop = false;
                showRobinRoundChk.CheckAlign = ContentAlignment.MiddleLeft;
                showRobinRoundChk.Checked = !_showHideRobinRoundValues[robinRoundIndex];
                showRobinRoundChk.AutoSize = false;
                int checkboxRobinRoundIndex = robinRoundIndex;
                showRobinRoundChk.CheckedChanged +=
                    delegate(object sender, EventArgs e)
                    {
                        _showHideRobinRoundValues[checkboxRobinRoundIndex] = !showRobinRoundChk.Checked;
                        refreshCheckboxVisibility();
                        calculateNumRobinRounds();
                        courtRoundsPaintable.Refresh();
                        this.Invalidate(new Rectangle(0, 0, this.Width, headerHeight));
                        refreshSizing();
                    };
                courtRoundsPaintable.Controls.Add(showRobinRoundChk);
                _showHideRobinRoundControls.Add(showRobinRoundChk);
                showRobinRoundChk.Height = 20;
                showRobinRoundChk.Font = new Font(showRobinRoundChk.Font.FontFamily, 8, showRobinRoundChk.Font.Style);
                showRobinRoundChk.TextAlign = ContentAlignment.MiddleLeft;
                showRobinRoundChk.AutoEllipsis = true;
                showRobinRoundChk.Margin = new Padding(0);
                courtRoundsPaintable.Controls.SetChildIndex(showRobinRoundChk, 0);
            }
            refreshCheckboxVisibility();
            courtRoundsPaintable.ResumeLayout();
        }

        private void ScheduleDisplay_Resize(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void courtRoundsPanel_Paint(object sender, PaintEventArgs e)
        {
            // Get ready to draw
            scrollingPanel.SuspendLayout(); 

            //Begin
            Graphics graphics = e.Graphics;
            
            //Paint
            graphics.Clear(courtRoundsPaintable.BackColor);
            int drawTop = 0;
            int drawLeft = 0;

            if (Tournament != null)
            {
                RectangleF[,] oldGameRectangles = gameRectangles;
                gameRectangles = new RectangleF[Tournament.CourtRounds.Count, Tournament.NumCourts];
                gameRectanglesByGame.Clear();

                #region Draw court rounds
                //Draw court rounds
                StringFormat roundNumberStringFormat = new StringFormat();
                roundNumberStringFormat.Alignment = StringAlignment.Center;
                roundNumberStringFormat.LineAlignment = StringAlignment.Center;
                Image WinnerIconImage = Properties.Resources.WinnerIcon;
                int winnerIconSpace = WinnerIconImage.Width + winnerIconMargin.Left + winnerIconMargin.Right;

                foreach (CourtRound courtRound in Tournament.CourtRounds)
                {
                    #region Draw court round other
                    drawLeft = 0;

                    bool courtRoundIsActive = courtRound.IsActive;
                    bool courtRoundIsCompleted = courtRound.IsCompleted;
                    bool courtRoundIsInProgress = courtRound.IsInProgress;
                    bool shouldShowUnconfirmedText = !courtRound.IsConfirmed;
                    bool shouldShowScoreKeeperName = courtRoundIsActive || shouldShowUnconfirmedText || courtRoundIsInProgress;

                    Dictionary<ScoreKeeper, int> timesScoreKeeperInCourtRound = null;
                    if (shouldShowScoreKeeperName)
                    {
                        timesScoreKeeperInCourtRound = new Dictionary<ScoreKeeper, int>();
                        foreach (KeyValuePair<GamePosition, ScoreKeeper> pair in Controller.GetScoreKeepersForCourtRound(courtRound.RoundNumber))
                        {
                            if (!timesScoreKeeperInCourtRound.ContainsKey(pair.Value)) timesScoreKeeperInCourtRound.Add(pair.Value, 1);
                            else timesScoreKeeperInCourtRound[pair.Value]++;
                        }
                    }

                    int roundHeight = courtRoundHeight;
                    if (shouldShowUnconfirmedText) roundHeight += unconfirmedTextHeight;
                    if (shouldShowScoreKeeperName) roundHeight += scoreKeeperNameHeight;

                    //Round number
                    RectangleF roundNumberRect =
                       new RectangleF(
                           drawLeft,
                           drawTop,
                           roundColumnWidth,
                           roundHeight);
                    RectangleF roundNumberTextRect =
                       new RectangleF(
                           roundNumberRect.Left + roundNumberMargin.Left,
                           roundNumberRect.Top + roundNumberMargin.Top,
                           roundNumberRect.Width - roundNumberMargin.Left - roundNumberMargin.Right,
                           roundNumberRect.Height - roundNumberMargin.Top - roundNumberMargin.Bottom);

                    graphics.DrawString(courtRound.RoundNumber.ToString(), roundNumberFont, normalTextBrush, roundNumberTextRect, roundNumberStringFormat);
                    graphics.DrawLine(headerHighlightPen, new PointF(roundNumberRect.Left, roundNumberRect.Top), new PointF(roundNumberRect.Right - 1, roundNumberRect.Top));
                    graphics.DrawLine(headerShadowPen, new PointF(roundNumberRect.Left, roundNumberRect.Bottom - 1), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                    graphics.DrawLine(headerShadowPen, new PointF(roundNumberRect.Right - 1, roundNumberRect.Top), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                    drawLeft += roundColumnWidth;
                    #endregion

                    #region Draw games
                    //Draw games
                    foreach (Game game in courtRound.Games)
                    {
                        #region Draw game other
                        GamePosition gamePosition = game.GamePosition;

                        RectangleF gameRectangle =
                            new RectangleF(
                                    drawLeft,
                                    drawTop,
                                    courtColumnWidth,
                                    roundHeight);
                        //Check if this game should be redrawn
                        bool shouldDraw = true;
                        if (oldGameRectangles != null && oldGameRectangles.GetLength(0) >= game.CourtRoundNum && oldGameRectangles.GetLength(1) >= game.CourtNumber)
                        {
                            RectangleF oldGameRectangle = oldGameRectangles[game.CourtRoundNum - 1, game.CourtNumber - 1];
                            if (oldGameRectangle != RectangleF.Empty)
                            {
                                if (gameRectangle.Equals(oldGameRectangle))
                                {
                                    //Region intersection = graphics.Clip.Clone();
                                    //intersection.Intersect(gameRectangle);
                                    //shouldDraw = !intersection.IsEmpty(graphics);
                                    if (!e.ClipRectangle.IntersectsWith(RectangleFToRectangle(gameRectangle))) shouldDraw = false;
                                }
                                else if (currentHoveredGame == game)
                                {
                                    //Update hover rectangle
                                    currentHoveredGameRectangle = gameRectangle;
                                }

                                if(currentSelectedGame == game){
                                    //Update selected rectangle
                                    currentSelectedGameRectangle = gameRectangle;
                                }
                            }
                        }

                        gameRectangles[game.CourtRoundNum - 1, game.CourtNumber - 1] = gameRectangle;
                        gameRectanglesByGame.Add(game, gameRectangle);
                        if (shouldDraw)
                        {

                            bool isHoveredGame = game == currentHoveredGame;
                            System.Diagnostics.Trace.WriteLine(game.ToString());
                            Color gameFillColor;
                            if (game.IsLogicalResult != false)
                            {
                                if (isHoveredGame)
                                {
                                    gameFillColor = game.RobinRoundNum % 2 != 0 ? oddGameHoverColor : evenGameHoverColor;
                                }
                                else
                                {
                                    gameFillColor = game.RobinRoundNum % 2 != 0 ? oddGameNormalColor : evenGameNormalColor;
                                }
                            }
                            else
                            {
                                // A game in which the loser scored more points than the winner
                                gameFillColor = isHoveredGame ? illogicalGameHoverColor : illogicalGameNormalColor;
                            }

                            graphics.FillRectangle(new SolidBrush(gameFillColor), gameRectangle);

                            if (isHoveredGame)
                            {
                                Image editScoreIconImage = Properties.Resources.EditScoreIcon;

                                Rectangle editScoreIconRectangle =
                                    new Rectangle(
                                            (int)gameRectangle.Right - editScoreIconImage.Width - editScoreIconMargin.Right,
                                            (int)gameRectangle.Bottom - editScoreIconImage.Height - editScoreIconMargin.Bottom,
                                            editScoreIconImage.Width,
                                            editScoreIconImage.Height);
                                graphics.DrawImage(editScoreIconImage, editScoreIconRectangle);
                            }
                            int gameWidth = courtColumnWidth - gamePadding.Top - gamePadding.Bottom;
                            int gameHeight = roundHeight - gamePadding.Left - gamePadding.Right;

                            int availableTeamsHeight = gameHeight;
                            if (shouldShowUnconfirmedText) availableTeamsHeight -= unconfirmedTextHeight;
                            if (shouldShowScoreKeeperName) availableTeamsHeight -= scoreKeeperNameHeight;

                            int teamDrawTop = drawTop + gamePadding.Top;
                            int teamDrawLeft = drawLeft + gamePadding.Left;
                            int teamDrawHeight = availableTeamsHeight / game.NumTeams;
                        #endregion
                            #region Calculate largest score and foul size
                            //Calculate largest score and foul size
                            int maxScoreFoulsWidth = 0;
                            Dictionary<Team, bool> shouldShowScoreForTeams = new Dictionary<Team, bool>();
                            Dictionary<Team, bool> shouldShowFoulsForTeams = new Dictionary<Team, bool>();
                            foreach (Team team in game.Teams)
                            {
                                if (team != null)
                                {
                                    TeamGameResult teamGameResult = game.TeamGameResults[team.Id];

                                    bool showGameStats = courtRoundIsActive || courtRoundIsCompleted || game.IsInProgress || game.IsCompleted;
                                    bool shouldShowScore = showGameStats;
                                    shouldShowScoreForTeams[team] = shouldShowScore;
                                    bool shouldShowFouls = (showGameStats) && teamGameResult.NumFouls > 0;
                                    shouldShowFoulsForTeams[team] = shouldShowFouls;

                                    if (shouldShowScore || shouldShowFouls)
                                    {
                                        int scoreFoulsWidth = 0;
                                        string teamScoreText = null;
                                        string teamFoulsText = null;

                                        #region Calculate size for score
                                        //Calculate size for score if needed
                                        if (shouldShowScore)
                                        {
                                            teamScoreText = teamGameResult.NumPoints.ToString();
                                           
                                            float scoreWidth = graphics.MeasureString(teamScoreText, teamScoreFont).Width;

                                            scoreFoulsWidth += (int)(scoreMargin.Left + scoreWidth + scoreMargin.Right);
                                        }
                                        #endregion

                                        #region Calculate size for fouls
                                        //Calculate size for number of fouls if needed
                                        if (shouldShowFouls)
                                        {
                                            teamFoulsText = string.Format("({0}F)", teamGameResult.NumFouls.ToString());
                                           
                                            float foulsWidth = graphics.MeasureString(teamFoulsText, teamFoulsFont).Width;

                                            scoreFoulsWidth += (int)(foulsMargin.Left + foulsWidth + foulsMargin.Right);
                                        }
                                        #endregion
                                        if (scoreFoulsWidth > maxScoreFoulsWidth) maxScoreFoulsWidth = scoreFoulsWidth;
                                    }
                                }
                            }
                                    
                            #endregion
                            #region Draw teams
                            //Draw teams
                            foreach (Team team in game.Teams)
                            {

                                if (team != null)
                                {
                                    #region Draw team other
                                    TeamGameResult teamGameResult = game.TeamGameResults[team.Id];

                                    bool shouldShowScore = shouldShowScoreForTeams[team];
                                    bool shouldShowFouls = shouldShowFoulsForTeams[team];

                                    Font teamIdFont = normalTeamIdFont;
                                    Font teamNameFont = normalTeamNameFont;
                                    Brush teamTextBrush = normalTextBrush;
                                    Brush teamScoreBrush = normalTextBrush;
                                    if (teamGameResult.WonGame)
                                    {
                                        teamTextBrush = winnerTextBrush;
                                        teamScoreBrush = winnerScoreBrush;
                                        teamIdFont = winnerTeamIdFont;
                                        teamNameFont = winnerTeamNameFont;
                                    }
                                    else if (game.WinningTeam != null)
                                    {
                                        teamTextBrush = loserTextBrush;
                                        teamScoreBrush = loserScoreBrush;
                                        teamIdFont = loserTeamIdFont;
                                        teamNameFont = loserTeamNameFont;
                                    }
                                    #endregion

                                    #region Draw score and fouls
                                    //Draw score and fouls
                                    if (shouldShowScore || shouldShowFouls)
                                    {
                                        RectangleF scoreRect = RectangleF.Empty;
                                        string teamScoreText = null;
                                        RectangleF foulsRect = RectangleF.Empty;
                                        string teamFoulsText = null;
                                        StringFormat teamFoulsStringFormat = null;
                                        StringFormat teamScoreStringFormat = null;

                                        #region Calculate positioning for score
                                        //Calculate positioning for score if needed
                                        if (shouldShowScore)
                                        {
                                            teamScoreText = teamGameResult.NumPoints.ToString();
                                            teamScoreStringFormat = new StringFormat();
                                            teamScoreStringFormat.Alignment = StringAlignment.Center;
                                            teamScoreStringFormat.LineAlignment = StringAlignment.Center;
                                            teamScoreStringFormat.FormatFlags = StringFormatFlags.NoWrap;

                                            float scoreWidth = graphics.MeasureString(teamScoreText, teamScoreFont).Width;

                                            scoreRect =
                                            new RectangleF(
                                                teamDrawLeft + gameWidth - maxScoreFoulsWidth + scoreMargin.Left,
                                                teamDrawTop,
                                                scoreWidth,
                                                teamDrawHeight);
                                        }
                                        #endregion

                                        #region Calculate positioning for fouls
                                        //Calculate positioning for number of fouls if needed
                                        if (shouldShowFouls)
                                        {
                                            teamFoulsText = string.Format("({0}F)", teamGameResult.NumFouls.ToString());
                                            teamFoulsStringFormat = new StringFormat();
                                            teamFoulsStringFormat.Alignment = StringAlignment.Center;
                                            teamFoulsStringFormat.LineAlignment = StringAlignment.Center;
                                            teamFoulsStringFormat.FormatFlags = StringFormatFlags.NoWrap;

                                            float foulsWidth = graphics.MeasureString(teamFoulsText, teamFoulsFont).Width;

                                            foulsRect =
                                            new RectangleF(
                                                teamDrawLeft + gameWidth - maxScoreFoulsWidth + foulsMargin.Left,
                                                teamDrawTop,
                                                foulsWidth,
                                                teamDrawHeight);
                                            if (shouldShowFouls) foulsRect.X += scoreMargin.Left + scoreRect.Width + scoreMargin.Right;
                                        }
                                        #endregion

                                        if (shouldShowScore)
                                        {
                                            graphics.DrawString(teamScoreText, teamScoreFont, teamScoreBrush, scoreRect, teamScoreStringFormat);
                                        }
                                        if (shouldShowFouls)
                                        {
                                            graphics.DrawString(teamFoulsText, teamFoulsFont, teamFoulsBrush, foulsRect, teamFoulsStringFormat);
                                        }
                                    }
                                    #endregion

                                    #region Draw team name and id
                                    //Draw team name and id
                                    if (team.Name != team.Id)
                                    {
                                        //Calculate team id location
                                        StringFormat teamIdStringFormat = new StringFormat();
                                        teamIdStringFormat.Alignment = StringAlignment.Near;
                                        teamIdStringFormat.LineAlignment = StringAlignment.Center;

                                        RectangleF teamIdRect =
                                            new RectangleF(
                                                teamDrawLeft + teamIdMargin.Left + winnerIconSpace,
                                                teamDrawTop,
                                                graphics.MeasureString(team.Id, teamIdFont).Width + teamIdMargin.Right,
                                                teamDrawHeight);

                                        //Draw winner icon if needed
                                        if (teamGameResult.WonGame)
                                        {
                                            Rectangle WinnerIconRectangle =
                                                new Rectangle(
                                                    teamDrawLeft + winnerIconMargin.Left,
                                                    teamDrawTop + (teamDrawHeight - WinnerIconImage.Height) / 2,
                                                    WinnerIconImage.Width,
                                                    WinnerIconImage.Height);

                                            graphics.DrawImage(WinnerIconImage, WinnerIconRectangle);
                                        }

                                        //Calculate team name location
                                        StringFormat teamNameStringFormat = new StringFormat();
                                        teamNameStringFormat.Alignment = StringAlignment.Near;
                                        teamNameStringFormat.LineAlignment = StringAlignment.Center;
                                        teamNameStringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                        teamNameStringFormat.Trimming = StringTrimming.EllipsisCharacter;

                                        RectangleF teamNameRect =
                                            new RectangleF(
                                                teamIdRect.Right,
                                                teamDrawTop,
                                                gameWidth - (teamIdRect.Right - teamDrawLeft),
                                                teamDrawHeight);

                                        if (maxScoreFoulsWidth > 0)
                                        {
                                            teamNameRect.Width -= maxScoreFoulsWidth;
                                        }

                                        //Draw text
                                        graphics.DrawString(team.Id, teamIdFont, teamTextBrush, teamIdRect, teamIdStringFormat);
                                        graphics.DrawString(team.Name, teamNameFont, teamTextBrush, teamNameRect, teamNameStringFormat);
                                    }
                                    else
                                    {
                                        //Calculate team id location
                                        StringFormat teamIdStringFormat = new StringFormat();
                                        teamIdStringFormat.Alignment = StringAlignment.Near;
                                        teamIdStringFormat.LineAlignment = StringAlignment.Center;

                                        RectangleF teamIdRect =
                                            new RectangleF(
                                                teamDrawLeft + teamIdMargin.Left + winnerIconSpace,
                                                teamDrawTop,
                                                gameWidth - winnerIconSpace,
                                                teamDrawHeight);

                                        //Draw text
                                        graphics.DrawString(team.Id, teamIdFont, teamTextBrush, teamIdRect, teamIdStringFormat);

                                        //Draw winner icon if needed
                                        if (teamGameResult.WonGame)
                                        {
                                            SizeF teamIdSize = graphics.MeasureString(team.Id, teamIdFont);
                                            Rectangle WinnerIconRectangle =
                                               new Rectangle(
                                                   teamDrawLeft + winnerIconMargin.Left,
                                                   teamDrawTop + (teamDrawHeight - WinnerIconImage.Height) / 2,
                                                   WinnerIconImage.Width,
                                                   WinnerIconImage.Height);
                                            graphics.DrawImage(WinnerIconImage, WinnerIconRectangle);
                                        }
                                    }
                                    #endregion
                                }
                                teamDrawTop += teamDrawHeight;
                            }
                            #endregion
                            #region Draw unofficial text
                            //Draw unofficial text if needed
                            if (shouldShowUnconfirmedText && !game.IsConfirmed)
                            {
                                RectangleF unconfirmedTextRect =
                                   new RectangleF(
                                           gameRectangle.Left + gamePadding.Left,
                                           gameRectangle.Top + gameRectangle.Height - gamePadding.Bottom - unconfirmedTextHeight,
                                           gameRectangle.Width - gamePadding.Left - gamePadding.Right,
                                           unconfirmedTextHeight);
                                if (shouldShowScoreKeeperName) unconfirmedTextRect.Y -= scoreKeeperNameHeight;
                                StringFormat unofficialTextStringFormat = new StringFormat();
                                unofficialTextStringFormat.Alignment = StringAlignment.Center;
                                unofficialTextStringFormat.LineAlignment = StringAlignment.Near;
                                unofficialTextStringFormat.Trimming = StringTrimming.EllipsisCharacter;

                                string unconfirmedText = "(Unofficial)";
                                graphics.DrawString(unconfirmedText, unconfirmedTextFont, unconfirmedTextBrush, unconfirmedTextRect, unofficialTextStringFormat);
                            }
                            #endregion
                            #region Draw score keeper name
                            //Draw score keeper name if needed
                            if (shouldShowScoreKeeperName)
                            {
                                RectangleF scoreKeeperNameRect =
                                   new RectangleF(
                                           gameRectangle.Left + gamePadding.Left,
                                           gameRectangle.Top + gameRectangle.Height - gamePadding.Bottom - scoreKeeperNameHeight,
                                           gameRectangle.Width - gamePadding.Left - gamePadding.Right,
                                           scoreKeeperNameHeight);
                                StringFormat scoreKeeperStringFormat = new StringFormat();
                                scoreKeeperStringFormat.Alignment = StringAlignment.Center;
                                scoreKeeperStringFormat.LineAlignment = StringAlignment.Center;
                                scoreKeeperStringFormat.Trimming = StringTrimming.EllipsisCharacter;

                                string scoreKeeperName = "No Scorekeeper";
                                Brush scoreKeeperTextBrush = scoreKeeperNotAssignedTextBrush;
                                if (Controller.ScoreKeepersAssignment.ContainsKey(gamePosition))
                                {
                                    ScoreKeeper scoreKeeper = Controller.ScoreKeepersAssignment[gamePosition];
                                    scoreKeeperName = scoreKeeper.Name;
                                    if (timesScoreKeeperInCourtRound[scoreKeeper] <= 1)
                                    {
                                        scoreKeeperTextBrush = normalTextBrush;
                                    }
                                    //Score keeper is assigned to more than one in the same court round
                                    else
                                    {
                                        scoreKeeperTextBrush = scoreKeeperDuplicateTextBrush;
                                    }

                                }
                                graphics.DrawString(scoreKeeperName, scoreKeeperNameFont, scoreKeeperTextBrush, scoreKeeperNameRect, scoreKeeperStringFormat);
                            }
                            #endregion
                           
                            graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Right - 1, gameRectangle.Top), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                            graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Left, gameRectangle.Bottom - 1), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                        }
                        drawLeft += courtColumnWidth;
                    }
                    #endregion

                    drawTop += roundHeight;
                }
                #endregion

                #region Separator lines
                #region Calculate separator lines
                //Calculate separator lines
                List<List<PointF>> robinRoundsSeparatorLines = new List<List<PointF>>();
                for (int courtNumber = 1; courtNumber <= Tournament.NumCourts; courtNumber++)
                {
                    for (int courtRoundNumber = 1; courtRoundNumber <= Tournament.CourtRounds.Count; courtRoundNumber++)
                    {
                        Game game = Tournament.getGame(courtRoundNumber, courtNumber);
                        if (game == null) continue;
                        RobinRound gameRobinRound = Tournament.RobinRounds[game.RobinRoundNum - 1];

                        //Calculate needed line segments for separator
                        bool isLastTotalRobinRound = game.RobinRoundNum == Tournament.TotalRobinRounds;
                        Game gameOnBottom = Tournament.getGame(game.CourtRoundNum + 1, game.CourtNumber);
                        bool shouldDrawLineOnBottom = (gameOnBottom != null && gameOnBottom.RobinRoundNum > game.RobinRoundNum) || (gameOnBottom == null && !isLastTotalRobinRound);
                        bool shouldDrawCheckboxSegment = game.RobinRoundNum < Tournament.TotalRobinRounds && (game == gameRobinRound.Games[gameRobinRound.numGames - 1]);

                        //Add needed points to separator
                        if (shouldDrawLineOnBottom || shouldDrawCheckboxSegment)
                        {
                            if (robinRoundsSeparatorLines.Count < game.RobinRoundNum)
                                robinRoundsSeparatorLines.Add(new List<PointF>());
                            List<PointF> robinRoundSeparatorLine = robinRoundsSeparatorLines[game.RobinRoundNum - 1];
                            RectangleF gameRectangle = gameRectanglesByGame[game];

                            if (shouldDrawLineOnBottom)
                            {
                                PointF p1 = new PointF(gameRectangle.Left, gameRectangle.Bottom);
                                PointF p2 = new PointF(gameRectangle.Right, gameRectangle.Bottom);

                                if (!robinRoundSeparatorLine.Contains(p1)) robinRoundSeparatorLine.Add(p1);
                                if (!robinRoundSeparatorLine.Contains(p2)) robinRoundSeparatorLine.Add(p2);
                            }

                            //Add line segment for check box if this is the last game in the robin  round
                            if (shouldDrawCheckboxSegment)
                            {
                                robinRoundSeparatorLine.InsertRange(0,
                                    new PointF[]{
                                        new PointF(0, gameRectangle.Bottom),
                                        new PointF(gameRectangles[game.CourtRoundNum - 1, 0].X, gameRectangle.Bottom)
                                    });
                            }
                        }
                    }
                }
                #endregion

               #region Draw separator lines
                //Draw separator lines
                foreach (List<PointF> separatorLine in robinRoundsSeparatorLines)
                {
                    PointF[] separatorLineArray = separatorLine.ToArray();
                    graphics.DrawLines(robinRoundSeparatorPen, separatorLineArray);

                    //Calculate and draw the outlines
                    int separatorOutlineOffset = (int)Math.Ceiling(robinRoundSeparatorPen.Width / 2);
                    for (int i = 0; i < robinRoundSeparatorOutlinePens.Count; i++)
                    {
                        //Calculate the outline
                        PointF[] separatorOutine1Array = new PointF[separatorLineArray.Length];
                        PointF[] separatorOutine2Array = new PointF[separatorLineArray.Length];

                        for (int j = 0; j < separatorLineArray.Length; j++)
                        {
                            PointF outline1CurrentPoint = separatorLineArray[j];
                            PointF outline2CurrentPoint = separatorLineArray[j];

                            PointF? outline1PreviousPoint = null;
                            PointF? outline2PreviousPoint = null;
                            PointF? outline1NextPoint = null;
                            PointF? outline2NextPoint = null;
                            if (j != 0)
                            {
                                outline1PreviousPoint = separatorLineArray[j - 1];
                                outline2PreviousPoint = separatorLineArray[j - 1];
                            }
                            if (j < separatorLineArray.Length - 1)
                            {
                                outline1NextPoint = separatorLineArray[j + 1];
                                outline2NextPoint = separatorLineArray[j + 1];
                            }
                            if (
                                (outline1PreviousPoint != null && ((PointF)outline1PreviousPoint).Y < outline1CurrentPoint.Y) ||
                                (outline1NextPoint != null && ((PointF)outline1NextPoint).Y > outline1CurrentPoint.Y))
                                outline1CurrentPoint.X += separatorOutlineOffset;
                            else if (
                                (outline1PreviousPoint != null && ((PointF)outline1PreviousPoint).Y > outline1CurrentPoint.Y) ||
                                (outline1NextPoint != null && ((PointF)outline1NextPoint).Y < outline1CurrentPoint.Y))
                                outline1CurrentPoint.X -= separatorOutlineOffset;

                            if (
                                (outline2PreviousPoint != null && ((PointF)outline2PreviousPoint).Y < outline2CurrentPoint.Y) ||
                                (outline2NextPoint != null && ((PointF)outline2NextPoint).Y > outline2CurrentPoint.Y))
                                outline2CurrentPoint.X -= separatorOutlineOffset;
                            else if (
                                (outline2PreviousPoint != null && ((PointF)outline2PreviousPoint).Y > outline2CurrentPoint.Y) ||
                                (outline2NextPoint != null && ((PointF)outline2NextPoint).Y < outline2CurrentPoint.Y))
                                outline2CurrentPoint.X += separatorOutlineOffset;

                            outline1CurrentPoint.Y -= separatorOutlineOffset;
                            outline2CurrentPoint.Y += separatorOutlineOffset;

                            separatorOutine1Array[j] = outline1CurrentPoint;
                            separatorOutine2Array[j] = outline2CurrentPoint;
                        }

                        //Draw the outline
                        Pen separatorOutlinePen = robinRoundSeparatorOutlinePens[i];
                        graphics.DrawLines(separatorOutlinePen, separatorOutine1Array);
                        graphics.DrawLines(separatorOutlinePen, separatorOutine2Array);

                        separatorOutlineOffset += (int)Math.Ceiling(separatorOutlinePen.Width / 2);
                    }
                }
                #endregion 
                #endregion

                // Selected game outline
                if (!currentSelectedGameRectangle.IsEmpty)
                {
                    Rectangle outlineRectangle = RectangleFToRectangle(currentSelectedGameRectangle);
                    outlineRectangle.Width -= (int)selectedGamePen.Width;
                    outlineRectangle.Height -= (int)selectedGamePen.Width;
                    graphics.DrawRectangle(selectedGamePen, outlineRectangle);
                }
            }

            btnMoreRounds.Top = drawTop + btnMoreRounds.Margin.Top;
            btnMoreRounds.Left = (courtRoundsPaintable.Width - btnMoreRounds.Width) / 2;
            courtRoundsPaintable.Height = btnMoreRounds.Bottom + btnMoreRounds.Margin.Bottom + courtRoundsPaintable.Padding.Bottom;
            refreshCheckboxSizing();

            scrollingPanel.ResumeLayout();
        }

        private void ScheduleDisplay_Load(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void courtRoundsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameRectangles == null) return;

            Game ClickedGame = null;
            RectangleF ClickedGameRectangle = new RectangleF();

            PointF clickPoint = new PointF(e.Location.X, e.Location.Y);
            foreach (KeyValuePair<Game, RectangleF> pair in gameRectanglesByGame)
            {
                if (pair.Value.Contains(clickPoint))
                {
                    ClickedGameRectangle = pair.Value;
                    ClickedGame = pair.Key;
                    break;
                }
            }

            //We figured out what game was clicked on
            if (ClickedGame != null)
            {
                startEditingGame(ClickedGame);
            }
        }

        private void startEditingGame(Game game)
        {
            startEditingGame(game, this.gameRectanglesByGame[game]);
        }

        private void startEditingGame(Game game, RectangleF gameRectangle)
        {
            //Calculate position
            int scoreEditorTop = courtRoundsPaintable.Top;
            scoreEditorTop += (int)(gameRectangle.Top + (gameRectangle.Height - scoreEditor.Height) / 2);
            scoreEditorTop = Math.Min(courtRoundsPaintable.Height - scoreEditor.Height, scoreEditorTop);
            scoreEditorTop = Math.Max(0, scoreEditorTop);

            int scoreEditorLeft = courtRoundsPaintable.Left;
            scoreEditorLeft += (int)(gameRectangle.Left + (gameRectangle.Width - scoreEditor.Width) / 2);
            scoreEditorLeft = Math.Min(drawWidth - scoreEditor.Width, scoreEditorLeft);
            scoreEditorLeft = Math.Max(0, scoreEditorLeft);


            //Set position
            scoreEditor.Top = scoreEditorTop;
            scoreEditor.Left = scoreEditorLeft;
            scoreEditor.beginEdit(game);

            scrollingPanel.ScrollControlIntoView(scoreEditor);
        }

        private void courtRoundsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameRectangles == null) return;

            Game HoveredGame = null;
            RectangleF HoveredGameRectangle = new RectangleF();

            //Region redrawRegion = null;

            PointF mouseLocation = new PointF(e.Location.X, e.Location.Y);
            foreach (KeyValuePair<Game, RectangleF> pair in gameRectanglesByGame)
            {
                if (pair.Value.Contains(mouseLocation))
                {
                    HoveredGameRectangle = pair.Value;
                    HoveredGame = pair.Key;
                    break;
                }
            }

            if (currentHoveredGame != null && currentHoveredGame != HoveredGame)
            {
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
                //if (redrawRegion == null) redrawRegion = new Region(currentHoveredGameRectangle);
                //else redrawRegion.Union(currentHoveredGameRectangle);
            }

            //We figured out what game the mouse is over
            if (HoveredGame != null)
            {
                if (currentHoveredGame != HoveredGame)
                {
                    currentHoveredGame = HoveredGame;
                    currentHoveredGameRectangle = HoveredGameRectangle;
                    courtRoundsPaintable.Invalidate(RectangleFToRectangle(HoveredGameRectangle));
                    //if (redrawRegion == null) redrawRegion = new Region(HoveredGameRectangle);
                    //else redrawRegion.Union(HoveredGameRectangle);
                    
                    courtRoundsPaintable.Cursor = Cursors.Hand;
                }
            }
            else
            {
                currentHoveredGame = null;
                currentHoveredGameRectangle = new RectangleF();
                courtRoundsPaintable.Cursor = Cursors.Default;
            }

            //Redraw if needed
           // if (redrawRegion != null) courtRoundsPanel.Invalidate(redrawRegion);
        }
        protected static Rectangle RectangleFToRectangle(RectangleF r)
        {
            return new Rectangle((int)Math.Round(r.X), (int)Math.Round(r.Y), (int)Math.Round(r.Width), (int)Math.Round(r.Height));
        }

        private void courtRoundsPanel_MouseLeave(object sender, EventArgs e)
        {
            if (currentHoveredGame != null)
            {
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
                currentHoveredGame = null;
                currentHoveredGameRectangle = new RectangleF();
            }
        }

        private void courtRoundsPanel_Leave(object sender, EventArgs e)
        {
            if (currentHoveredGame != null)
            {
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
                currentHoveredGame = null;
                currentHoveredGameRectangle = new RectangleF();
            }

            if (currentSelectedGame != null)
            {
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentSelectedGameRectangle));
                currentSelectedGame = null;
                currentSelectedGameRectangle = new RectangleF();
                currentSelectedGameIndex = -1;
            }
        }

        private void courtRoundsPaintable_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if((e.Modifiers & Keys.Shift) == Keys.Shift){
                    this.currentSelectedGameIndex--;
                }
                else
                {
                    this.currentSelectedGameIndex++;
                }

                if (Tournament.Games.Count > this.currentSelectedGameIndex && this.currentSelectedGameIndex >= 0)
                {
                    selectGameByIndex(this.currentSelectedGameIndex);
                    e.IsInputKey = true;
                }
            }
        }

        private void selectGameByIndex(int gameIndex)
        {
            if (Tournament.Games.Count < gameIndex || gameIndex < 0)
            {
                return;
            }

            this.currentSelectedGameIndex = gameIndex;
            this.currentSelectedGame = Tournament.Games[gameIndex];
            if (!currentSelectedGameRectangle.IsEmpty)
            {
                // Clear the old rectangke
                courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentSelectedGameRectangle));
            }
            if (this.currentSelectedGame != null && this.gameRectanglesByGame.ContainsKey(this.currentSelectedGame))
            {
                this.currentSelectedGameRectangle = this.gameRectanglesByGame[this.currentSelectedGame];
            }
            else
            {
                this.currentSelectedGameRectangle = new RectangleF();
            }
            courtRoundsPaintable.Invalidate(RectangleFToRectangle(currentSelectedGameRectangle));

            // Make sure the game is visible.
            int scrollY = Math.Abs(scrollingPanel.AutoScrollPosition.Y);
            if (scrollY > currentSelectedGameRectangle.Top)
            {
                scrollY = (int)currentSelectedGameRectangle.Top;
            }
            else if (scrollY < currentSelectedGameRectangle.Bottom - scrollingPanel.Height)
            {
                scrollY = (int)currentSelectedGameRectangle.Bottom - scrollingPanel.Height;
            }
            scrollingPanel.AutoScrollPosition = new Point(Math.Abs(scrollingPanel.AutoScrollPosition.X), scrollY);
        }

        private void courtRoundsPaintable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (currentSelectedGame != null && !scoreEditor.Visible)
                {
                    this.startEditingGame(currentSelectedGame);
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == 'F' || e.KeyChar == 'f')
            {
                if (currentSelectedGame != null && !scoreEditor.Visible)
                {
                    currentSelectedGame.IsConfirmed = !currentSelectedGame.IsConfirmed;
                    Controller.TriggerGameResultChanged(currentSelectedGame);
                    e.Handled = true;
                }
            }
        }

        private void scoreEditor_scoreEditorClosed(object sender, ScoreEditorClosedEventArgs e)
        {
            if (scoreEditor.Game != null)
            {
                this.selectGameByIndex(Tournament.Games.IndexOf(scoreEditor.Game));
            }
            scrollingPanel.AutoScrollPosition = new Point(Math.Abs(scrollingPanel.AutoScrollPosition.X), Math.Abs(tempScrollPosition.Y));
        }

        private void btnMoreRounds_Click(object sender, EventArgs e)
        {
            if (Tournament != null)
            {
                Tournament.Cycles++;
                Tournament.AutoCalculateNumRobinRounds = true;
                _showHideRobinRoundValues.Clear();
                resetRobinRoundCheckboxes();
                refreshSizing();
                Refresh();
            }
        }

        private void scoreEditor_scoreEditorClosing(object sender, ScoreEditorClosingEventArgs e)
        {
            tempScrollPosition = scrollingPanel.AutoScrollPosition;
        }
    }
}