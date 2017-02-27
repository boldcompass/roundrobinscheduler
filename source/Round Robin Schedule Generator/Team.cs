using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class Team
    {
        protected string _name;
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

        [XmlIgnore()]
        public string Id
        {
            get
            {
                return Division.Abbreviation + Number.ToString();
            }
        }

        protected int _number;
        [XmlAttribute("Number")]
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        protected Division _division;
        [XmlIgnore()]
        public Division Division
        {
            get
            {
                return _division;
            }
            set
            {
                _division = value;
            }
        }

        [XmlIgnore()]
        public RoundRobinTeamData RoundRobinData
        {
            get
            {
                return Division.getTeamData(this);
            }
        }

        public Team(string name, int number)
        {
            _name = name;
            _number = number;
        }

        protected Team()
        {
        }

        public override string ToString()
        {
            if (Id == Name)
            {
                return Name;
            }
            else
            {
               return "(" + Id + ") " + Name;
            }
        }
    }
}
