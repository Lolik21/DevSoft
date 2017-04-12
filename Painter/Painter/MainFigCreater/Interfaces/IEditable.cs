using System.Drawing;

namespace MainFigCreater
{
    public interface IEditable
    {
        void Mark();
        void Move(int dx, int dy);
        void ChangeColor(Color Color);
        bool PIsInFigure(Point Point);
    }
}
