using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeTechie.RoundRobinScheduler
{
    class NamedData<t>
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected t _data;
        public t Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public NamedData(string name, t data)
            : base()
        {
            Name = name;
            Data = data;
        }
        public NamedData() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
