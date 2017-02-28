using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    class Rectangle : Figure
    {
        protected int x1, y1;
        protected int Width, Height;        

        public override void Draw()
        {
            Painter.FillRectangle(Pen.Brush, x1, y1, Width, Height);
            Painter.DrawRectangle(Pen, x1, y1, Width, Height);             
        }        
        public override void CalcPerimetr()
        {

        }
        public override string GetName()
        {
            return "Прямоугольник";
        }
        public Rectangle(Pen PenType,int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.Width = x2 - x1;
            this.Height = y2 - y1;
            Pen = PenType;
        }
    }
}
