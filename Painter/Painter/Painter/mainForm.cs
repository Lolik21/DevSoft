using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painter
{
    public partial class mainForm : Form
    {
        private Bitmap Canvas;
        private Graphics Painter;
        private Fabric CurrFabric = new LineFabric();
        private Pen PenColor = new Pen(Color.Black,3);
        private List<Point> points = new List<Point>();
        private FigList FigList = FigList.Instanse();
        private Figure SelectedFig = null;                    


        public mainForm()
        {
            InitializeComponent();
            Init();          
        }
        private void Init()
        {
            Canvas = new Bitmap(MainView.Width, MainView.Height);
            Painter = Graphics.FromImage(Canvas);
            lblChousenFig.Text = "Линия";
            lblPointsN.Text = "0";
        }

        
        private void btnLine_Click(object sender, EventArgs e)
        {
            CurrFabric = new LineFabric();
            lblChousenFig.Text = "Линия";
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            CurrFabric = new RectangleFabric();
            lblChousenFig.Text = "Прямоугольник";
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            CurrFabric = new EllipseFabric();
            lblChousenFig.Text = "Эллипс";
        }
        private void btnCurveLine_Click(object sender, EventArgs e)
        {
            CurrFabric = new CurveLineFabric();
            lblChousenFig.Text = "Кривая линия";
        }
        private void btnPoligon_Click(object sender, EventArgs e)
        {
            CurrFabric = new PoligonFabric();
            lblChousenFig.Text = "Многоугольник";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            int i = CurrFabric.CheckPoints(ref points);
            if (i > 0)
            {
                CurrFabric.Painter = Painter;
                Figure Fig;
                Fig = CurrFabric.GetFigure(PenColor, points);
                Fig.Draw();
                lvFigures.Items.Add(Fig.GetName());
                FigList.AddToList(Fig);
            }
            else
            {
                MessageBox.Show("Неверно заданы точки!!");
            }
            for (int j = 0; j < i; j++)
            {
                points.RemoveAt(0);
            }
            lblPointsN.Text = points.Count.ToString();
            MainView.Image = Canvas;
        }        
        private void btnPenColor_Click(object sender, EventArgs e)
        {
            if (this.ColorPenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PenColor.Color = this.ColorPenDialog.Color;
            }
        }
        private void btnClearPoints_Click(object sender, EventArgs e)
        {
            points.Clear();
        }
        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            points.Add(p);
            lblPointsN.Text = points.Count.ToString();
        }

        private void lvFigures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
