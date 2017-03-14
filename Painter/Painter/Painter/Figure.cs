using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    public interface IPerimetr
    {
        void CalcPerimetr();
    }
    public interface IEditable
    {
        bool IS_Seasiable();
        void Mark();
        void Move(int dx, int dy);
        void ChangeColor(Color Color);
        bool PIsInFigure(Point Point);     
    }

    [Serializable]
    public abstract class Figure : IPerimetr, IEditable
    {
        [NonSerialized()]
        protected Pen PenType;
        [NonSerialized()]
        public Graphics _Painter;

        public Color PenColor { get; set; }

        public abstract void Draw();
        public abstract void CalcPerimetr();
        public abstract string GetName();
        public abstract bool IS_Seasiable();
        public abstract void Mark();
        public abstract void Move(int dx, int dy);
        public abstract bool PIsInFigure(Point Point);
        public abstract void DeleteFig();

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
