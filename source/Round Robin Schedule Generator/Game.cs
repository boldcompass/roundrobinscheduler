using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class Game
    {
        protected RoundRobinTeamData _team1Data;
        [XmlIgnore()]
        public RoundRobinTeamData Team1Data
        {
            get
            {
                return _team1Data;
            }
        }

        [XmlIgnore()]
        protected string _vsID;
        public string vsID
        {
            get
            {
                return CalculateVsId(Team1, Team2);
            }
        }

        protected static int _idPos = 0;
        protected int _id = _idPos++;
        [XmlAttribute("Id")]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                if (value > _idPos) _idPos = value;
            }
        }

        protected int _scheduleVersion = 0;
        [XmlAttribute("ScheduleVersion")]
        public int ScheduleVersion
        {
            get
            {
                return _scheduleVersion;
            }
            set
            {
                _scheduleVersion = value;
            }
        }

        [XmlIgnore()]
        public Team Team1
        {
            get
            {
                if (Team1Data != null)
                {
                    return Team1Data.Team;
                }
                else
                {
                    return null;
                }
            }
        }

        protected RoundRobinTeamData _team2Data;
        [XmlIgnore()]
        public RoundRobinTeamData Team2Data
        {
            get
            {
                return _team2Data;
            }
        }

        [XmlIgnore()]
        public Team Team2
        {
            get
            {
                if (Team2Data != null)
                {
                    return Team2Data.Team;
                }
                else
                {
                    return null;
                }
            }
        }

        [XmlIgnore()]
        Team[] _teams = null;
        public Team[] Teams
        {
            get
            {
                return _teams;
            }
        }

        [XmlIgnore()]
        Division _division = null;
        public Division Division
        {
            get
            {
                return _division;
            }
        }

        [XmlIgnore()]
        public int NumTeams
        {
            get
            {
                return 2;
            }
        }

        [XmlIgnore()]
        public List<RoundRobinTeamData> TeamDatas
        {
            get
            {
                List<RoundRobinTeamData> teamDatas = new List<RoundRobinTeamData>();
                teamDatas.Add(Team1Data);
                teamDatas.Add(Team2Data);
                return teamDatas;
            }
            set
            {
                if (value.Count >= 2)
                {
                    _team1Data = value[0];
                    _team2Data = value[1];
                    _teams = new Team[] { Team1, Team2 };
                    _division = Team1.Division;
                }
            }
        }

        protected Dictionary<string, TeamGameResult> _teamGameResults = new Dictionary<string, TeamGameResult>();
        [XmlIgnore()]
        public Dictionary<string, TeamGameResult> TeamGameResults
        {
            get
            {
                return _teamGameResults;
            }
            set
            {
                _teamGameResults = value;
            }
        }
        [XmlArray("TeamGameResults"), XmlArrayItem("TeamGameResult")]
        public SerializableKeyValuePair<string, TeamGameResult>[] XmlTeamGameResults
        {
            get
            {
                List<SerializableKeyValuePair<string, TeamGameResult>> xmlResults = new List<SerializableKeyValuePair<string, TeamGameResult>>();
                string[] keys = new string[TeamGameResults.Count];
                TeamGameResults.Keys.CopyTo(keys,0);
                TeamGameResult[] values = new TeamGameResult[TeamGameResults.Count];
                TeamGameResults.Values.CopyTo(values,0);

                for (int i = 0; i < TeamGameResults.Count; i++)
                {
                    xmlResults.Add(new SerializableKeyValuePair<string, TeamGameResult>(keys[i], values[i]));
                }
                return xmlResults.ToArray();
            }
            set
            {
                _teamGameResults.Clear();
                foreach (SerializableKeyValuePair<string, TeamGameResult> pair in value)
                {
                    _teamGameResults.Add(pair.Key, pair.Value);
                }
            }
        }

        protected List<string> _xmlTeams;
        [XmlArray("Teams"), XmlArrayItem("Team")]
        public string[] XmlSerializerTeams
        {
            get
            {
                List<string> xmlTeams = new List<string>();
                foreach (Team team in Teams)
                {
                    if (team != null)
                    {
                        xmlTeams.Add(team.Id);
                    }
                    else
                    {
                        xmlTeams.Add(null);
                    }
                }
                return xmlTeams.ToArray();
            }
            set
            {
                _xmlTeams = new List<string>(value);
            }
        }
        [XmlIgnore()]
        public string[] XmlTeamsIn
        {
            get
            {
                return _xmlTeams.ToArray();
            }
        }

        [XmlIgnore()]
        protected ScoreKeeper _scoreKeeper;
        public ScoreKeeper ScoreKeeper
        {
            get
            {
                Controller controller = Controller.GetController();
                if (controller.ScoreKeepersAssignment.ContainsKey(GamePosition)) return controller.ScoreKeepersAssignment[GamePosition];
                else return null;
            }
        }

        [XmlIgnore()]
        public GamePosition GamePosition
        {
            get
            {
                return GamePosition.GetGamePosition(CourtRoundNum, CourtNumber);
            }
        }

        [XmlIgnore()]
        public bool IsBye
        {
            get
            {
                return Team1Data.IsBye || Team2Data.IsBye;
            }
        }

        [XmlIgnore()]
        public bool IsInProgress
        {
            get
            {
                foreach (KeyValuePair<string, TeamGameResult> teamResult in TeamGameResults)
                {
                    if (teamResult.Value.NumPoints > 0)return true;
                }
                return false;
            }
        }

        protected int _courtRoundNum;
        [XmlAttribute("CourtRoundNum")]
        public int CourtRoundNum
        {
            get
            {
                return _courtRoundNum;
            }
            set
            {
                _courtRoundNum = value;
            }
        }

        protected int _robinRoundNum;
        [XmlAttribute("RobinRoundNum")]
        public int RobinRoundNum
        {
            get
            {
                return _robinRoundNum;
            }
            set
            {
                _robinRoundNum = value;
            }
        }

        protected int _courtNumber;
        [XmlAttribute("CourtNumber")]
        public int CourtNumber
        {
            get
            {
                return _courtNumber;
            }
            set
            {
                _courtNumber = value;
            }
        }

        protected bool _isConfirmed = true;
        [XmlAttribute("IsConfirmed")]
        public bool IsConfirmed
        {
            get
            {
                return _isConfirmed;
            }
            set
            {
                _isConfirmed = value;
            }
        }

        [XmlIgnore()]
        public bool IsCompleted
        {
            get
            {
                return _winningTeam != null;
            }
        }

        protected Team _winningTeam = null;
        [XmlIgnore()]
        public Team WinningTeam
        {
            get
            {
                return _winningTeam;
            }
            set
            {
                _winningTeam = value;
                if (_winningTeam == null)
                {
                    foreach (RoundRobinTeamData teamData in TeamDatas)
                    {
                        teamData.removePlayedGame(this);
                    }
                }
                else
                {
                    foreach (RoundRobinTeamData teamData in TeamDatas)
                    {
                        if (!(new List<Game>(teamData.PlayedGames).Contains(this)))
                        {
                            teamData.addPlayedGame(this);
                        }
                    }
                }
            }
        }

        protected string _xmlWinningTeam = null;
        [XmlAttribute("WinningTeam")]
        public string XmSerializerWinningTeam
        {
            get
            {
                if (WinningTeam != null)
                {
                    return WinningTeam.Id;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _xmlWinningTeam = value;
            }
        }
        public string XmlWinningTeamIn
        {
            get
            {
                return _xmlWinningTeam;
            }
        }

        public Game(RoundRobinTeamData team1Data, RoundRobinTeamData team2Data)
        {
            if (team1Data.Team.Division != team2Data.Team.Division) throw new Exception("Teams must be in the same division");
            _team1Data = team1Data;
            _team2Data = team2Data;
            _division = Team1.Division;
            _teams = new Team[] { Team1, Team2 };
            foreach (RoundRobinTeamData teamData in TeamDatas)
            {
                _teamGameResults.Add(teamData.Team.Id, new TeamGameResult(teamData));
            }
        }

        protected Game()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} vs {1}", Team1.ToString(), Team2.ToString());
        }

        public static string CalculateVsId(Team team1, Team team2)
        {
            return team1.NumId.ToString() + "," + team2.NumId.ToString();
        }

        public Game Clone()
        {
            Game game = new Game(Team1Data, Team2Data);
            game.RobinRoundNum = RobinRoundNum;
            game.CourtRoundNum = CourtRoundNum;

            return game;
        }
    }
}
