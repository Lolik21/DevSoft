using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    class Line : Figure
    {
        protected Point x;
        protected Point y;
      

        public Line (Pen PenType, Point x, Point y)
        {
            this.Pen = PenType;
            this.x = x;
            this.y = y;    
        }
        public override void CalcPerimetr()
        {

        }
        public override void Draw()
        {
            Painter.DrawLine(Pen, x, y);    
        }
        public override string GetName()
        {
            return "Линия";
        }
    }
}
