using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class ScoreKeeper
    {
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

        protected string _associatedAccessCode;
        [XmlIgnore()]
        public string AssociatedAccessCode
        {
            get
            {
                return _associatedAccessCode;
            }
            set
            {
                _associatedAccessCode = value;
            }
        }

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

        public ScoreKeeper(string name)
        {
            _name = name;
        }

        protected ScoreKeeper()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
