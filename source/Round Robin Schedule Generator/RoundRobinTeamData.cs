using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class RoundRobinTeamData
    {
        protected Team _team;
        [XmlElement("Team", typeof(Team))]
        public Team Team
        {
            get
            {
                return _team;
            }
            set
            {
                _team = value;
            }
        }

        [XmlIgnore()]
        public bool IsBye
        {
            get
            {
                return Team is ByeTeam;
            }
        }



        protected int _scheduleVersion = -1;
        [XmlIgnore()]
        public int ScheduleVersion
        {
            get
            {
                return _scheduleVersion;
            }
        }

        [XmlIgnore()]
        protected int _numGamesWon = -1;
        public int NumGamesWon
        {
            get
            {

                if (_numGamesWon == -1)
                {
                    int numGamesWon = 0;
                    foreach (Game game in PlayedGames)
                    {
                        if (game.TeamGameResults[Team.Id].WonGame)
                        {
                            numGamesWon++;
                        }
                    }
                    _numGamesWon = numGamesWon;
                }

                return _numGamesWon;
            }
        }

        [XmlIgnore()]
        protected decimal _percentGamesWon = -1;
        public decimal PercentGamesWon
        {
            get
            {

                if (_percentGamesWon == -1)
                {
                    decimal percentGamesWon = 0;
                    if (PlayedGames.Length > 0)
                    {
                        percentGamesWon = (decimal)NumGamesWon / (decimal)PlayedGames.Length;
                    }

                    _percentGamesWon = percentGamesWon;
                }

                return _percentGamesWon;
            }
        }

        [XmlIgnore()]
        protected int _totalScore = -1;
        public int TotalScore
        {
            get
            {

                if (_totalScore == -1)
                {
                    int totalScore = 0;
                    foreach (Game game in PlayedGames)
                    {
                        totalScore += game.TeamGameResults[Team.Id].NumPoints;
                    }
                    _totalScore = totalScore;
                }

                return _totalScore;
            }
        }

        [XmlIgnore()]
        protected decimal _averageScore = -1;
        public decimal AverageScore
        {
            get
            {

                if (_averageScore == -1)
                {
                    decimal averageScore = 0;
                    if (PlayedGames.Length > 0)
                    {
                        averageScore = (decimal)TotalScore / (decimal)PlayedGames.Length;
                    }

                    _averageScore = averageScore;
                }

                return _averageScore;
            }
        }

        private List<Game> _playedGames = new List<Game>();
        private List<Game> _currentSchedulePlayedGames = new List<Game>();
        private int _currentScheduleVersion = -1;
        [XmlIgnore()]
        public Game[] PlayedGames
        {
            get
            {
                int version = Controller.GetController().Tournament.ScheduleVersion;
                List<Game> playedGamesForSchedule;
                if (_currentScheduleVersion != version)
                {
                    playedGamesForSchedule = new List<Game>();
                    foreach (Game game in _playedGames)
                    {
                        if (game.ScheduleVersion == version)
                        {
                            playedGamesForSchedule.Add(game);
                        }
                    }

                    _currentScheduleVersion = version;
                    _currentSchedulePlayedGames = playedGamesForSchedule;
                    resetStatistics();
                }
                else
                {
                    playedGamesForSchedule = _currentSchedulePlayedGames;
                }

                return playedGamesForSchedule.ToArray();
            }
            protected set
            {
                _playedGames = new List<Game>(value);
            }
        }

        protected List<int> _xmlPlayedGames;
        [XmlArray("PlayedGames"), XmlArrayItem("PlayedGame")]
        public int[] XmlSerializerPlayedGames
        {
            get
            {
                List<int> xmlPlayedGames = new List<int>();
                foreach (Game playedGame in PlayedGames)
                {
                    if (playedGame != null)
                    {
                        xmlPlayedGames.Add(playedGame.Id);
                    }
                    else
                    {
                        xmlPlayedGames.Add(-1);
                    }
                }
                return xmlPlayedGames.ToArray();
            }
            set
            {
                _xmlPlayedGames = new List<int>(value);
            }
        }
        [XmlIgnore()]
        public List<int> XmlPlayedGamesIn
        {
            get
            {
                return _xmlPlayedGames;
            }
        }

        public RoundRobinTeamData(Team team)
        {
            _team = team;
        }

        protected RoundRobinTeamData()
        {
        }

        public void resetStatistics()
        {
            _numGamesWon = -1;
            _percentGamesWon = -1;
            _totalScore = -1;
            _averageScore = -1;
            _scheduleVersion = Controller.GetController().Tournament.ScheduleVersion;
        }

        public void addPlayedGame(Game game)
        {
            _playedGames.Add(game);
            if (game.ScheduleVersion == _currentScheduleVersion)
            {
                _currentSchedulePlayedGames.Add(game);
            }
            resetStatistics();
        }

        public void removePlayedGame(Game game)
        {
            _playedGames.Remove(game);
            if (game.ScheduleVersion == _currentScheduleVersion)
            {
                _currentSchedulePlayedGames.Remove(game);
            }
            resetStatistics();
        }

        public override string ToString()
        {
            return Team.ToString();
        }
    }
}