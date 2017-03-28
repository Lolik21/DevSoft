using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    abstract class Fabric
    {
        private Graphics _Painter;
        public Graphics Painter
        {
            get { return _Painter; }
            set { _Painter = value; }
        }
        private Pen PenType;
        public Pen Pen
        {
            get { return PenType; }
            set
            {
                if (value != null)
                {
                    this.PenType = value;
                }
            }
        }
        public abstract Figure GetFigure(Pen Pen, List<Point> points);
        public abstract int CheckPoints(ref List<Point> points);
        
    } 
    class RectangleFabric : Fabric
    {
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
        public override Figure GetFigure(Pen Pen, List<Point> points)
        {
            Point[] p = new Point[2];
            p = GetOkPoints(points);
            Rectangle Rect = new Rectangle(Pen,p[0].X, p[0].Y, p[1].X, p[1].Y);
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
    class LineFabric : Fabric
    {
        public override Figure GetFigure(Pen Pen, List<Point> points)
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
    class EllipseFabric : RectangleFabric 
    {
        public override Figure GetFigure(Pen Pen, List<Point> points)
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
    class CurveLineFabric : Fabric
    {
        public override Figure GetFigure(Pen Pen, List<Point> points)
        {
            CurveLine CrvLine = new CurveLine(Pen, points);
            CrvLine.Painter = Painter;
            return CrvLine;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 3)
                return points.Count();
            else return -1;
        }
    }
    class PoligonFabric : Fabric
    {
        public override Figure GetFigure(Pen Pen, List<Point> points)
        {
            Poligon Pol = new Poligon(Pen, points);
            Pol.Painter = Painter;
            return Pol;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 3)
                return points.Count();
            else return -1;
        }
    }
}
