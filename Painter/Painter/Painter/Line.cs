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
        
        public Line (int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public override void Draw()
        {
            Painter.DrawLine(PenType, x1, y1, x2, y2);
        }
    }
}
