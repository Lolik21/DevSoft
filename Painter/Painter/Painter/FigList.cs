using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    [Serializable]
    public class FigList
    {        
        private List<Figure> Figures;

        public FigList()
        {
            Figures = new List<Figure>();
        }
        public void AddToList(Figure Figure)
        {
            Figures.Add(Figure);
        }
        public Figure GetItem(int Index)
        {
            return Figures[Index];
        }
        public Figure FindPoint(Point Point)
        {
            for (int i = Figures.Count-1; i >= 0; i--)
            {
                if (Figures[i] is Interfaces.IEditable)
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
