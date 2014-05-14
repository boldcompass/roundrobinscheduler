using System;
using System.Collections.Generic;
using System.Text;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    public class CourtRound : Round
    {
        public bool IsActive
        {
            get
            {
                if(IsCompleted)return false;
                bool isActive=true;
                List<CourtRound> courtRounds = Controller.GetController().Tournament.CourtRounds;
                int thisIndex = courtRounds.IndexOf(this);
                if(thisIndex<0)return false;
                for (int i = 0; i < thisIndex; i++)
                {
                    if (!courtRounds[i].IsCompleted)
                    {
                        isActive = false;
                        break;
                    }
                }
                return isActive;
            }
        }

        public CourtRound(List<Game> games, int roundNumber)
            : base(games, roundNumber)
        {
        }

        protected CourtRound()
            : base()
        {
        }

        public static string CourtNumToCourtName(int courtNum)
        {
            return String.Format("Court {0}", CourtNumToCourtLetter(courtNum));
        }

        public static string CourtNumToCourtLetter(int courtNum)
        {
            char courtLetter = Convert.ToChar((courtNum - 1) + 65);
            return courtLetter.ToString();
        }

        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < numGames; i++)
            {
                if(i>0){
                    value+="\n";
                }
                string courtName = CourtNumToCourtName(i + 1);
                value += String.Format("{0}: {1}", courtName, Games[i]);
            }
            return value;
        }
    }
}
