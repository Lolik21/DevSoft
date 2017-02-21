using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    class Rectangle : Figure
    {
        
        protected int _Width, _Height;

        public override void Draw()
        {
            Painter.DrawRectangle(PenType,x1, y1, x2, y2);
        }

        public int x1
        {
            get { return this._x1; }
            set
            {
                _x1 = value;
                CalculateHW();
            }
        }

        public int x2
        {
            get { return this._x2; }
            set
            {
                _x2 = value;
                CalculateHW();
            }
        }

        public int y1
        {
            get { return this._y1; }
            set
            {
                _y1 = value;
                CalculateHW();
            }
        }

        public int y2
        {
            get { return this._y2; }
            set
            {
                _y2 = value;
                CalculateHW();
            }
        }

        private void CalculateHW()
        {
            this._Height = this._y1 - this._y2;
            this._Width = this._x1 - this._x2;
        }

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            this._x1 = x1;
            this._x2 = x2;
            this._y1 = y1;
            this._y2 = y2;
            CalculateHW();
        }
    }
}
