using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    class CurveLine : Figure
    {
        protected Point[] PointsArr;
        
        public CurveLine (Pen pen, List<Point> points)
        {
            PointsArr = new Point[points.Count];
            PointsArr = points.ToArray();
            this.Pen = pen;           
        }
        public override void CalcPerimetr()
        {
            
        }
        public override void Draw()
        {
            Painter.DrawCurve(Pen, PointsArr);
        }
        public override string GetName()
        {
            return "Кривая линия";
        }
        public override void Move(int dx, int dy)
        {
            throw new NotImplementedException();
        }
        public override void Mark()
        {
            throw new NotImplementedException();
        }
        public override bool IS_Seasiable()
        {
            return false;
        }
        public override bool PIsInFigure(Point Point)
        {
            throw new NotImplementedException();
        }
        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White, 3);
            Painter.DrawCurve(tmpPen, PointsArr);
        }
    }
}
