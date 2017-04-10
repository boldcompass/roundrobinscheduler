using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class Round
    {
        private List<Game> _games;
        [XmlIgnore()]
        public List<Game> Games
        {
            get
            {
                return _games;
            }
            set
            {
                _games = value;
            }
        }

        protected List<int> _xmlGames;
        [XmlArray("Games"), XmlArrayItem("Game")]
        public int[] XmlSerializerGames
        {
            get
            {
                List<int> xmlGames = new List<int>();
                foreach (Game game in Games)
                {
                    if (game != null)
                    {
                        xmlGames.Add(game.Id);
                    }
                    else
                    {
                        xmlGames.Add(-1);
                    }
                }
                return xmlGames.ToArray();
            }
            set
            {
                _xmlGames = new List<int>(value);
            }
        }
        [XmlIgnore()]
        public int[] XmlGamesIn
        {
            get
            {
                return _xmlGames.ToArray();
            }
        }

        [XmlIgnore()]
        public int numGames
        {
            get
            {
                return Games.Count;
            }
        }

        protected int _roundNumber;
        [XmlAttribute("RoundNumber")]
        public int RoundNumber
        {
            get
            {
                return _roundNumber;
            }
            set
            {
                _roundNumber = value;
            }
        }

        [XmlIgnore()]
        public List<Team> Teams
        {
            get
            {
                List<Team> teams = new List<Team>();
                foreach (Game game in Games)
                {
                    if (!game.Enabled) continue;
                    teams.AddRange(game.Teams);
                }
                return teams;
            }
        }

        public bool IsConfirmed
        {
            get
            {
                foreach (Game game in Games)
                {
                    if (!game.Enabled) continue;
                    if (!game.IsConfirmed) return false;
                }
                return true;
            }
        }

        public bool IsInProgress
        {
            get
            {
                bool? isCompleted = null;
                foreach (Game game in Games)
                {
                    if (!game.Enabled) continue;
                    if (game.IsInProgress) return true;
                    else if (game.IsCompleted)
                    {
                        if (isCompleted == null) isCompleted = IsCompleted;
                        if (isCompleted == false) return true;
                    }
                }
                return false;
            }
        }

        public bool IsCompleted
        {
            get
            {
                foreach (Game game in Games)
                {
                    if (game.Enabled && !game.IsCompleted) return false;
                }
                return true;
            }
        }

        public Round(List<Game> games, int roundNumber)
        {
            _games = games;
            _roundNumber = roundNumber;
        }

        protected Round()
        {
        }

        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < numGames; i++)
            {
                if(i>0){
                    value+="\n";
                }
                value += Games[i].ToString();
            }
            return value;
        }
    }
}
