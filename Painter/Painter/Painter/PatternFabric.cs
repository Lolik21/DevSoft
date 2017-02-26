using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Painter
{
    
    class PatternFabric
    {
        private Graphics _Painter;
        private Pen _FigPen = new Pen(Color.Black, 3);
        private SolidBrush _FigBrush = new SolidBrush(Color.Black);
        public int x1, x2, y1, y2;
        public Point[] Points;

        public Pen FigPen
        {
            get { return _FigPen; }
            set
            {    
                _FigPen = value;           
            }
        }
        public SolidBrush FigBrush
        {
            get { return _FigBrush; }
            set
            {
                _FigBrush = value;
            }
        }
        public Graphics FigGraphics
        {
            get { return _Painter; }
            set { _Painter = value; }
        }

        public PatternFabric()
        {
      
        }

        public void DrawFigure(Rectangle Rectangle)
        {

        }
        public void DrawFigure(Line Line)
        {
            Line.Painter = FigGraphics;
            Line.Pen = FigPen;
            Line.x1 = this.x1;
            Line.y1 = this.y1;
            Line.x2 = this.x2;
            Line.y2 = this.y2;
            Line.Draw();
        }
        public void DrawFigure(Ellipse Ellipse)
        {

        }
        public void DrawFigure(CurveLine CurveLine)
        {

        }

        public void DrawFigure(Poligon Poligon)
        {

        }

        
    }
}
