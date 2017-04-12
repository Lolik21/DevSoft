using System.Collections.Generic;
using MainFigCreater;
using System.Drawing;
using System;

namespace CurveLine
{
    public class CurveLineFabric : AbstractFabric
    {
        public override Type GetFType()
        {
            return typeof(CurveLine);
        }
        public override string GetImgName()
        {
            return "curve.png";
        }
        public override AbstractFigure GetFigure(Pen Pen, List<Point> points)
        {
            CurveLine CrvLine = new CurveLine(Pen, points);
            CrvLine.Painter = Painter;
            return CrvLine;
        }
        public override int CheckPoints(ref List<Point> points)
        {
            if (points.Count >= 3)
                return points.Count;
            else return -1;
        }
    }
}
