using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    [XmlType()]
    public class RobinRound : Round
    {
        public RobinRound(List<Game> games, int roundNumber)
            : base(games,roundNumber)
        {
        }

        protected RobinRound()
            : base()
        {
        }
    }
}
