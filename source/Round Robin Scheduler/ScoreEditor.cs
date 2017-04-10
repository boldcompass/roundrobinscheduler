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
        public event ScoreEditorClosingEventHandler scoreEditorClosing;


        protected bool enableChangeTeams = false;

        public bool EnableChangeTeams
        {
            get
            {
                return this.enableChangeTeams;
            }
            set
            {
                this.enableChangeTeams = value;

                comboBoxTeam1.Visible = value;
                comboBoxTeam2.Visible = value;

                setTeamNames();
            }
        }

        protected Controller Controller = Controller.GetController();

        protected Team team1;
        protected Team team2;

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
                    team1 = value.Team1;
                    team2 = value.Team2;
                    TeamGameResult team1Results = value.TeamGameResults[team1.Id];
                    TeamGameResult team2Results = value.TeamGameResults[team2.Id];

                    // Team Comboboxes
                    comboBoxTeam1.Items.Clear();
                    comboBoxTeam2.Items.Clear();
                    foreach (Division division in Controller.Tournament.Divisions)
                    {
                        comboBoxTeam1.Items.AddRange(division.Teams.ToArray());
                        comboBoxTeam2.Items.AddRange(division.Teams.ToArray());
                    }

                    comboBoxTeam2.SelectedItem = team2;
                    comboBoxTeam1.SelectedItem = team1;

                    //Team names
                    setTeamNames();

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

        protected void setTeamNames()
        {
            if (!this.enableChangeTeams)
            {
                if(team1 != null) 
                {
                    if (team1.Id == team1.Name)
                    {
                        chkTeam1Winner.Text = team1.Name;
                    }
                    else
                    {
                        chkTeam1Winner.Text = "(" + team1.Id + ") " + team1.Name;
                    }
                }

                if(team2 != null)
                {
                    if (team2.Id == team2.Name)
                    {
                        chkTeam2Winner.Text = team2.Name;
                    }
                    else
                    {
                        chkTeam2Winner.Text = "(" + team2.Id + ") " + team2.Name;
                    }
                }
            }
            else
            {
                chkTeam1Winner.Text = "";
                chkTeam2Winner.Text = "";
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
            if (shouldSave && team1.Division != team2.Division)
            {
                MessageBox.Show("The teams must be in the same division.", "Division Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (shouldSave && team1 == team2)
            {
                MessageBox.Show("The two teams must be different.", "Duplicate Teams", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = shouldSave ? DialogResult.OK : DialogResult.Cancel;
            if (scoreEditorClosing != null) scoreEditorClosing(this, new ScoreEditorClosingEventArgs(result));
            this.Hide();
            if (shouldSave)
            {
                if (team1 != Game.Team1 || team2 != Game.Team2)
                {
                    // Create a new Game
                    Game.resetTeams(team1.RoundRobinData, team2.RoundRobinData);
                }

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
            if (scoreEditorClosed != null) scoreEditorClosed(this, new ScoreEditorClosedEventArgs(result));
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

        private void comboBoxTeam1_SelectedIndexChanged(object sender, EventArgs e)
        {
            team1 = (Team)comboBoxTeam1.SelectedItem;
            setTeamNames();
        }

        private void comboBoxTeam2_SelectedIndexChanged(object sender, EventArgs e)
        {
            team2 = (Team)comboBoxTeam2.SelectedItem;
            setTeamNames();
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

    public delegate void ScoreEditorClosingEventHandler(object sender, ScoreEditorClosingEventArgs e);
    public class ScoreEditorClosingEventArgs : EventArgs
    {
        protected DialogResult _result;
        public DialogResult Result
        {
            get
            {
                return _result;
            }
        }
        public ScoreEditorClosingEventArgs(DialogResult result)
        {
            _result = result;
        }
    }
}
