using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler
{
    class TournamentPrintDocument:PrintDocument
    {
        protected int defaultNumCourts = 5;
        protected int headerHeight = 20;
        protected const int defaultRoundColumnWidth = 50;
        protected int roundColumnWidth = 50;
        protected int courtRoundHeight = 30;

        //Pens and brushes
        protected Brush textBrush = new SolidBrush(Color.Black);
        protected Brush headerBackgroundBrush = new SolidBrush(SystemColors.Control);
        protected Pen headerSeparatorPen = new Pen(SystemColors.ControlDark, 1);
        protected Pen gamesSeparatorPen = new Pen(Color.FromArgb(245, 230, 205), 1);

        protected int _courtRoundIndex = 0;

        private Font _font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        protected Tournament _tournament;
        public Tournament Tournament
        {
            get
            {
                return _tournament;
            }
            set
            {
                _tournament = value;
            }
        }

        public TournamentPrintDocument()
        {
        }

        public TournamentPrintDocument(Tournament tournament)
            : this()
        {
            Tournament = tournament;
        }

       

        protected override void OnBeginPrint(PrintEventArgs e) 
        {
            _courtRoundIndex = 0;
            base.OnBeginPrint(e);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            if (Tournament == null) return;

            Graphics gdiPage = e.Graphics;
            Rectangle marginBounds = e.MarginBounds;
            

            int drawTop = marginBounds.Top;
            int drawLeft;

            //Header
            drawLeft = marginBounds.Left;
            Font headerFont = new Font(Font, FontStyle.Bold);
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
            e.Graphics.FillRectangle(headerBackgroundBrush, roundHeaderRect);
            e.Graphics.DrawLine(headerSeparatorPen, new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Top), new PointF(roundHeaderRect.Right - 1, roundHeaderRect.Bottom - 1));
            e.Graphics.DrawString("Round", headerFont, textBrush, roundHeaderRect, headerStringFormat);
            drawLeft += roundColumnWidth;

            //Court headers
            int numCourts = Tournament.NumCourts;
            int courtColumnWidth = (int)Math.Floor((double)((marginBounds.Width - defaultRoundColumnWidth) / numCourts));
            
            for (int courtNum = 0; courtNum < numCourts; courtNum++)
            {
                char courtLetter = Convert.ToChar((courtNum) + 65);
                string headerTitle = String.Format("Court {0}", courtLetter);
                RectangleF courtHeaderRect =
                new RectangleF(
                    drawLeft,
                    drawTop,
                    courtColumnWidth,
                    headerHeight);
                e.Graphics.FillRectangle(headerBackgroundBrush, courtHeaderRect);
                e.Graphics.DrawLine(headerSeparatorPen, new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Top), new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Bottom - 1));
                e.Graphics.DrawString(headerTitle, headerFont, textBrush, courtHeaderRect, headerStringFormat);
               
                drawLeft += courtColumnWidth;
            }
            e.Graphics.DrawLine(headerSeparatorPen, new PointF(marginBounds.Left, roundHeaderRect.Bottom - 1), new PointF(marginBounds.Right, roundHeaderRect.Bottom - 1));
            drawTop += headerHeight;


            //Draw each court round
            Font roundNumberFont = new Font(Font, FontStyle.Bold);
            StringFormat roundNumberStringFormat = new StringFormat();
            roundNumberStringFormat.Alignment = StringAlignment.Center;
            roundNumberStringFormat.LineAlignment = StringAlignment.Far;
            while (drawTop + courtRoundHeight < marginBounds.Bottom && _courtRoundIndex<Tournament.CourtRounds.Count)
            {
                //Draw a single court round
                drawLeft = marginBounds.Left;

                CourtRound courtRound = Tournament.CourtRounds[_courtRoundIndex];

                //Round number
                int roundNumberBottomSpacing = 5;
                RectangleF roundNumberRect =
                   new RectangleF(
                       drawLeft,
                       drawTop,
                       roundColumnWidth,
                       courtRoundHeight);
                RectangleF roundNumberTextRect = roundNumberRect;
                roundNumberTextRect.Height -= roundNumberBottomSpacing;

                e.Graphics.FillRectangle(headerBackgroundBrush, roundNumberRect);
                e.Graphics.DrawLine(headerSeparatorPen, new PointF(roundNumberRect.Left, roundNumberRect.Bottom - 1), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                e.Graphics.DrawLine(headerSeparatorPen, new PointF(roundNumberRect.Right - 1, roundNumberRect.Top), new PointF(roundNumberRect.Right - 1, roundNumberRect.Bottom - 1));
                e.Graphics.DrawString(courtRound.RoundNumber.ToString(), roundNumberFont, textBrush, roundNumberTextRect, roundNumberStringFormat);
                drawLeft += roundColumnWidth;

                //Games
                foreach (Game game in courtRound.Games)
                {
                    RectangleF gameRectangle =
                        new RectangleF(
                                drawLeft,
                                drawTop,
                                courtColumnWidth,
                                courtRoundHeight);

                    if (game.Enabled)
                    {
                        Color gameFillColor = game.RobinRoundNum % 2 != 0 ? Color.FromArgb(255, 240, 215) : Color.White;
                        e.Graphics.FillRectangle(new SolidBrush(gameFillColor), gameRectangle);

                        int gamePadding = 2;
                        int gameWidth = courtColumnWidth - gamePadding * 2;
                        int gameHeight = courtRoundHeight - gamePadding * 2;

                        int teamDrawTop = drawTop + gamePadding;
                        int teamDrawLeft = drawLeft + gamePadding;
                        int teamDrawHeight = gameHeight / game.NumTeams;
                        foreach (Team team in game.Teams)
                        {
                            if (team != null)
                            {
                                if (team.Name != team.Id)
                                {
                                    //Team id
                                    StringFormat teamIdStringFormat = new StringFormat();
                                    teamIdStringFormat.Alignment = StringAlignment.Near;
                                    teamIdStringFormat.LineAlignment = StringAlignment.Center;

                                    Font teamIdFont = new Font(Font, FontStyle.Bold);
                                    RectangleF teamIdRect =
                                        new RectangleF(
                                            drawLeft + 2,
                                            teamDrawTop,
                                            e.Graphics.MeasureString(team.Id, teamIdFont).Width,
                                            teamDrawHeight);
                                    e.Graphics.DrawString(team.Id + " ", teamIdFont, textBrush, teamIdRect, teamIdStringFormat);

                                    //Team name
                                    StringFormat teamNameStringFormat = new StringFormat();
                                    teamNameStringFormat.Alignment = StringAlignment.Near;
                                    teamNameStringFormat.LineAlignment = StringAlignment.Center;
                                    teamNameStringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                    teamNameStringFormat.Trimming = StringTrimming.EllipsisCharacter;

                                    Font teamNameFont = new Font(Font, FontStyle.Regular);
                                    RectangleF teamNameRect =
                                        new RectangleF(
                                            teamIdRect.Right,
                                            teamDrawTop,
                                            gameWidth - teamIdRect.Width,
                                            teamDrawHeight);
                                    e.Graphics.DrawString(team.Name, teamNameFont, textBrush, teamNameRect, teamNameStringFormat);
                                }
                                else
                                {
                                    //Team id
                                    StringFormat teamIdStringFormat = new StringFormat();
                                    teamIdStringFormat.Alignment = StringAlignment.Near;
                                    teamIdStringFormat.LineAlignment = StringAlignment.Center;

                                    Font teamIdFont = new Font(Font, FontStyle.Bold);
                                    RectangleF teamIdRect =
                                        new RectangleF(
                                            drawLeft,
                                            teamDrawTop,
                                            gameWidth,
                                            teamDrawHeight);
                                    e.Graphics.DrawString(team.Id, teamIdFont, textBrush, teamIdRect, teamIdStringFormat);
                                }
                            }
                            e.Graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Right - 1, gameRectangle.Top), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                            e.Graphics.DrawLine(gamesSeparatorPen, new PointF(gameRectangle.Left, gameRectangle.Bottom - 1), new PointF(gameRectangle.Right - 1, gameRectangle.Bottom - 1));
                            teamDrawTop += teamDrawHeight;
                        }
                    }

                    drawLeft += courtColumnWidth;
                }


                //Prepare for next court round
                drawTop += courtRoundHeight;
                _courtRoundIndex++;
            }

            //If more court rounds exist, print another page.
            if (_courtRoundIndex < Tournament.CourtRounds.Count - 1)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
    }
}
