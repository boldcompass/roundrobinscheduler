using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeTechie.RoundRobinScheduler
{
    using System.Windows.Forms;

    public class Paintable : Control
    {
        public Paintable()
        {
            this.DoubleBuffered = true;

            // or

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }
}
