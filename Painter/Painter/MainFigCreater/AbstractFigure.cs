using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace MainFigCreater
{
    [DataContract]
    public abstract class AbstractFigure
    {
        [IgnoreDataMember]
        protected Pen PenType;
        [IgnoreDataMember]
        public Graphics _Painter;
        [DataMember]
        public Color PenColor { get; set; }

        public abstract void Draw();
        public abstract void DeleteFig();
        public abstract void Move(int dx, int dy);
        public abstract bool PIsInFigure(Point Point);
        public abstract void Mark();
        public abstract string GetName();
        public void ChangeColor(Color Color)
        {
            Pen.Color = Color;
            Draw();
        }
        [IgnoreDataMember]
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
        [IgnoreDataMember]
        public Graphics Painter
        {
            get { return _Painter; }
            set { _Painter = value; }
        }
    }
}
