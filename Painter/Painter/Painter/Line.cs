using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    class Line : Figure
    {
        protected int _x1, _y1;
        protected int _x2, _y2;

        public int x1
        {
            get { return this._x1; }
            set
            {
                _x1 = value;
            }
        }
        public int x2
        {
            get { return this._x2; }
            set
            {
                _x2 = value;
            }
        }
        public int y1
        {
            get { return this._y1; }
            set
            {
                _y1 = value;
            }
        }
        public int y2
        {
            get { return this._y2; }
            set
            {
                _y2 = value;
            }
        }

        public Line (int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public override void Draw()
        {
            Painter.DrawLine(PenType, x1, y1, x2, y2);
        }
    }
}
