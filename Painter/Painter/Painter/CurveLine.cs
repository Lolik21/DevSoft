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
        private Point[] PointsArr;
        public void SetPoints(Point[] Points)
        {
            for (int i = 0; i< Points.Length; i++)
            {
                PointsArr[i] = Points[i];
            }
        }

        public override void CalcPerimetr()
        {
            
        }

        public override void Draw()
        {
            
        }
    }
}
