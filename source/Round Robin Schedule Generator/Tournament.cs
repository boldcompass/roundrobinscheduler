using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class Tournament
    {
        protected List<Division> _divisions;
        [XmlElement("Divisions", typeof(List<Division>))]
        public List<Division> Divisions
        {
            get
            {
                return _divisions;
            }
            set
            {
                _divisions = value;
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

        protected int _numCourts;
        [XmlAttribute("NumCourts")]
        public int NumCourts
        {
            get
            {
                return _numCourts;
            }
            set
            {
                _numCourts = value;
            }
        }

        protected int _cycles = 1;
        [XmlAttribute("Cycles")]
        public int Cycles
        {
            get
            {
                return _cycles;
            }
            set
            {
                if (_cycles != value){ 
                    // The new value is different
                    if (AutoCalculateNumRobinRounds){
                        // The number of robin rounds is auto-calculated; regenerate the schedule
                        _courtRounds = null;
                    }
                    _cycles = value;
                }
            }
        }

        [XmlIgnore()]
        public bool AutoCalculateNumRobinRounds
        {
            get
            {
                return _robinRoundsToPlay == -1;
            }
            set
            {
                RobinRoundsToPlay = -1;
            }
        }

        [XmlIgnore()]
        public int TotalRobinRounds
        {
            get
            {
                if(_robinRoundsToPlay > RobinRoundsNeededToFinish * Cycles)
                    return _robinRoundsToPlay;
                else
                    return RobinRoundsNeededToFinish * Cycles;
            }
        }

        protected int _robinRoundsToPlay = -1;
        [XmlAttribute("RobinRoundsToPlay")]
        public int RobinRoundsToPlay
        {
            get
            {
                return _robinRoundsToPlay == -1 ? TotalRobinRounds : _robinRoundsToPlay;
            }
            set
            {
                if (_robinRoundsToPlay != value){
                    // The new value is different
                    if(_robinRoundsToPlay != -1 || value != TotalRobinRounds){
                        // Either the number of robin rounds didn't used to be auto-calculated, or the new value isn't the auto-calculated value
                        if (_robinRoundsToPlay != TotalRobinRounds || value != -1)
                        {
                            // Either the old value was manually set, or the new value isn't to auto-calculate
                            _courtRounds = null;
                        }
                    }
                    _robinRoundsToPlay = value;
                }
            }
        }

        protected string _name = "Untitled Tournament";
        [XmlAttribute("Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        protected Dictionary<Division, List<RobinRound>> _robinRoundsForDivision = new Dictionary<Division, List<RobinRound>>();
        [XmlIgnore()]
        protected Dictionary<Division, List<RobinRound>> RobinRoundsForDivision
        {
            get
            {
                if (_robinRoundsForDivision.Count < Divisions.Count)
                {
                    foreach (Division division in Divisions)
                    {
                        _robinRoundsForDivision[division] = CalculateRobinRoundsForDivision(division);
                    }
                }
                return _robinRoundsForDivision;
            }
        }

        [XmlIgnore()]
        public int RobinRoundsNeededToFinish
        {
            get
            {
                int robinRoundsNeededToFinish = 0;
                //Calculate the number of needed robin rounds (greatest robin round count for any division)
                foreach (Division division in Divisions)
                {
                    if (((List<RobinRound>)RobinRoundsForDivision[division]).Count > robinRoundsNeededToFinish)
                    {
                        robinRoundsNeededToFinish = ((List<RobinRound>)RobinRoundsForDivision[division]).Count;
                    }
                }
                return robinRoundsNeededToFinish;
            }
        }

        protected ScoreKeeper[] _scoreKeepers;
        [XmlArray("ScoreKeepers"), XmlArrayItem("ScoreKeeper")]
        public ScoreKeeper[] ScoreKeepers
        {
            get
            {
                return _scoreKeepers;
            }
            set
            {
                _scoreKeepers = value;
            }
        }

        protected List<string> _scoreKeeperAccessCodes;
        [XmlArray("AccessCodes"), XmlArrayItem("AccessCode")]
        public List<string> ScoreKeeperAccessCodes
        {
            get
            {
                return _scoreKeeperAccessCodes;
            }
            set
            {
                _scoreKeeperAccessCodes = value;
            }
        }

        protected Dictionary<GamePosition, ScoreKeeper> _scoreKeepersAssignment = new Dictionary<GamePosition, ScoreKeeper>();
        [XmlIgnore()]
        public Dictionary<GamePosition, ScoreKeeper> ScoreKeepersAssignment
        {
            get
            {
                return _scoreKeepersAssignment;
            }
            set
            {
                _scoreKeepersAssignment = value;
            }
        }

        [XmlArray("ScoreKeepersAssignment"), XmlArrayItem("ScoreKeeperAssignment")]
        public SerializableKeyValuePair<XmlGamePosition,int>[] XmlScoreKeepersAssignment
        {
            get
            {
                List<SerializableKeyValuePair<XmlGamePosition, int>> xmlResults = new List<SerializableKeyValuePair<XmlGamePosition, int>>();
                GamePosition[] keys = new GamePosition[ScoreKeepersAssignment.Count];
                ScoreKeepersAssignment.Keys.CopyTo(keys, 0);
                ScoreKeeper[] values = new ScoreKeeper[ScoreKeepersAssignment.Count];
                ScoreKeepersAssignment.Values.CopyTo(values, 0);

                for (int i = 0; i < ScoreKeepersAssignment.Count; i++)
                {
                    xmlResults.Add(new SerializableKeyValuePair<XmlGamePosition, int>(new XmlGamePosition(keys[i].CourtRoundNum, keys[i].CourtNumber), values[i].Id));
                }
                return xmlResults.ToArray();
            }
            set
            {
                _scoreKeepersAssignment.Clear();
                foreach (SerializableKeyValuePair<XmlGamePosition, int> pair in value)
                {
                    _scoreKeepersAssignment.Add(GamePosition.GetGamePosition(pair.Key.CourtRoundNum, pair.Key.CourtNumber), getScoreKeeperById(pair.Value));
                }
            }
        }

        protected Dictionary<int, Game> _games = new Dictionary<int, Game>();
        [XmlIgnore()]
        public List<Game> Games
        {
            get
            {
                List<Game> games = new List<Game>();
                foreach (CourtRound courtRound in CourtRounds)
                {
                    games.AddRange(courtRound.Games);
                }
                return games;
            }
        }
        [XmlIgnore()]
        public Dictionary<int, Game> GamesById
        {
            get
            {
                Dictionary<int, Game> gamesById = new Dictionary<int, Game>();
                foreach (Game game in Games)
                {
                    gamesById.Add(game.Id,game);
                }
                return gamesById;
            }
        }
        [XmlArray("Games"),XmlArrayItem("Game")]
        public Game[] XmlGames
        {
            get
            {
                return Games.ToArray();
            }
            set
            {
                _games.Clear();
                foreach (Game game in value)
                {
                    List<RoundRobinTeamData> teamDatas = new List<RoundRobinTeamData>();
                    foreach (string teamId in game.XmlTeamsIn)
                    {
                        teamDatas.Add(getTeamDataById(teamId));
                    }
                    game.TeamDatas = teamDatas;

                    RoundRobinTeamData winningTeamData = getTeamDataById(game.XmlWinningTeamIn);
                    if (winningTeamData != null)
                    {
                        game.WinningTeam = winningTeamData.Team;
                    }

                    foreach (KeyValuePair<string,TeamGameResult> teamResult in game.TeamGameResults)
                    {
                        teamResult.Value.TeamData = getTeamDataById(teamResult.Key);
                    }

                    foreach (RoundRobinTeamData teamData in game.TeamDatas)
                    {
                        if (teamData.XmlPlayedGamesIn.Contains(game.Id))
                        {
                            teamData.addPlayedGame(game);
                        }
                    }

                    _games.Add(game.Id, game);
                }
            }
        }

        [XmlIgnore()]
        public bool IsInProgress
        {
            get
            {
                bool? isCompleted = null;
                foreach (Game game in Games)
                {
                    if (game.IsInProgress) return true;
                    else if(game.IsCompleted)
                    {
                        if (isCompleted == null) isCompleted = IsCompleted;
                        if (isCompleted == false) return true;
                    }
                }
                return false;
            }
        }

        [XmlIgnore()]
        public bool IsCompleted
        {
            get
            {
                foreach (Game game in Games)
                {
                    if (!game.IsCompleted) return false;
                }
                return true;
            }
        }

        protected List<CourtRound> _courtRounds;
        [XmlIgnore()]
        public List<CourtRound> CourtRounds
        {
            get
            {
                if (_courtRounds == null)
                {
                    _courtRounds = CalculateCourtRounds(RobinRoundsToPlay);
                }
                return _courtRounds;
            }
        }

        [XmlArray("CourtRounds"), XmlArrayItem("CourtRound")]
        public CourtRound[] XmlCourtRounds
        {
            get
            {
                return CourtRounds.ToArray();
            }
            set
            {
                _courtRounds = new List<CourtRound>(value);
                foreach (CourtRound CourtRound in _courtRounds)
                {
                    List<Game> games = new List<Game>();
                    foreach (int gameId in CourtRound.XmlGamesIn)
                    {
                        games.Add((Game)_games[gameId]);
                    }
                    CourtRound.Games = games;
                }
            }
        }

        [XmlIgnore()]
        public CourtRound ActiveCourtRound
        {
            get
            {
                CourtRound activeCourtRound = null;
                foreach (CourtRound courtRound in CourtRounds)
                {
                    if (courtRound.IsActive)
                    {
                        activeCourtRound = courtRound;
                        break;
                    }
                }
                return activeCourtRound;
            }
        }

        protected List<RobinRound> _robinRounds;
        [XmlIgnore()]
        public List<RobinRound> RobinRounds
        {
            get
            {
                if (_robinRounds == null)
                {
                    _courtRounds = CalculateCourtRounds(RobinRoundsToPlay);
                }
                return _robinRounds;
            }
        }

        [XmlArray("RobinRounds"),XmlArrayItem("RobinRound")]
        public RobinRound[] XmlRobinRounds{
            get
            {
                return RobinRounds.ToArray();
            }
            set
            {
                _robinRounds = new List<RobinRound>(value);
                foreach (RobinRound robinRound in _robinRounds)
                {
                    List<Game> games = new List<Game>();
                    foreach (int gameId in robinRound.XmlGamesIn)
                    {
                        games.Add((Game)_games[gameId]);
                    }
                    robinRound.Games = games;
                }
            }
        }

        public Tournament(List<Division> divisions, int numCourts)
        {
            _divisions = divisions;
            _numCourts = numCourts;
        }

        protected Tournament()
        {
        }

        private List<Game> calculatePossibleGames(List<Game> unusedGames, List<Game> courtRoundGames)
        {
            List<Team> OccupiedTeams = new List<Team>();
            foreach (Game game in courtRoundGames)
            {
                OccupiedTeams.AddRange(game.Teams);
            }
            return calculatePossibleGames(unusedGames, OccupiedTeams);
        }

        private List<Game> calculatePossibleGames(List<Game> unusedGames, List<Team> occupiedTeams)
        {
            List<Game> PossibleGames = new List<Game>();
            foreach (Game game in unusedGames)
            {
                bool isPossibleGame = true;
                foreach (Team team in game.Teams)
                {
                    if (occupiedTeams.Contains(team))
                    {
                        isPossibleGame = false;
                        break;
                    }
                }
                if (isPossibleGame)
                {
                    PossibleGames.Add(game);
                }
            }
            return PossibleGames;
        }

        private List<CourtRound> CalculateCourtRounds(int numRobinRounds = -1)
        {
            _robinRounds = new List<RobinRound>();
            List<CourtRound> CourtRounds = new List<CourtRound>();
            resetGameCloneCount();
            _scheduleVersion++;

            List<List<RobinRound>> DivisionsRobinRounds = new List<List<RobinRound>>();
            Dictionary<Team, int> TeamsNumberOfHomeGames = new Dictionary<Team, int>();
            bool haveRobinRounds = false;
            foreach (Division division in Divisions)
            {
                DivisionsRobinRounds.Add(((List<RobinRound>)RobinRoundsForDivision[division]));
                if (RobinRoundsForDivision[division].Count > 0)
                {
                    haveRobinRounds = true;
                }
                foreach (Team team in division.Teams)
                {
                    TeamsNumberOfHomeGames.Add(team, 0);
                }
            }

            if (!haveRobinRounds)
            {
                // We don't have any games to work with
                return CourtRounds;
            }

            if (numRobinRounds < 1) numRobinRounds = TotalRobinRounds;

            int RobinRoundNumber = 0;
            int CourtRoundNumber = 0;
            List<Game> RobinRoundUsedGames = null;
            List<Game> RobinRoundUnusedGames = null;

            //Keep track of what teams we have used for each court round
            List<List<Team>> CourtRoundsUsedTeams = new List<List<Team>>();

            bool completedAllCourtRounds = false;
            //To help convert team to index
            List<Team> teams = new List<Team>();
            foreach (Division division in Divisions)
            {
                teams.AddRange(division.Teams);
            }
            //To help put teams on different courts
            Dictionary<int, Dictionary<int, int>> timesOnCourtByTeam = new Dictionary<int, Dictionary<int, int>>();
            foreach(Team team in teams)
            {
                Dictionary<int, int> timesOnCourt = new Dictionary<int, int>();
                for (int i = 0; i <= NumCourts; i++)
                {
                    timesOnCourt.Add(i, 0);
                }
                timesOnCourtByTeam.Add(team.NumId, timesOnCourt);
            }
           
            while (!completedAllCourtRounds)
            {
                CourtRoundsUsedTeams.Add(new List<Team>());
                Dictionary<int,List<Game>> CourtRoundGamesByDivision = new Dictionary<int,List<Game>>();
                //Populate CourtRoundGamesByDivision initial values
                foreach (Division division in Divisions)
                {
                    CourtRoundGamesByDivision.Add(division.NumId, new List<Game>());
                }
                for (int i = 0; i < this.NumCourts; i++)
                {
                    if (RobinRoundUnusedGames == null)
                    {
                        RobinRoundUnusedGames = new List<Game>();
                        foreach (List<RobinRound> DivisionRobinRounds in DivisionsRobinRounds)
                        {
                            if (DivisionRobinRounds.Count < 1) continue;
                            int DivisionRobinRoundIndex = RobinRoundNumber % DivisionRobinRounds.Count;
                            RobinRoundUnusedGames.AddRange(DivisionRobinRounds[DivisionRobinRoundIndex].Games);
                        }
                        RobinRoundUsedGames = new List<Game>();
                    }

                    List<Game> PossibleGames = calculatePossibleGames(RobinRoundUnusedGames, CourtRoundsUsedTeams[CourtRoundNumber]);

                    if (PossibleGames.Count <= 0)
                    {
                        break;
                    }

                    double GamesSectionLength = PossibleGames.Count / NumCourts;
                    Game PickedGame = PossibleGames[(int)Math.Floor(GamesSectionLength * i)];
                    Game ClonedGame;

                    if (TeamsNumberOfHomeGames[PickedGame.Team1] <= TeamsNumberOfHomeGames[PickedGame.Team2])
                    {
                        ClonedGame = getGame(PickedGame.Team1Data, PickedGame.Team2Data);
                    }
                    else
                    {
                        ClonedGame = getGame(PickedGame.Team2Data, PickedGame.Team1Data);
                    }
                    TeamsNumberOfHomeGames[ClonedGame.Team1]++;

#if DEBUG
                    if (ClonedGame == null)
                        System.Diagnostics.Debugger.Break();
#endif

                    ClonedGame.CourtRoundNum = CourtRoundNumber + 1;
                    ClonedGame.RobinRoundNum = RobinRoundNumber + 1;
                    ClonedGame.ScheduleVersion = _scheduleVersion;
                    CourtRoundGamesByDivision[ClonedGame.Division.NumId].Add(ClonedGame);
                    CourtRoundsUsedTeams[CourtRoundNumber].AddRange(PickedGame.Teams);
                    RobinRoundUsedGames.Add(ClonedGame);
                    RobinRoundUnusedGames.Remove(PickedGame);

                    if (RobinRoundUnusedGames.Count <= 0)
                    {
                        RobinRoundUnusedGames = null;
                        _robinRounds.Add(new RobinRound(RobinRoundUsedGames, RobinRoundNumber + 1));
                        RobinRoundNumber++;
                        if (RobinRoundNumber >= numRobinRounds)
                        {
                            completedAllCourtRounds = true;
                            break;
                        }
                    }

                }
            
                List<Game> CourtRoundGamesSchedule = new List<Game>();
                foreach (KeyValuePair<int, List<Game>> CourtRoundGamesForDivision in CourtRoundGamesByDivision)
                {
                    List<Game> GamesToSchedule = new List<Game>(CourtRoundGamesForDivision.Value);
                    while (GamesToSchedule.Count > 0)
                    {
                        int CourtId = CourtRoundGamesSchedule.Count;
                        int CourtNumber = CourtId + 1;
                        Game PickedGame = null;
                        int pickedGameTeamsNumberSum = 0;
                        int lowestTimesOnCourtSum = int.MaxValue;
                        if (GamesToSchedule.Count == 0)
                        {
                            PickedGame = GamesToSchedule[0];
                        }
                        else
                        {
                            foreach (Game game in GamesToSchedule)
                            {
                                int timesOnCourtSum = 0;
                                int teamsNumberSum = 0;
                                foreach (Team team in game.Teams)
                                {
                                    timesOnCourtSum += timesOnCourtByTeam[team.NumId][CourtId];
                                    teamsNumberSum+=team.Number;
                                }
                                if (timesOnCourtSum < lowestTimesOnCourtSum || (timesOnCourtSum == lowestTimesOnCourtSum && teamsNumberSum < pickedGameTeamsNumberSum))
                                {
                                    PickedGame = game;
                                    lowestTimesOnCourtSum = timesOnCourtSum;
                                    pickedGameTeamsNumberSum = teamsNumberSum;
                                }
                            }
                        }
                        CourtRoundGamesSchedule.Add(PickedGame);
                        GamesToSchedule.Remove(PickedGame);
                        PickedGame.CourtNumber = CourtNumber;
                        foreach (Team team in PickedGame.Teams)
                        {
                            timesOnCourtByTeam[team.NumId][CourtId] += 1;
                        }
                    }
                }
                CourtRounds.Add(new CourtRound(CourtRoundGamesSchedule, CourtRoundNumber + 1));
                CourtRoundNumber++;
            }
            return CourtRounds;
        }

        protected Dictionary<string, List<Game>> gameClones = new Dictionary<string, List<Game>>();
        protected Dictionary<string, int> gameCloneCount = new Dictionary<string, int>();
       
            /*if (TeamsNumberOfHomeGames[firstTeamData.Team] <= TeamsNumberOfHomeGames[secondTeamData.Team])
                        {
                            game = new Game(firstTeamData, secondTeamData);
                        }
                        else
                        {
                            game = new Game(secondTeamData, firstTeamData);
                        }
                        TeamsNumberOfHomeGames[game.Team1Data.Team]++;
                        */
        protected Game getGame(RoundRobinTeamData team1Data, RoundRobinTeamData team2Data)
        {
            string vsID = Game.CalculateVsId(team1Data.Team, team2Data.Team);
            if (!gameCloneCount.ContainsKey(vsID))
            {
                gameCloneCount[vsID] = -1;
            }
            gameCloneCount[vsID]++;

            int index = gameCloneCount[vsID];

            if (!gameClones.ContainsKey(vsID))
            {
                gameClones.Add(vsID, new List<Game>());
            }

            List<Game> clones = gameClones[vsID];
            Game clone;

            if (clones.Count <= index)
            {
                clone = new Game(team1Data, team2Data);
                clones.Add(clone);
            }
            else
            {
                clone = clones[index];
            }

            return clone;
        }

        [XmlArray("GameCloneLists"), XmlArrayItem("GameCloneList")]
        public SerializableKeyValuePair<string,int[]>[] XmlCloneList
        {
            get
            {
                List<SerializableKeyValuePair<string, int[]>> cloneLists = new List<SerializableKeyValuePair<string, int[]>>();

                foreach(KeyValuePair<string,List<Game>> clones in gameClones){
                    List<int> cloneList = new List<int>();
                    foreach(Game game in clones.Value)
                    {
                        cloneList.Add(game.Id);
                    }
                    cloneLists.Add(new SerializableKeyValuePair<string, int[]>(clones.Key, cloneList.ToArray()));
                }

                return cloneLists.ToArray();
            }
            set
            {
                foreach (SerializableKeyValuePair<string, int[]> cloneList in value)
                {
                    if (cloneList.Value != null)
                    {
                        List<Game> clones = new List<Game>();

                        foreach (int gameId in cloneList.Value)
                        {
                            if (_games.ContainsKey(gameId))
                            {
                                clones.Add(_games[gameId]);
                            }
                        }

                        gameClones[cloneList.Key] = clones;
                    }
                }
            }
        }

        protected void resetGameCloneCount(){
            gameCloneCount.Clear();
        }

        protected List<RobinRound> CalculateRobinRoundsForDivision(Division division, bool includeByeGames = false)
        {
            if (division.Teams.Count < 2) return new List<RobinRound>();
            List<RoundRobinTeamData> TeamDatas = new List<RoundRobinTeamData>();
            TeamDatas.AddRange(division.RoundRobinDatas);
            if (TeamDatas.Count % 2 == 1)
            {
                TeamDatas.Add(new RoundRobinTeamData(new ByeTeam()));
            }

            int numTeams = TeamDatas.Count;
            int numDays = (numTeams - 1);
            int halfSize = numTeams / 2;
            
            RoundRobinTeamData Team1Data = TeamDatas[0];
            TeamDatas.RemoveAt(0);

            int TeamsSize = TeamDatas.Count;

            List<RobinRound> robinRounds = new List<RobinRound>();
            for (int robinRoundNum = 0; robinRoundNum < numDays; robinRoundNum++)
            {
                List<Game> robinRoundGames = new List<Game>();
                int teamIdx = robinRoundNum % TeamsSize;

                for (int idx = 0; idx < halfSize; idx++)
                {

                    RoundRobinTeamData firstTeamData;
                    if (idx == 0)
                    {
                        firstTeamData = Team1Data;
                    }
                    else
                    {
                        firstTeamData = TeamDatas[(robinRoundNum + idx) % TeamsSize];
                    }

                    RoundRobinTeamData secondTeamData;
                    if (idx == 0)
                    {
                        secondTeamData = TeamDatas[teamIdx];
                    }
                    else
                    {
                        secondTeamData = TeamDatas[(robinRoundNum + TeamsSize - idx) % TeamsSize];
                    }

                    if (!includeByeGames && !firstTeamData.IsBye && !secondTeamData.IsBye)
                    {
                        Game game = new Game(firstTeamData, secondTeamData);
                        robinRoundGames.Add(game);
                    }
                }
                robinRounds.Add(new RobinRound(robinRoundGames, robinRoundNum));
            }

            return robinRounds;
        }

        public Dictionary<Division, List<Team>> CalculateSeedingByDivisions()
        {
            Dictionary<Division, List<Team>> seeedingForDivision = new Dictionary<Division, List<Team>>();
            foreach (Division division in Divisions)
            {
                seeedingForDivision[division] = CalculateSeedingForDivision(division);
            }
            return seeedingForDivision;
        }

        protected List<Team> CalculateSeedingForDivision(Division division)
        {
            List<RoundRobinTeamData> teamDatas = new List<RoundRobinTeamData>(division.RoundRobinDatas);
            Comparison<RoundRobinTeamData> CompareTeams = delegate(RoundRobinTeamData x, RoundRobinTeamData y)
            {
                if (y.ScheduleVersion != ScheduleVersion)
                {
                    y.resetStatistics();
                }

                if (x.ScheduleVersion != ScheduleVersion)
                {
                    x.resetStatistics();
                }

                int compareValue = y.PercentGamesWon.CompareTo(x.PercentGamesWon);
                if (compareValue == 0)
                {
                    compareValue = y.AverageScore.CompareTo(x.AverageScore);
                }
                if (compareValue == 0)
                {
                    compareValue = x.Team.Number.CompareTo(y.Team.Number);
                }
                return compareValue;
            };
            teamDatas.Sort(CompareTeams);

            List<Team> teams = new List<Team>();
            foreach(RoundRobinTeamData teamData in teamDatas){
                teams.Add(teamData.Team);
            }
            return teams;
        }

        public RoundRobinTeamData getTeamDataById(string teamId)
        {
            if (string.IsNullOrEmpty(teamId) || teamId.Length < 2)
            {
                return null;
            }
            string teamDivisionAbbreviation = teamId.Substring(0, 1);
            int teamIndex = int.Parse(teamId.Substring(1)) - 1;
            foreach (Division division in Divisions)
            {
                if (division.Abbreviation != teamDivisionAbbreviation) continue;
                return division.RoundRobinDatas[teamIndex];
            }
            return null;
        }

        public RoundRobinTeamData getTeamDataByNumId(int teamNumId)
        {
            foreach (Division division in Divisions)
            {
                foreach (RoundRobinTeamData roundRobinData in division.RoundRobinDatas)
                {
                    if (roundRobinData.Team.NumId == teamNumId) return roundRobinData;
                }
            }
            return null;
        }

        public ScoreKeeper getScoreKeeperById(int scoreKeeperId)
        {
            foreach (ScoreKeeper scoreKeeper in ScoreKeepers)
            {
                if (scoreKeeper.Id == scoreKeeperId) return scoreKeeper;
            }
            return null;
        }

        public Game getGame(int courtRoundNumber, int courtNumber)
        {
            if (courtRoundNumber > 0 && this.CourtRounds.Count >= courtRoundNumber)
            {
                CourtRound courtRound = this.CourtRounds[courtRoundNumber - 1];
                if (courtNumber > 0 && courtRound.numGames >= courtNumber)
                {
                    return courtRound.Games[courtNumber - 1];
                }
            }
            return null;
        }
    }
}