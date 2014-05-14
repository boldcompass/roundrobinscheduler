using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class TeamWebData
    {
        protected Team _team;
        [ScriptIgnore()]
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
        public string Id
        {
            get
            {
                if (Team != null)
                {
                    return Team.Id;
                }
                return null;
            }
        }
        public string Name
        {
            get
            {
                if (Team != null)
                {
                    return Team.Name;
                }
                return null;
            }
        }
        public int Number
        {
            get
            {
                if (Team != null)
                {
                    return Team.Number;
                }
                return -1;
            }
        }
        public string Division
        {
            get
            {
                if (Team != null)
                {
                    return Team.Division.Name;
                }
                return null;
            }
        }
        public TeamWebData(Team team)
        {
            _team = team;
        }
        public TeamWebData()
        {
        }
    }
}
