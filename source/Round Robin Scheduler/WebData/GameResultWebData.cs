using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class GameResultWebData
    {
        protected Game _game;
        [ScriptIgnore()]
        public Game Game
        {
            get
            {
                return _game;
            }
            set
            {
                _game = value;
            }
        }
        public int GameId
        {
            get
            {
                if (Game != null)
                {
                    return Game.Id;
                }
                return -1;
            }
        }
        public string WinningTeam
        {
            get
            {
                if (Game != null)
                {
                    if (Game.WinningTeam != null)
                    {
                        return Game.WinningTeam.Id;
                    }
                }
                return null;
            }
        }
        public Dictionary<string,TeamGameResultWebData> TeamGameResults
        {
            get
            {
                if (Game != null)
                {
                    if (Game.TeamGameResults != null)
                    {
                        Dictionary<string, TeamGameResultWebData> teamGameResults = new Dictionary<string, TeamGameResultWebData>();
                        foreach (KeyValuePair<string,TeamGameResult> pair in Game.TeamGameResults)
                        {
                            teamGameResults.Add(pair.Key, new TeamGameResultWebData(pair.Value));
                        }
                        return teamGameResults;
                    }
                }
                return null;
            }
        }
        public GameResultWebData(Game game)
        {
            _game = game;
        }
        public GameResultWebData()
        {
        }
    }
}
