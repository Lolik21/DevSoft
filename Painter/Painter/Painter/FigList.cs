using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    class FigList
    {
        private List<Figure> Figures;
        public void AddToList(Figure Figure)
        {
            Figures.Add(Figure);
        }
    }
}
