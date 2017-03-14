using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    [Serializable]
    class Line : Figure
    {
        protected Point x;
        protected Point y;
       
        public Line (Pen PenType, Point x, Point y)
        {
            this.Pen = PenType;
            this.x = x;
            this.y = y;    
        }
        public override void CalcPerimetr()
        {

        }
        public override void Draw()
        {
            Painter.DrawLine(Pen, x, y);    
        }
        public override bool IS_Seasiable()
        {
            return false;
        }
        public override void Move(int dx, int dy)
        {
            throw new NotImplementedException();
        }
        public override void Mark()
        {
            throw new NotImplementedException();
        }
        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White, 3);
            Painter.DrawLine(tmpPen, x, y);
        }
        public override string GetName()
        {
            return "Линия";
        }
        public override bool PIsInFigure(Point Point)
        {
            throw new NotImplementedException();
        }
    }
}
