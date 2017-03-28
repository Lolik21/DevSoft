using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{

    [Serializable]
    public abstract class Figure 
    {
        [NonSerialized()]
        protected Pen PenType;
        [NonSerialized()]
        public Graphics _Painter;

        public Color PenColor { get; set; }

        public abstract void Draw();
        public abstract string GetName();
        public abstract void DeleteFig();
        public abstract void Move(int dx, int dy);
        public abstract bool PIsInFigure(Point Point);
        public abstract void Mark();
        public void ChangeColor(Color Color)
        {
            Pen.Color = Color;
            Draw();
        }
        public Pen Pen
        {
            get { return PenType; }
            set
            {
                if (value != null)
                {
                    this.PenType = value;
                    PenColor = value.Color;
                }
            }
        }        
        public Graphics Painter
        {
            get { return _Painter; }
            set { _Painter = value; }
        }     
    }
}
