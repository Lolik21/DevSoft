using System.Collections.Generic;
using System.Drawing;
using System;


namespace MainFigCreater
{
   
    public abstract class AbstractFabric
    {
        private Graphics _Painter;
        public abstract string GetImgName();     
        public Graphics Painter
        {
            get { return _Painter; }
            set { _Painter = value; }
        }
        private Pen PenType;
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
        public abstract AbstractFigure GetFigure(Pen Pen, List<Point> points);
        public abstract int CheckPoints(ref List<Point> points);
        public abstract Type GetFType();
        

    }
}
