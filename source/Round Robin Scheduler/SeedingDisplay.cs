using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class SeedingDisplay : UserControl
    {
        protected int divisionWidth;
        protected int headerHeight = 0;
        protected float headerFontHeight;
        protected float dataFontHeight;
        protected int dataRowHeight;
        protected int drawWidth;
        protected Padding headerMargin = new Padding(0, 5, 0, 5);
        protected Padding dataRowMargin = new Padding(0, 5, 0, 5);

        protected Dictionary<Division,List<Team>> seedingCache;
        protected int seedingCacheVersion = -1;


        //Fonts
        Font headerFont;
        Font dataFont;

        Controller Controller = Controller.GetController();

        public Tournament Tournament
        {
            get
            {
                return Controller.Tournament;
            }
        }

        public SeedingDisplay()
        {
            InitializeComponent();
            calcuateFonts();
            Controller.TournamentChanged += new EventHandler(Controller_TournamentChanged);
            Controller.GameResultChanged += new GameResultChangedEventHandler(Controller_GameResultChanged);
            Controller.TeamNameChanged += new TeamNameChangedEventHandler(Controller_TeamNameChanged);
        }

        void Controller_TeamNameChanged(object sender, TeamNameChangedEventArgs e)
        {
            Refresh();
        }

        void Controller_GameResultChanged(object sender, GameResultChangedEventArgs e)
        {
            regenerateSeeding();
        }

        void Controller_TournamentChanged(object sender, EventArgs e)
        {
            if (Tournament != null)
            {
                refreshSizing();
                Refresh();
                regenerateSeeding();
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
        
        protected void calcuateFonts()
        {
            headerFont = new Font(Font, FontStyle.Bold);
            dataFont = new Font(Font, FontStyle.Regular);

            headerFontHeight = headerFont.GetHeight();
            dataFontHeight = dataFont.GetHeight();

            headerHeight = (int)headerFontHeight + headerMargin.Top + headerMargin.Bottom;
            dataRowHeight = (int)dataFontHeight + dataRowMargin.Top + dataRowMargin.Bottom;

            refreshSizing();
        }

        private void refreshSizing()
        {
            scrollingPanel.Top = headerHeight;
            scrollingPanel.Width = Width;
            scrollingPanel.Height = Height - headerHeight;

            Dictionary<Division, List<Team>> seeding = getSeeding();
            if (seeding == null) return;
            int maxSeedingLength = 0;
            foreach (KeyValuePair<Division, List<Team>> divisionSeeding in seeding)
            {
                if (divisionSeeding.Value.Count > maxSeedingLength) maxSeedingLength = divisionSeeding.Value.Count;
            }

            seedingPanel.Height = maxSeedingLength * dataRowHeight;

            //Calculate draw width (width minus scrollbars if applicable)
            drawWidth = Width;
            if (scrollingPanel.VerticalScroll.Visible) drawWidth -= SystemInformation.VerticalScrollBarWidth;

            divisionWidth = (int)Math.Floor((decimal)drawWidth / seeding.Count);
        }

        private void SeedingDisplay_Load(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void SeedingDisplay_Resize(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private Dictionary<Division, List<Team>> getSeeding()
        {
            if (Tournament!=null && seedingCacheVersion != Tournament.ScheduleVersion)
            {
                regenerateSeeding(false);
                seedingCacheVersion = Tournament.ScheduleVersion;
            }

            return seedingCache;
        }
        private void regenerateSeeding(bool repaint = true)
        {
            if (Tournament == null) return;
            seedingCache = Tournament.CalculateSeedingByDivisions();

            if (repaint)
            {
                refreshSizing();

                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            Dictionary<Division, List<Team>> seeding = getSeeding();

            if (seeding == null) return;

            int drawTop = 0;
            int drawLeft;

            //Header
            drawLeft = 0;
            StringFormat headerStringFormat = new StringFormat();
            headerStringFormat.Alignment = StringAlignment.Center;
            headerStringFormat.LineAlignment = StringAlignment.Center;

            //Division headers
            foreach (KeyValuePair<Division, List<Team>> division in seeding)
            {
                string headerTitle = division.Key.Name;
                RectangleF divisionHeaderRect =
                new RectangleF(
                    drawLeft,
                    drawTop,
                    divisionWidth,
                    headerHeight);
                e.Graphics.DrawString(headerTitle, headerFont, new SolidBrush(ForeColor), divisionHeaderRect, headerStringFormat);
               // e.Graphics.DrawLine(headerHighlightPen, new PointF(courtHeaderRect.Left, courtHeaderRect.Top), new PointF(courtHeaderRect.Left, courtHeaderRect.Bottom - 1));
                //e.Graphics.DrawLine(headerShadowPen, new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Top), new PointF(courtHeaderRect.Right - 1, courtHeaderRect.Bottom - 1));
                drawLeft += divisionWidth;
            }
            //e.Graphics.DrawLine(headerShadowPen, new PointF(0, roundHeaderRect.Bottom - 1), new PointF(drawWidth, roundHeaderRect.Bottom - 1));
        }

        private void seedingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(seedingPanel.BackColor);

            Dictionary<Division, List<Team>> seeding = getSeeding();

            if (seeding == null) return;

            StringFormat dataStringFormat = new StringFormat();
            dataStringFormat.Alignment = StringAlignment.Near;
            dataStringFormat.LineAlignment = StringAlignment.Center;
            dataStringFormat.FormatFlags = StringFormatFlags.NoWrap;
            dataStringFormat.Trimming = StringTrimming.EllipsisCharacter;

            int drawLeft = 0;
            int drawTop;
            foreach (KeyValuePair<Division, List<Team>> divisionSeeding in seeding)
            {
                drawTop = 0;
                for (int i = 0; i < divisionSeeding.Value.Count;i++ )
                {
                    Team team = divisionSeeding.Value[i];
                    RectangleF dataRect =
                        new RectangleF(
                            drawLeft,
                            drawTop,
                            divisionWidth,
                            dataRowHeight);

                    string id = team.Id;
                    string name = team.Name;
                    string text = "";

                    if (id != name)
                    {
                        text = String.Format("{0}. {1} - {2}", i + 1, id, name);
                    }
                    else
                    {
                        text = String.Format("{0}. {1}", i + 1, id);
                    }

                    e.Graphics.DrawString(text, dataFont, new SolidBrush(ForeColor), dataRect, dataStringFormat);

                    drawTop += dataRowHeight;
                }

                drawLeft += divisionWidth;
            }
        }
    }
}
