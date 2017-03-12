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
      //  bool IS_Seasiable();
      //  void Mark();
      //  void Move(int dx, int dy);
        void ChangeColor(Color Color);      
    }


    public abstract class Figure : IPerimetr, IEditable
    {
        protected Pen PenType;
        protected Graphics _Painter;

        public abstract void Draw();
        public abstract void CalcPerimetr();
        public abstract string GetName();


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
