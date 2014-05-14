using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebData
{
    class ScoreKeeperWebData
    {
        protected ScoreKeeper _scoreKeeper;
        [ScriptIgnore()]
        public ScoreKeeper ScoreKeeper
        {
            get
            {
                return _scoreKeeper;
            }
            set
            {
                _scoreKeeper = value;
            }
        }
        public int Id
        {
            get
            {
                if (ScoreKeeper != null)
                {
                    return ScoreKeeper.Id;
                }
                return -1;
            }
        }
        public string Name
        {
            get
            {
                if (ScoreKeeper != null)
                {
                    return ScoreKeeper.Name;
                }
                return null;
            }
        }
        public ScoreKeeperWebData(ScoreKeeper scoreKeeper)
        {
            _scoreKeeper = scoreKeeper;
        }
        public ScoreKeeperWebData()
        {
        }
    }
}
