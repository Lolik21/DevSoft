using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    public class FigList
    {
        static FigList uniqueFigList;
        public static FigList Instanse()
        {
            if (uniqueFigList == null)
            {
                uniqueFigList = new FigList();
            }
            return uniqueFigList;
        }

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
                if (Figures[i].IS_Seasiable())
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
    }
}
