using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class TournamentStatusWebData
    {
        protected Tournament _tournament;
        [ScriptIgnore()]
        public Tournament Tournament
        {
            get
            {
                return _tournament;
            }
            set
            {
                _tournament = value;
            }
        }
        public int ActiveCourtRoundNum
        {
            get
            {
                if (Tournament != null)
                {
                    CourtRound activeCourtRound = Tournament.ActiveCourtRound;
                    if (activeCourtRound != null)
                    {
                        return activeCourtRound.RoundNumber;
                    }
                }
                return -1;
            }
        }
        public TournamentStatusWebData(Tournament tournament)
        {
            _tournament = tournament;
        }
        public TournamentStatusWebData()
        {
        }
    }
}
