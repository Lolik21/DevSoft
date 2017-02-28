using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    class Rectangle : Figure
    {
        protected int _x1, _x2, _y1, _y2;
        protected int _Width, _Height;        

        public override void Draw()
        {
            Painter.FillRectangle(PenType.Brush, x1, y1, _Width, _Height);
            Painter.DrawRectangle(PenType, x1, y1, _Width, _Height);             
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
            this._Height = Math.Abs(this._y1 - this._y2);
            this._Width = Math.Abs(this._x1 - this._x2);
        }

        public override void CalcPerimetr()
        {

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
