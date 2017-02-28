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
    }
}
