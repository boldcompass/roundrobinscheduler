using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public struct XmlGamePosition
    {
        [XmlAttribute("CourtRoundNum")]
        public int CourtRoundNum;
       
        [XmlAttribute("CourtNumber")]
        public  int CourtNumber;

        public XmlGamePosition(int courtRoundNum, int courtNumber)
        {
            CourtRoundNum = courtRoundNum;
            CourtNumber = courtNumber;
        }
    }
}
