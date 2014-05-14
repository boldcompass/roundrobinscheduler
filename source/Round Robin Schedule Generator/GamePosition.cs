using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SomeTechie.RoundRobinScheduleGenerator
{
    public class GamePosition
    {
        protected int _courtRoundNum;
        public int CourtRoundNum
        {
            get
            {
                return _courtRoundNum;
            }
            protected set
            {
                _courtRoundNum = value;
            }
        }

        protected int _courtNumber;
        public int CourtNumber
        {
            get
            {
                return _courtNumber;
            }
            protected set
            {
                _courtNumber = value;
            }
        }

        protected GamePosition(int courtRoundNum, int courtNum)
        {
            CourtRoundNum = courtRoundNum;
            CourtNumber = courtNum;
        }

        protected GamePosition()
        {
        }

        protected static List<GamePosition> GamePositionList = new List<GamePosition>();
        public static GamePosition GetGamePosition(int courtRoundNum, int courtNum)
        {
            foreach (GamePosition gamePosition in GamePositionList)
            {
                if (gamePosition.CourtRoundNum == courtRoundNum && gamePosition.CourtNumber == courtNum) return gamePosition;
            }
            GamePosition newGamePosition = new GamePosition(courtRoundNum, courtNum);
            GamePositionList.Add(newGamePosition);
            return newGamePosition;
        }
    }
}
