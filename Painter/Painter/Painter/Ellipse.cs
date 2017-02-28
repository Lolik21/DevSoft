using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    class Ellipse : Rectangle
    {
        public Ellipse(Pen PenType, int x1, int y1, int x2, int y2) : base(PenType, x1, y1, x2, y2)
        {
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
    }
}
