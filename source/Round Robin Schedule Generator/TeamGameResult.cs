using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class TeamGameResult
    {
        protected bool _wonGame = false;
        [XmlAttribute("WonGame")]
        public bool WonGame
        {
            get
            {
                return _wonGame;
            }
            set
            {
                _wonGame = value;
                if (TeamData != null) TeamData.resetStatistics();
            }
        }

        protected int _numPoints = 0;
        [XmlAttribute("NumPoints")]
        public int NumPoints
        {
            get
            {
                return _numPoints;
            }
            set
            {
                _numPoints = value;
                if (TeamData != null) TeamData.resetStatistics();
            }
        }

        protected int _numFouls = 0;
        [XmlAttribute("NumFouls")]
        public int NumFouls
        {
            get
            {
                return _numFouls;
            }
            set
            {
                _numFouls = value;
                if(TeamData!=null) TeamData.resetStatistics();
            }
        }


        protected RoundRobinTeamData _teamData;
        [XmlIgnore()]
        public RoundRobinTeamData TeamData
        {
            get
            {
                return _teamData;
            }
            set
            {
                _teamData = value;
            }
        }

        public TeamGameResult(RoundRobinTeamData teamdata)
        {
            _teamData = teamdata;
        }

        protected TeamGameResult()
        {
        }
    }
}
