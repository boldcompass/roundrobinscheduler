using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class TeamGameResultWebData
    {
        protected TeamGameResult _teamGameResult;
        [ScriptIgnore()]
        public TeamGameResult TeamGameResult
        {
            get
            {
                return _teamGameResult;
            }
            set
            {
                _teamGameResult = value;
            }
        }
        public string Team
        {
            get
            {
                if (TeamGameResult != null&&TeamGameResult.TeamData!=null)
                {
                    return TeamGameResult.TeamData.Team.Id;
                }
                return null;
            }
        }
        public string TeamName
        {
            get
            {
                if (TeamGameResult != null && TeamGameResult.TeamData != null)
                {
                    return TeamGameResult.TeamData.Team.Name;
                }
                return null;
            }
        }
        public int NumPoints
        {
            get
            {
                if (TeamGameResult != null && TeamGameResult.TeamData != null)
                {
                    return TeamGameResult.NumPoints;
                }
                return -1;
            }
        }
        public int NumFouls
        {
            get
            {
                if (TeamGameResult != null)
                {
                    return TeamGameResult.NumFouls;
                }
                return -1;
            }
        }
        public bool? WonGame
        {
            get
            {
                if (TeamGameResult != null)
                {
                    return TeamGameResult.WonGame;
                }
                return null;
            }
        }
        public TeamGameResultWebData(TeamGameResult teamGameResult)
        {
            _teamGameResult = teamGameResult;
        }
        public TeamGameResultWebData()
        {
        }
    }
}
