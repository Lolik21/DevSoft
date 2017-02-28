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
        public abstract Fabric GetFigure(Pen Pen, List<Point> points);
        public abstract int CheckPoints(ref List<Point> points);
        
    }
    class RectangleFabric : Fabric
    {
        public override Fabric GetFigure(Pen Pen, List<Point> points)
        {
            return null;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 4)
            {
                return 4;
            }
            else return -1;   
        }

    }
    class LineFabric : Fabric
    {
        public override Fabric GetFigure(Pen Pen, List<Point> points)
        {
            return null; 
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
    class EllipseFabric : Fabric
    {
        public override Fabric GetFigure(Pen Pen, List<Point> points)
        {
            return null;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 4)
            {
                return 4;
            }
            else return -1;
        }
    }
    class CurveLineFabric : Fabric
    {
        public override Fabric GetFigure(Pen Pen, List<Point> points)
        {
            return null;
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
    class PoligonFabric : Fabric
    {
        public override Fabric GetFigure(Pen Pen, List<Point> points)
        {
            return null;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 3)
            {
                return 3;
            }
            else return -1;
        }
    }
}
