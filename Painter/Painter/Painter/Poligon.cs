using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    class Poligon : CurveLine
    {
        public Poligon(Pen pen, List<Point> points) : base(pen, points)
        {

        }
        public override void CalcPerimetr()
        {

        }
        public override void Draw()
        {
            Painter.DrawPolygon(Pen, PointsArr);
            Painter.FillPolygon(Pen.Brush, PointsArr);
        }
        public override string GetName()
        {
            return "Многоугольник";
        }
    }
}
