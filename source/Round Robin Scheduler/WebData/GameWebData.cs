using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class GameWebData
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
        public int Id
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
        public bool? IsCompleted
        {
            get
            {
                if (Game != null)
                {
                    return Game.IsCompleted;
                }
                return null;
            }
        }
        public int RobinRoundNum
        {
            get
            {
                if (Game != null)
                {
                    return Game.RobinRoundNum;
                }
                return -1;
            }
        }
        public int CourtRoundNum
        {
            get
            {
                if (Game != null)
                {
                    return Game.CourtRoundNum;
                }
                return -1;
            }
        }
        public int CourtNumber
        {
            get
            {
                if (Game != null)
                {
                    return Game.CourtNumber;
                }
                return -1;
            }
        }
        public string CourtName
        {
            get
            {
                if (CourtNumber > 0)
                {
                    return CourtRound.CourtNumToCourtName(CourtNumber);
                }
                return null;
            }
        }
        public string CourtLetter
        {
            get
            {
                if (CourtNumber > 0)
                {
                    return CourtRound.CourtNumToCourtLetter(CourtNumber);
                }
                return null;
            }
        }
        public string Team1
        {
            get
            {
                if (Game != null)
                {
                    if (Game.Team1 != null)
                    {
                        return Game.Team1.Id;
                    }
                }
                return null;
            }
        }
        public string Team2
        {
            get
            {
                if (Game != null)
                {
                    if (Game.Team2 != null)
                    {
                        return Game.Team2.Id;
                    }
                }
                return null;
            }
        }
        public string Team1Name
        {
            get
            {
                if (Game != null)
                {
                    if (Game.Team1 != null)
                    {
                        return Game.Team1.Name;
                    }
                }
                return null;
            }
        }
        public string Team2Name
        {
            get
            {
                if (Game != null)
                {
                    if (Game.Team2 != null)
                    {
                        return Game.Team2.Name;
                    }
                }
                return null;
            }
        }
        public int ScoreKeeper
        {
            get
            {
                if (Game != null)
                {
                    if (Game.ScoreKeeper != null)
                    {
                        return Game.ScoreKeeper.Id;
                    }
                }
                return -1;
            }
        }
        public GameResultWebData GameResult
        {
            get
            {
                if (Game != null)
                {
                    return new GameResultWebData(this.Game);
                }
                return null;
            }
        }
        public GameWebData(Game game)
        {
            _game = game;
        }
        public GameWebData()
        {
        }
    }
}
