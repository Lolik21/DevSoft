using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    [Serializable]
    class Ellipse : Rectangle
    {
        public Ellipse(Pen PenType, int x1, int y1, int x2, int y2) : base(PenType, x1, y1, x2, y2)
        {
        }
        public override bool IS_Seasiable()
        {
            return true;
        }
        public override void Mark()
        {
            Pen MarkPen = new Pen(Color.Red,Pen.Width);
            Painter.DrawEllipse(MarkPen, x1, y1, Width, Height);
        }        
        public override void Draw()
        {
            Painter.DrawEllipse(Pen, x1, y1, Width, Height);
            Painter.FillEllipse(Pen.Brush, x1, y1, Width, Height);
        }
        public override string GetName()
        {
            return "Эллипс";
        }
        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White, 3);
            Painter.DrawEllipse(tmpPen, x1, y1, Width, Height);
            Painter.FillEllipse(tmpPen.Brush, x1, y1, Width, Height);
        }
    }
}
