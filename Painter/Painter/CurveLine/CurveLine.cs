using System;
using System.Collections.Generic;
using MainFigCreater;
using System.Drawing;
using System.Runtime.Serialization;

namespace CurveLine
{
    [DataContract]
    class CurveLine : AbstractFigure
    {
        [DataMember]
        protected Point[] PointsArr;

        public override void Move(int dx, int dy)
        {

        }
        public override bool PIsInFigure(Point Point)
        {
            return false;
        }
        public override void Mark()
        {

        }

        public CurveLine(Pen pen, List<Point> points)
        {
            PointsArr = new Point[points.Count];
            PointsArr = points.ToArray();
            this.Pen = pen;
        }
        public override void Draw()
        {
            Painter.DrawCurve(Pen, PointsArr);
        }
        public override string GetName()
        {
            return "Кривая линия";
        }

        public override Type GetFabricType()
        {
            return typeof(CurveLineFabric);
        }

        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White, 3);
            Painter.DrawCurve(tmpPen, PointsArr);
        }
    }
}
