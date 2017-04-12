using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MainFigCreater;
using System.Runtime.Serialization;

namespace Painter
{
    [DataContract]
    public class FigList
    {
        [DataMember]
        private List<AbstractFigure> Figures;
        public FigList()
        {
            Figures = new List<AbstractFigure>();
        }
        public void AddToList(AbstractFigure Figure)
        {
            Figures.Add(Figure);
        }
        public AbstractFigure GetItem(int Index)
        {
            return Figures[Index];
        }
        public AbstractFigure FindPoint(Point Point)
        {
            for (int i = Figures.Count-1; i >= 0; i--)
            {
                if (Figures[i] is IEditable)
                {
                    if (Figures[i].PIsInFigure(Point))
                    {
                        return Figures[i];
                    }    
                }       
            }
            return null;
        }
        public void ReadrawFigures()
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                Figures[i].Draw();
            }
        }
        public void SetPainter(Graphics Gr)
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                Figures[i].Painter = Gr;
            }
        }
        public void ResetColors()
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                Figures[i].Pen = new Pen(Figures[i].PenColor);
                Figures[i].Pen.Width = 3;
            }
        }
        public void Clear()
        {
            Figures.Clear();
        }       
        public int Count()
        {
            return Figures.Count();
        }
    }
}
