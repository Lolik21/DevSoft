using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    class Line : Figure
    {
        protected int x1, y1;
        protected int x2, y2;
      

        public Line ()
        {
            
        }

        public override void CalcPerimetr()
        {

        }

        public override void Draw(ref FigList Figures)
        {
            Painter.DrawLine(PenType, x1, y1, x2, y2);
        }
    }
}
