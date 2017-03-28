using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    [Serializable]
    class Poligon : Figure, Interfaces.IEditable
    {
        protected Point[] PointsArr;
        public Poligon(Pen pen, List<Point> points)
        {
            PointsArr = new Point[points.Count];
            PointsArr = points.ToArray();
            this.Pen = pen;
        }
        public override void Mark()
        {
            Pen MarkPen = new Pen(Color.Red, Pen.Width);
            Painter.DrawPolygon(MarkPen, PointsArr);
        }
        public override void Move(int dx, int dy)
        {
            DeleteFig();
            for (int i = 0; i < PointsArr.Length; i++)
                PointsArr[i].X = PointsArr[i].X + dx;
            for (int i = 0; i < PointsArr.Length; i++)
                PointsArr[i].Y = PointsArr[i].Y + dy;
            Draw();
        }
        public override bool PIsInFigure(Point Point)
        {
            int MaxX = FindMaxX(PointsArr);
            int MaxY = FindMaxY(PointsArr);
            int MinX = FindMinX(PointsArr);
            int MinY = FindMinY(PointsArr);
            if ((Point.X > MinX && Point.X < MaxX) &&
                (Point.Y > MinY && Point.Y < MaxY))
            {
                return true;
            }
            else return false;
        }

        public override void DeleteFig()
        {
            Pen tmpPen = new Pen(Color.White, 3);
            Painter.DrawPolygon(tmpPen, PointsArr);
            Painter.FillPolygon(tmpPen.Brush, PointsArr);
        }
        public override void Draw()
        {
            Painter.DrawPolygon(Pen, PointsArr);
            Painter.FillPolygon(Pen.Brush, PointsArr);
        }       
        public override string GetName()
        {
            return "Многоугольник";
        }            
        private int FindMaxX(Point[] Points)
        {
            int Max = 0;
            for (int i = 0; i<Points.Length; i++)
            {
                if (Points[i].X > Max)
                {
                    Max = Points[i].X;
                }
            }
            return Max;
        }
        private int FindMaxY(Point[] Points)
        {
            int Max = 0;
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].Y > Max)
                {
                    Max = Points[i].Y;
                }
            }
            return Max;

        }
        private int FindMinX(Point[] Points)
        {
            int Min = int.MaxValue;
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].X < Min)
                {
                    Min = Points[i].X;
                }
            }
            return Min;
        }
        private int FindMinY(Point[] Points)
        {
            int Min = int.MaxValue;
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].Y < Min)
                {
                    Min = Points[i].Y;
                }
            }
            return Min;
        }
    }
}
