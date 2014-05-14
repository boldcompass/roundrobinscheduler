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
    public partial class ScoreEditor : UserControl
    {
        public event ScoreEditorClosedEventHandler scoreEditorClosed;

        protected Controller Controller = Controller.GetController();

        protected Game _game;
        public Game Game
        {
            get
            {
                return _game;
            }
            protected set
            {
                _game = value;


                if (value != null)
                {
                    TeamGameResult team1Results = value.TeamGameResults[value.Team1.Id];
                    TeamGameResult team2Results = value.TeamGameResults[value.Team2.Id];

                    //Team names
                    if (value.Team1.Id == value.Team1.Name)
                    {
                        chkTeam1Winner.Text = value.Team1.Name;
                    }
                    else
                    {
                        chkTeam1Winner.Text = "(" + value.Team1.Id + ") " + value.Team1.Name;
                    }

                    if (value.Team2.Id == value.Team2.Name)
                    {
                        chkTeam2Winner.Text = value.Team2.Name;
                    }
                    else
                    {
                        chkTeam2Winner.Text = "(" + value.Team2.Id + ") " + value.Team2.Name;
                    }

                    //Winner
                    chkTeam1Winner.Checked = team1Results.WonGame;
                    chkTeam2Winner.Checked = team2Results.WonGame;

                    //Number of points
                    txtTeam1PointsNum.IntValue = team1Results.NumPoints;
                    txtTeam2PointsNum.IntValue = team2Results.NumPoints;

                    //Number of fouls
                    txtTeam1FoulsNum.IntValue = team1Results.NumFouls;
                    txtTeam2FoulsNum.IntValue = team2Results.NumFouls;

                    //Is confirmed
                    chkConfirmed.Checked = value.IsConfirmed;
                }
            }
        }

        public ScoreEditor(Game game)
            : base()
        {
            Game = game;
        }

        public ScoreEditor()
        {
            InitializeComponent();
        }

        private void setTeamWon(Team team, bool teamWon)
        {
            if (teamWon)
            {
                Game.WinningTeam = team;
            }
            else if (Game.WinningTeam == team)
            {
                Game.WinningTeam = null;
            }
            Game.TeamGameResults[team.Id].WonGame = teamWon;
        }

        private void setTeamPoints(Team team, int numPoints)
        {
            Game.TeamGameResults[team.Id].NumPoints = numPoints;
        }

        private void setTeamFouls(Team team, int numFouls)
        {
            Game.TeamGameResults[team.Id].NumFouls = numFouls;
        }

        public void beginEdit(Game game)
        {
            if (Visible)
            {
                endEdit();
            }
            Game = game;
            this.Show();
            chkTeam1Winner.Focus();
        }

        public void endEdit(bool shouldSave = true)
        {
            this.Hide();
            if (shouldSave)
            {
                //Winner
                setTeamWon(Game.Teams[0], chkTeam1Winner.Checked);
                setTeamWon(Game.Teams[1], chkTeam2Winner.Checked);

                //Number of points
                setTeamPoints(Game.Teams[0], (int)txtTeam1PointsNum.IntValue);
                setTeamPoints(Game.Teams[1], (int)txtTeam2PointsNum.IntValue);

                //Number of fouls
                setTeamFouls(Game.Teams[0], (int)txtTeam1FoulsNum.IntValue);
                setTeamFouls(Game.Teams[1], (int)txtTeam2FoulsNum.IntValue);

                //Is confirmed
                Game.IsConfirmed = chkConfirmed.Checked;

                Controller.TriggerGameResultChanged(Game);
            }
            if (scoreEditorClosed != null) scoreEditorClosed(this, new ScoreEditorClosedEventArgs(shouldSave ? DialogResult.OK : DialogResult.Cancel));
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            endEdit(true);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            endEdit(false);
        }

        private void team1WinnerChk_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTeam1Winner.Checked)
            {
                chkTeam2Winner.Checked = false;
            }
        }

        private void team2WinnerChk_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTeam2Winner.Checked)
            {
                chkTeam1Winner.Checked = false;
            }
        }

        private void ScoreEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return) || e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                endEdit();
            }
        }

        private void txtNum_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }

    public delegate void ScoreEditorClosedEventHandler(object sender, ScoreEditorClosedEventArgs e);
    public class ScoreEditorClosedEventArgs : EventArgs
    {
        protected DialogResult _result;
        public DialogResult Result
        {
            get
            {
                return _result;
            }
        }
        public ScoreEditorClosedEventArgs(DialogResult result)
        {
            _result = result;
        }
    }
}
