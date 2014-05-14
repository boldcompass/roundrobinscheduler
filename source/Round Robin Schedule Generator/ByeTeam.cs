using System;
using System.Collections.Generic;
using System.Text;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    public class ByeTeam:Team
    {
        public ByeTeam()
            : base(null, -1)
        {
            
        }

        public override string ToString()
        {
            return "%Bye%";
        }
    }
}
