using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MainFigCreater;
using System;

namespace Poligon
{
    public class PoligonFabric : AbstractFabric
    {
        public override Type GetFType()
        {
            return typeof(Poligon);
        }
        public override string GetImgName()
        {
            return "poligon.png";
        }
        public override AbstractFigure GetFigure(Pen Pen, List<Point> points)
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
