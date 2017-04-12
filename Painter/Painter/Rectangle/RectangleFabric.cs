using System;
using System.Collections.Generic;
using MainFigCreater;
using System.Drawing;

namespace Rectangle
{
    public class RectangleFabric : AbstractFabric
    {
        public override Type GetFType()
        {
            return typeof(Rectangle);
        }
        public override string GetImgName()
        {
            return "rectangle.png";
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
            Rectangle Rect = new Rectangle(Pen, p[0].X, p[0].Y, p[1].X, p[1].Y);
            Rect.Painter = Painter;
            return Rect;
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
