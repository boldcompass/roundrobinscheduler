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
    public partial class ScoreKeepersScheduleDisplay : UserControl
    {
        protected int headerTop;
        protected int headerHeight = 20;
        protected const int defaultRoundColumnWidth = 55;
        protected int roundColumnWidth = 50;
        protected int courtRoundHeight = 30;
        protected int courtColumnWidth;
        protected int drawWidth;
        protected Padding gamePadding = new Padding(5);
               
        //Colors
        protected Brush textBrush;
        protected Brush scoreKeeperDuplicateTextBrush;
        protected Brush scoreKeeperNotAssignedTextBrush;
        protected Pen headerHighlightPen;
        protected Pen headerShadowPen;
        protected Pen gamesSeparatorPen;
        protected Color gameScoreKeeperAssignedFillColor;
        protected Color gameDuplicateScoreKeeperFillColor;
        protected Color gameScoreKeeperNotAssignedFillColor;
        protected Color gameHoverOverlayColor;
        
        //Fonts
        protected Font headerFont;
        protected Font roundNumberFont;
        protected Font scoreKeeperNameFont;

        protected Controller Controller = Controller.GetController();

        protected RectangleF[,] gameRectangles = null;

        protected GamePosition currentHoveredGamePosition = null;
        protected RectangleF currentHoveredGameRectangle = new RectangleF();

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

        public int NumRounds
        {
            get
            {
                return (int)numRoundsUpDwn.Value;
            }
            set
            {
                numRoundsUpDwn.Value = value;
            }
        }

        public ScoreKeepersScheduleDisplay()
        {
            InitializeComponent();

            calcuateFonts();
            calculateColors();

            Controller.NumCourtsChanged += new EventHandler(Controller_NumCourtsChanged);
            Controller.ScoreKeepersChanged += new EventHandler(Controller_ScoreKeepersChanged);
            Controller.ScoreKeeperNameChanged += new ScoreKeeperNameChangedEventHandler(Controller_ScoreKeeperNameChanged);
            Controller.TournamentChanged += new EventHandler(Controller_TournamentChanged);
        }

        void Controller_TournamentChanged(object sender, EventArgs e)
        {
            if (Tournament.CourtRounds.Count > numRoundsUpDwn.Value) numRoundsUpDwn.Value = Tournament.CourtRounds.Count;
        }

        void Controller_NumCourtsChanged(object sender, EventArgs e)
        {
            refreshSizing();
            Refresh();
        }

        void Controller_ScoreKeeperNameChanged(object sender, ScoreKeeperNameChangedEventArgs e)
        {
            UpdateScoreKeepersSelector();
            Refresh();
        }

        void Controller_ScoreKeepersChanged(object sender, EventArgs e)
        {
            UpdateScoreKeepersSelector();
            Refresh();
        }

        void UpdateScoreKeepersSelector()
        {
            assignScoreKeeperCmb.Items.Clear();
            assignScoreKeeperCmb.Items.Add("Clear Assignment");
            assignScoreKeeperCmb.Items.AddRange(Controller.ScoreKeepers.ToArray());
        }

        protected void calcuateFonts()
        {
            headerFont = new Font(Font, FontStyle.Bold);
            roundNumberFont = new Font(Font, FontStyle.Bold);
            scoreKeeperNameFont = new Font(Font, FontStyle.Regular);
        }

        protected void calculateColors()
        {
            //Brushes
            textBrush = new SolidBrush(ForeColor);
            scoreKeeperDuplicateTextBrush = new SolidBrush(Color.Red);
            scoreKeeperNotAssignedTextBrush = new SolidBrush(Color.DarkGray);
            
            //Pens
            headerHighlightPen = new Pen(SystemColors.ControlLightLight, 1);
            headerShadowPen = new Pen(SystemColors.ControlDark, 1);
            gamesSeparatorPen = new Pen(Color.LightGray);

            //Colors
            gameScoreKeeperAssignedFillColor = Color.White;
            gameScoreKeeperNotAssignedFillColor = Color.WhiteSmoke;
            gameDuplicateScoreKeeperFillColor = Color.LightPink;
            gameHoverOverlayColor = Color.FromArgb(75,Color.LightGray);
        }


        protected override void  OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            int drawTop = headerTop;
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
            e.Graphics.DrawString("Round", headerFont, textBrush, roundHeaderRect, headerStringFormat);
            e.Graphics.DrawLine(headerHighlightPen, new PointF(roundHeaderRect.Left, roundHeaderRect.Top), new PointF(roundHeaderRect.Left, roundHeaderRect.Bottom - 1));
            e.Graphics.DrawLine(headerShadowPen, new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Top), new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Bottom - 1));
            drawLeft += roundColumnWidth;

            //Court headers
            for (int courtNum = 0; courtNum < Controller.NumCourts; courtNum++)
            {
                char courtLetter = Convert.ToChar((courtNum) + 65);
                string headerTitle = String.Format("Court {0}", courtLetter);
                RectangleF courtHeaderRect =
                new RectangleF(
                    drawLeft,
                    drawTop,
                    courtColumnWidth,
                    headerHeight);
                e.Graphics.DrawString(headerTitle, headerFont, textBrush, courtHeaderRect, headerStringFormat);
                e.Graphics.DrawLine(headerHighlightPen, new PointF(courtHeaderRect.Left, courtHeaderRect.Top), new PointF(courtHeaderRect.Left, courtHeaderRect.Bottom - 1));
                e.Graphics.DrawLine(headerShadowPen, new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Top), new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Bottom - 1));
                drawLeft += courtColumnWidth;
            }
            e.Graphics.DrawLine(headerShadowPen, new PointF(0, roundHeaderRect.Bottom - 1), new PointF(drawWidth, roundHeaderRect.Bottom - 1));
        }

        private void refreshSizing()
        {
            courtRoundsPanel.Height = NumRounds * courtRoundHeight;

            headerTop = controlsPanel.Bottom;

            scrollingPanel.Top = headerTop + headerHeight;
            scrollingPanel.Width = Width;
            scrollingPanel.Height = Height - headerTop - headerHeight;

            //Calculate draw width (width minus scrollbars if applicable)
            drawWidth = Width;
            if (scrollingPanel.VerticalScroll.Visible) drawWidth -= SystemInformation.VerticalScrollBarWidth;

            courtColumnWidth = (int)Math.Floor((double)((drawWidth - defaultRoundColumnWidth) / Controller.NumCourts));
            roundColumnWidth = drawWidth - (courtColumnWidth * Controller.NumCourts);
        }
       
        private void ScheduleDisplay_Resize(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void courtRoundsPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clip = new Region(e.ClipRectangle);
            e.Graphics.Clear(courtRoundsPanel.BackColor);
            if (Tournament != null)
            {
                int drawTop = 0;
                int drawLeft = 0;

                gameRectangles = new RectangleF[NumRounds, Tournament.NumCourts];

                //Court rounds
                StringFormat roundNumberStringFormat = new StringFormat();
                roundNumberStringFormat.Alignment = StringAlignment.Center;
                roundNumberStringFormat.LineAlignment = StringAlignment.Center;

                for (int courtRoundNum = 0; courtRoundNum < NumRounds; courtRoundNum++)
                {
                    drawLeft = 0;

                    Dictionary<ScoreKeeper, int> timesScoreKeeperInCourtRound = new Dictionary<ScoreKeeper, int>();
                    foreach (KeyValuePair<GamePosition, ScoreKeeper> pair in Controller.GetScoreKeepersForCourtRound(courtRoundNum + 1))
                    {
                        if (!timesScoreKeeperInCourtRound.ContainsKey(pair.Value)) timesScoreKeeperInCourtRound.Add(pair.Value, 1);
                        else timesScoreKeeperInCourtRound[pair.Value]++;
                    }

                    //Round number
                    RectangleF roundNumberRect =
                       new RectangleF(
                           drawLeft,
                           drawTop,
                           roundColumnWidth,
                           courtRoundHeight);
                    RectangleF roundNumberTextRect = roundNumberRect;

                    e.Graphics.DrawString((courtRoundNum+1).ToString(), roundNumberFont, textBrush, roundNumberTextRect, roundNumberStringFormat);
                    e.Graphics.DrawLine(headerHighlightPen, new PointF(roundNumberRect.Left, roundNumberRect.Top), new PointF(roundNumberRect.Right - 1, roundNumberRect.Top));
                    e.Graphics.DrawLine(headerShadowPen, new PointF(roundNumberRect.Left, roundNumberRect.Bottom - 1), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                    e.Graphics.DrawLine(headerShadowPen, new PointF(roundNumberRect.Right - 1, roundNumberRect.Top), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                    drawLeft += roundColumnWidth;

                    //Games
                    for (int courtNum = 0; courtNum < Tournament.NumCourts; courtNum++)
                    {
                        GamePosition gamePosition = GamePosition.GetGamePosition(courtRoundNum + 1, courtNum + 1);
                        RectangleF gameRectangle =
                            new RectangleF(
                                    drawLeft,
                                    drawTop,
                                    courtColumnWidth,
                                    courtRoundHeight);
                        gameRectangles[courtRoundNum, courtNum] = gameRectangle;

                        string scoreKeeperName = "Not Assigned";
                        Color gameFillColor = gameScoreKeeperNotAssignedFillColor;
                        Brush scoreKeeperTextBrush = scoreKeeperNotAssignedTextBrush;
                        if (Controller.ScoreKeepersAssignment.ContainsKey(gamePosition))
                        {
                            ScoreKeeper scoreKeeper = Controller.ScoreKeepersAssignment[gamePosition];
                            scoreKeeperName = scoreKeeper.Name;
                            if (timesScoreKeeperInCourtRound[scoreKeeper] <= 1)
                            {
                                scoreKeeperTextBrush = textBrush;
                                gameFillColor = gameScoreKeeperAssignedFillColor;
                            }
                            //Score keeper is assigned to more than one in the same court round
                            else
                            {
                                scoreKeeperTextBrush = scoreKeeperDuplicateTextBrush;
                                gameFillColor = gameDuplicateScoreKeeperFillColor;
                            }

                        }
                        
                        e.Graphics.FillRectangle(new SolidBrush(gameFillColor), gameRectangle);
                        if (gamePosition == currentHoveredGamePosition)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(gameHoverOverlayColor), gameRectangle);
                        }

                        int gameWidth = courtColumnWidth - gamePadding.Top - gamePadding.Bottom;
                        int gameHeight = courtRoundHeight - gamePadding.Left - gamePadding.Right;

                        //Draw score keeper name
                        RectangleF scoreKeeperNameRect =
                           new RectangleF(
                                   gameRectangle.Left + gamePadding.Left,
                                   gameRectangle.Top + gamePadding.Top,
                                   gameRectangle.Width - gamePadding.Left - gamePadding.Right,
                                   gameRectangle.Height - gamePadding.Top - gamePadding.Bottom);
                        StringFormat scoreKeeperStringFormat = new StringFormat();
                        scoreKeeperStringFormat.Alignment = StringAlignment.Center;
                        scoreKeeperStringFormat.LineAlignment = StringAlignment.Center;
                        scoreKeeperStringFormat.Trimming = StringTrimming.EllipsisCharacter;

                        e.Graphics.DrawString(scoreKeeperName, scoreKeeperNameFont, scoreKeeperTextBrush, scoreKeeperNameRect, scoreKeeperStringFormat);


                        e.Graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Right - 1, gameRectangle.Top), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                        e.Graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Left, gameRectangle.Bottom - 1), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                        drawLeft += courtColumnWidth;
                    }
                    drawTop += courtRoundHeight;
                }
            }
        }

        private void ScheduleDisplay_Load(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void courtRoundsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameRectangles == null) return;

            GamePosition ClickedGamePosition = null;
            RectangleF ClickedGameRectangle = new RectangleF();

            PointF clickPoint = new PointF(e.Location.X, e.Location.Y);
            for (int courtRoundNum = 0; courtRoundNum < gameRectangles.GetLength(0); courtRoundNum++)
            {
                for (int courtNum = 0; courtNum < gameRectangles.GetLength(1); courtNum++)
                {
                    RectangleF rect = gameRectangles[courtRoundNum, courtNum];
                    if (rect.Contains(clickPoint))
                    {
                        ClickedGameRectangle = rect;
                        ClickedGamePosition = GamePosition.GetGamePosition(courtRoundNum + 1, courtNum + 1);
                        break;
                    }
                }
            }

            //We figured out what game was clicked on
            if (ClickedGamePosition != null)
            {
                if (assignScoreKeeperCmb.SelectedItem != null)
                {
                    if (assignScoreKeeperCmb.SelectedItem is ScoreKeeper)
                    {
                        ScoreKeeper currentScoreKeeper = (ScoreKeeper)assignScoreKeeperCmb.SelectedItem;
                        Controller.ScoreKeepersAssignment[ClickedGamePosition] = currentScoreKeeper;
                        Controller.TriggerScoreKeepersAssignmentChanged();
                    }
                    else
                    {
                        Controller.ScoreKeepersAssignment.Remove(ClickedGamePosition);
                        Controller.TriggerScoreKeepersAssignmentChanged();
                    }
                    //Redraw the court round
                    courtRoundsPanel.Invalidate(
                        new Rectangle(
                            0,
                            (int)ClickedGameRectangle.Y,
                            courtRoundsPanel.Width,
                            (int)ClickedGameRectangle.Height));
                }
            }
        }

        private void courtRoundsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameRectangles == null) return;

            GamePosition HoveredGamePosition = null;
            RectangleF HoveredGameRectangle = new RectangleF();

            PointF mouseLocation = new PointF(e.Location.X, e.Location.Y);
            for (int courtRoundNum = 0; courtRoundNum < gameRectangles.GetLength(0); courtRoundNum++)
            {
                for (int courtNum = 0; courtNum < gameRectangles.GetLength(1); courtNum++)
                {
                    RectangleF rect = gameRectangles[courtRoundNum, courtNum];
                    if (rect.Contains(mouseLocation))
                    {
                        HoveredGameRectangle = rect;
                        HoveredGamePosition = GamePosition.GetGamePosition(courtRoundNum + 1, courtNum + 1);
                        break;
                    }
                }
            }

            if (currentHoveredGamePosition != null && currentHoveredGamePosition != HoveredGamePosition)
            {
                courtRoundsPanel.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
            }

            //We figured out what game the mouse is over
            if (HoveredGamePosition != null)
            {
                if (currentHoveredGamePosition != HoveredGamePosition)
                {
                    currentHoveredGamePosition = HoveredGamePosition;
                    currentHoveredGameRectangle = HoveredGameRectangle;
                    courtRoundsPanel.Invalidate(RectangleFToRectangle(HoveredGameRectangle));
                    courtRoundsPanel.Cursor = Cursors.Hand;
                }
            }
            else
            {
                currentHoveredGamePosition = null;
                currentHoveredGameRectangle = new RectangleF();
                courtRoundsPanel.Cursor = Cursors.Default;
            }
        }
        protected static Rectangle RectangleFToRectangle(RectangleF r)
        {
            return new Rectangle((int)Math.Round(r.X), (int)Math.Round(r.Y), (int)Math.Round(r.Width), (int)Math.Round(r.Height));
        }

        private void courtRoundsPanel_MouseLeave(object sender, EventArgs e)
        {
            if (currentHoveredGamePosition != null)
            {
                courtRoundsPanel.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
                currentHoveredGamePosition = null;
                currentHoveredGameRectangle = new RectangleF();
            }
        }

        private void courtRoundsPanel_Leave(object sender, EventArgs e)
        {
            if (currentHoveredGamePosition != null)
            {
                courtRoundsPanel.Invalidate(RectangleFToRectangle(currentHoveredGameRectangle));
                currentHoveredGamePosition = null;
                currentHoveredGameRectangle = new RectangleF();
            }
        }

        private void numRoundsUpDwn_ValueChanged(object sender, EventArgs e)
        {
            refreshSizing();
            Refresh();
        }

        private void controlsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}