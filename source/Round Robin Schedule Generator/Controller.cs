using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    public class Controller
    {
        protected static Controller _controller = null;
        public static Controller GetController()
        {
            if (_controller == null)
            {
                _controller = new Controller();
            }
            return _controller;
        }

        public event EventHandler NumCourtsChanged;
        protected int _numCourts = 5;
        public int NumCourts
        {
            get
            {
                return _numCourts;
            }
            set
            {
                _numCourts = value;
                if (_divisions != null) Tournament = new Tournament(_divisions, _numCourts);
                if (NumCourtsChanged != null) TriggerEvent(NumCourtsChanged,this, new EventArgs());
            }
        }

        public event EventHandler TournamentChanged;
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
                if (Tournament != null)
                {
                    _tournament.ScoreKeeperAccessCodes = ScoreKeeperAccessCodes;   
                }
                if (TournamentChanged != null) TriggerEvent(TournamentChanged,this, new EventArgs());
            }
        }

        public event EventHandler ScoreKeepersChanged;
        protected List<ScoreKeeper> _scoreKeepers = new List<ScoreKeeper>();
        public List<ScoreKeeper> ScoreKeepers
        {
            get
            {
                return _scoreKeepers;
            }
            set
            {
                if (value != null) _scoreKeepers = value;
                else _scoreKeepers = new List<ScoreKeeper>();
                if (Tournament != null)
                {
                    _tournament.ScoreKeepers = ScoreKeepers.ToArray();
                }
                TriggerScoreKeepersChanged();
            }
        }

        public event EventHandler ScoreKeeperAccessCodesChanged;
        protected List<string> _scoreKeeperAccessCodes = new List<string>();
        public List<string> ScoreKeeperAccessCodes
        {
            get
            {
                return _scoreKeeperAccessCodes;
            }
            set
            {
                _scoreKeeperAccessCodes = value;
                TriggerScoreKeeperAccessCodesChanged();
            }
        }

        protected Dictionary<GamePosition, ScoreKeeper> _scoreKeepersAssignment = new Dictionary<GamePosition, ScoreKeeper>();
        public Dictionary<GamePosition, ScoreKeeper> ScoreKeepersAssignment
        {
            get
            {
                return _scoreKeepersAssignment;
            }
            set
            {
                _scoreKeepersAssignment = value;
                if (Tournament != null)
                {
                    _tournament.ScoreKeepersAssignment = ScoreKeepersAssignment;
                }
                TriggerScoreKeepersAssignmentChanged();
            }
        }

        protected List<Division> _divisions = null;
        public Division[] Divisions
        {
            get
            {
                if (_divisions != null) return _divisions.ToArray();
                else return null;
            }
            set
            {
                _divisions = new List<Division>(value);
                Tournament = new Tournament(_divisions, NumCourts);
            }
        }

        protected Controller()
        {
            ScoreKeepersChanged += new EventHandler(Controller_ScoreKeepersChanged);
            ScoreKeeperAccessCodesChanged += new EventHandler(Controller_ScoreKeeperAccessCodesChanged);
            ScoreKeepersAssignmentChanged += new EventHandler(Controller_ScoreKeepersAssignmentChanged);
            TournamentChanged += new EventHandler(Controller_TournamentChanged);
        }

        void Controller_TournamentChanged(object sender, EventArgs e)
        {
            if (CourtRoundsIsActiveHasChanged()) TriggerEvent(CourtRoundsIsActiveChanged, this, new EventArgs());
            if (CourtRoundsIsConfirmedHasChanged()) TriggerEvent(CourtRoundsIsConfirmedChanged, this, new EventArgs());
        }

        void Controller_ScoreKeepersAssignmentChanged(object sender, EventArgs e)
        {
            Tournament.ScoreKeepersAssignment = ScoreKeepersAssignment;
        }

        void Controller_ScoreKeeperAccessCodesChanged(object sender, EventArgs e)
        {
            Tournament.ScoreKeeperAccessCodes = ScoreKeeperAccessCodes;
        }

        void Controller_ScoreKeepersChanged(object sender, EventArgs e)
        {
            Tournament.ScoreKeepers = ScoreKeepers.ToArray();
        }

        public event TeamNameChangedEventHandler TeamNameChanged;
        public void TriggerTeamNameChanged(Team team)
        {
            if (TeamNameChanged != null) TriggerEvent(TeamNameChanged, this, new TeamNameChangedEventArgs(team));
        }

        public event GameResultChangedEventHandler GameResultChanged;
        public void TriggerGameResultChanged(Game game)
        {
            if (GameResultChanged != null) TriggerEvent(GameResultChanged, this, new GameResultChangedEventArgs(game));
            if (CourtRoundsIsActiveHasChanged()) TriggerEvent(CourtRoundsIsActiveChanged, this, new EventArgs());
            if (CourtRoundsIsConfirmedHasChanged()) TriggerEvent(CourtRoundsIsConfirmedChanged, this, new EventArgs());
        }

        public event EventHandler CourtRoundsIsActiveChanged;
        protected Dictionary<CourtRound, bool> OldCourtRoundsIsActive = new Dictionary<CourtRound, bool>();
        protected bool CourtRoundsIsActiveHasChanged()
        {
            List<CourtRound> toRemove = new List<CourtRound>();
            foreach (KeyValuePair<CourtRound,bool> pair in OldCourtRoundsIsActive)
            {
                if (!Tournament.CourtRounds.Contains(pair.Key)) toRemove.Add(pair.Key);
            }
            foreach (CourtRound courtRound in toRemove)
            {
                OldCourtRoundsIsActive.Remove(courtRound);
            }
            bool hasChanged = false;
            foreach (CourtRound courtRound in Tournament.CourtRounds)
            {
                if (!OldCourtRoundsIsActive.ContainsKey(courtRound))
                {
                    OldCourtRoundsIsActive.Add(courtRound, courtRound.IsActive);
                    hasChanged = true;
                }
                else
                {
                    if (OldCourtRoundsIsActive[courtRound] != courtRound.IsActive)
                    {
                        OldCourtRoundsIsActive[courtRound] = courtRound.IsActive;
                        hasChanged = true;
                    }
                }
            }
            return hasChanged;
        }

        public event EventHandler CourtRoundsIsConfirmedChanged;
        protected Dictionary<CourtRound, bool> OldCourtRoundsIsConfirmed = new Dictionary<CourtRound, bool>();
        protected bool CourtRoundsIsConfirmedHasChanged()
        {
            List<CourtRound> toRemove = new List<CourtRound>();
            foreach (KeyValuePair<CourtRound, bool> pair in OldCourtRoundsIsConfirmed)
            {
                if (!Tournament.CourtRounds.Contains(pair.Key)) toRemove.Add(pair.Key);
            }
            foreach (CourtRound courtRound in toRemove)
            {
                OldCourtRoundsIsConfirmed.Remove(courtRound);
            }
            bool hasChanged = false;
            foreach (CourtRound courtRound in Tournament.CourtRounds)
            {
                if (!OldCourtRoundsIsConfirmed.ContainsKey(courtRound))
                {
                    OldCourtRoundsIsConfirmed.Add(courtRound, courtRound.IsConfirmed);
                    hasChanged = true;
                }
                else
                {
                    if (OldCourtRoundsIsConfirmed[courtRound] != courtRound.IsConfirmed)
                    {
                        OldCourtRoundsIsConfirmed[courtRound] = courtRound.IsConfirmed;
                        hasChanged = true;
                    }
                }
            }
            return hasChanged;
        }

        public event EventHandler ScoreKeepersAssignmentChanged;
        public void TriggerScoreKeepersAssignmentChanged()
        {
            if (ScoreKeepersAssignmentChanged != null) TriggerEvent(ScoreKeepersAssignmentChanged, this, new EventArgs());
        }

        public void TriggerScoreKeepersChanged()
        {
            if (ScoreKeepersChanged != null) TriggerEvent(ScoreKeepersChanged, this, new EventArgs());
        }

        public void TriggerScoreKeeperAccessCodesChanged()
        {
            if (ScoreKeeperAccessCodesChanged != null) TriggerEvent(ScoreKeeperAccessCodesChanged, this, new EventArgs());
        }

        public event ScoreKeeperNameChangedEventHandler ScoreKeeperNameChanged;
        public void TriggerScoreKeeperNameChanged(ScoreKeeper scoreKeeper)
        {
            if (ScoreKeeperNameChanged != null) TriggerEvent(ScoreKeeperNameChanged, this, new ScoreKeeperNameChangedEventArgs(scoreKeeper));
        }

        protected void TriggerEvent(Delegate e, params object[] args)
        {
            foreach (Delegate del in e.GetInvocationList())
            {
                System.ComponentModel.ISynchronizeInvoke syncer = del.Target as System.ComponentModel.ISynchronizeInvoke;
                if (syncer == null)
                {
                    del.DynamicInvoke(args);
                }
                else
                {
                    syncer.BeginInvoke(del, args);
                }
            }
        }

        public Dictionary<GamePosition, ScoreKeeper> GetScoreKeepersForCourtRound(int courtRoundNum)
        {
            Dictionary<GamePosition, ScoreKeeper> scoreKeepers = new Dictionary<GamePosition, ScoreKeeper>();
            foreach (KeyValuePair<GamePosition, ScoreKeeper> pair in ScoreKeepersAssignment)
            {
                if (pair.Key.CourtRoundNum == courtRoundNum) scoreKeepers.Add(pair.Key, pair.Value);
            }
            return scoreKeepers;
        }
    }

    public delegate void TeamNameChangedEventHandler(object sender, TeamNameChangedEventArgs e);
    public class TeamNameChangedEventArgs : EventArgs
    {
        protected Team _team;
        public Team Team {
            get
            {
                return _team;
            }
        }

        public TeamNameChangedEventArgs(Team team)
        {
            _team = team;
        }
    }

    public delegate void ScoreKeeperNameChangedEventHandler(object sender, ScoreKeeperNameChangedEventArgs e);
    public class ScoreKeeperNameChangedEventArgs : EventArgs
    {
        protected ScoreKeeper _scoreKeeper;
        public ScoreKeeper ScoreKeeper
        {
            get
            {
                return _scoreKeeper;
            }
        }

        public ScoreKeeperNameChangedEventArgs(ScoreKeeper scoreKeeper)
        {
            _scoreKeeper = scoreKeeper;
        }
    }

    public delegate void GameResultChangedEventHandler(object sender, GameResultChangedEventArgs e);
    public class GameResultChangedEventArgs : EventArgs
    {
        protected Game _game;
        public Game Game
        {
            get
            {
                return _game;
            }
        }

        public GameResultChangedEventArgs(Game game)
        {
            _game = game;
        }
    }
}