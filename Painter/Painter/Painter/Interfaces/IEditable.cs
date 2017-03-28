using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter.Interfaces
{
    public interface IEditable
    {
        void Mark();
        void Move(int dx, int dy);
        void ChangeColor(Color Color);
        bool PIsInFigure(Point Point);
    }
}
