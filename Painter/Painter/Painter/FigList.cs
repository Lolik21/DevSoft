using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
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
    }
}
