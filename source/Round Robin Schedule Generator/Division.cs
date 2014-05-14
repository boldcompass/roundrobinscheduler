using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class Division
    {
        protected string _name;
        [XmlAttribute("Type")]
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


        protected string _abbreviation;
        [XmlAttribute("Abbreviation")]
        public string Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            set
            {
                _abbreviation = value;
            }
        }

        protected static int _numIdPos = 0;
        protected int _numId = _numIdPos++;
        [XmlAttribute("Id")]
        public int NumId
        {
            get
            {
                return _numId;
            }
            set
            {
                _numId = value;
                if (value > _numIdPos) _numIdPos = value;
            }
        }

        protected List<Team> _teams;
        //[XmlElement("Teams", typeof(List<Team>))]
        [XmlIgnore()]
        public List<Team> Teams
        {
            get
            {
                return new List<Team>(_teams);
            }
            set
            {
                _teams = value;
                foreach (Team team in _teams)
                {
                    team.Division = this;
                }
                _roundRobinDatas = TeamsToRoundRobinTeamDatas(Teams).ToArray();
            }
        }

        protected RoundRobinTeamData[] _roundRobinDatas;
        [XmlArray("TeamDatas"),XmlArrayItem("TeamData")]
        public RoundRobinTeamData[] RoundRobinDatas
        {
            get
            {
                return _roundRobinDatas;
            }
            set
            {
                _roundRobinDatas = value;

                _teams = new List<Team>();
                foreach (RoundRobinTeamData teamData in _roundRobinDatas)
                {
                    teamData.Team.Division = this;
                    _teams.Add(teamData.Team);
                }
            }
        }

        public Division(string name, string abbreviation)
        {
            _name = name;
            _abbreviation = abbreviation;
        }

        protected Division()
        {
        }

        public override string ToString()
        {
            return Name;
        }

        private static List<RoundRobinTeamData> TeamsToRoundRobinTeamDatas(List<Team> teams)
        {
            List<RoundRobinTeamData> TeamDatas = new List<RoundRobinTeamData>();
            foreach (Team team in teams)
            {
                TeamDatas.Add(new RoundRobinTeamData(team));
            }
            return TeamDatas;
        }


    }
}