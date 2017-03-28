using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    [Serializable]
    class Rectangle : Figure, Interfaces.IEditable
    {
        protected int x1, y1;
        protected int Width, Height;

        public Rectangle(Pen PenType, int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.Width = x2 - x1;
            this.Height = y2 - y1;
            Pen = PenType;
        }

        public override void Mark()
        {
            Pen MarkPen = new Pen(Color.Red,Pen.Width);
            Painter.DrawRectangle(MarkPen, x1, y1, Width, Height);
        }
        public override bool PIsInFigure(Point Point)
        {
            if ((Point.X > x1 && Point.X < x1 + Width) &&
                (Point.Y > y1 && Point.Y < y1 + Height))
            {
                return true;
            }
            else return false;
        }
        public override void Move(int dx, int dy)
        {
            DeleteFig();
            x1 = x1 + dx;
            y1 = y1 + dy;
            Draw();
        }

        public override void Draw()
        {
            Painter.FillRectangle(Pen.Brush, x1, y1, Width, Height);
            Painter.DrawRectangle(Pen, x1, y1, Width, Height);             
        }
        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White,3);
            Painter.FillRectangle(tmpPen.Brush, x1, y1, Width, Height);
            Painter.DrawRectangle(tmpPen, x1, y1, Width, Height);
        }
        
        public override string GetName()
        {
            return "Прямоугольник";
        }
        
        
    }
}
