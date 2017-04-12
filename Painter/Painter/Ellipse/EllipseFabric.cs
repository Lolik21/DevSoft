using System;
using System.Collections.Generic;
using System.Drawing;
using MainFigCreater;

namespace Ellipse
{
    public class EllipseFabric : AbstractFabric
    {
        public override Type GetFType()
        {
            return typeof(Ellipse);
        }
        public override string GetImgName()
        {
            return "ellipse.png";
        }
        public static Point[] GetOkPoints(List<Point> points)
        {
            Point tmpPoint = new Point();
            Point tmpPoint2 = new Point();

            tmpPoint.X = Math.Min(points[0].X, points[1].X);
            tmpPoint.Y = Math.Min(points[0].Y, points[1].Y);

            tmpPoint2.X = Math.Max(points[0].X, points[1].X);
            tmpPoint2.Y = Math.Max(points[0].Y, points[1].Y);

            Point[] p = new Point[2];
            p[0] = tmpPoint;
            p[1] = tmpPoint2;
            return p;
        }
        public override AbstractFigure GetFigure(Pen Pen, List<Point> points)
        {
            Point[] p = new Point[2];
            p = GetOkPoints(points);
            Ellipse El = new Ellipse(Pen, p[0].X, p[0].Y, p[1].X, p[1].Y);
            El.Painter = Painter;
            return El;
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
