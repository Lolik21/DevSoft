using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    public abstract class Figure
    {
        protected int _x1, _x2, _y1, _y2;
        protected float _fx1, _fx2, _fy1, _fx3;
        protected Pen PenType;
        protected Brush BrushType;
        protected Graphics Painter;

        public abstract void Draw();

        public Pen Pen
        {
            get { return PenType }
            set
            {
                if (value != null)
                {
                    this.PenType = value;
                }
            }
        }

        public Brush Brush
        {
            get { return this.BrushType; }
            set
            {
                if (value != null)
                {
                    this.BrushType = value;
                }
            }
        }



    }
}
