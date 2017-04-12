using System.Collections.Generic;
using System.Drawing;
using MainFigCreater;
using System;

namespace Line
{
    public class LineFabric : AbstractFabric
    {
        public override Type GetFType()
        {
            return typeof(Line);
        }
        public override string GetImgName()
        {
            return "line.png";
        }
        public override AbstractFigure GetFigure(Pen Pen, List<Point> points)
        {
            Line Line = new Line(Pen, points[0], points[1]);
            Line.Painter = Painter;
            return Line;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 2)
            {
                return 2;
            }
            else return -1;
        }
    }
}
