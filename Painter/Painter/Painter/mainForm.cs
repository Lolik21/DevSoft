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
        private FigList FigList = new FigList();
        private Figure SelectedFig = null;
        private bool MouseIsDown = false;
        private Point PrevPoint = new Point();
        private Point CurrPoint = new Point();
        private FigSerialization FigSerializator = new FigSerialization();

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
                Pen Pen = new Pen(PenColor.Color, PenColor.Width);
                Fig = CurrFabric.GetFigure(Pen, points);
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
            lblPointsN.Text = points.Count.ToString();
        }
        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            points.Add(p);            
        }
        private void lvFigures_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReadrawSelected();
            if (lvFigures.SelectedIndices.Count > 0)
            {
                int ind = 0;
                ind = lvFigures.SelectedIndices[0];
                SelectedFig = FigList.GetItem(ind);
                MarkFigure(SelectedFig);
            }                     
        }
        private void btnChangeFig_Click(object sender, EventArgs e)
        {
            if (SelectedFig != null)
            {
                SelectedFig.ChangeColor(PenColor.Color);
                SelectedFig.Draw();
                MainView.Image = Canvas;
                SelectedFig = null;
                lvFigures.SelectedIndices.Clear();
            }
        }

        private void MarkFigure(Figure SelectedFig)
        {                        
            if (SelectedFig.IS_Seasiable())
            {
                SelectedFig.Mark();
                MainView.Image = Canvas;
            }
        }
        private void ReadrawSelected()
        {
            if (SelectedFig != null)
            {
                FigList.ReadrawFigures();
                MainView.Image = Canvas;
            }
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            lvFigures.SelectedIndices.Clear();
            ReadrawSelected();
            Point p = new Point();
            p.X = e.X;
            p.Y = e.Y;
            SelectedFig = FigList.FindPoint(p);
            if (SelectedFig != null)
            {
                MouseIsDown = true;
                PrevPoint = p;
                MarkFigure(SelectedFig);
            }
        }
        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseIsDown) points.RemoveAt(points.Count - 1);
            MouseIsDown = false;
            lblPointsN.Text = points.Count.ToString();
            FigList.ReadrawFigures();
        }
        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
            {
                CurrPoint.X = e.X;
                CurrPoint.Y = e.Y;
                int dx =  CurrPoint.X - PrevPoint.X;
                int dy =  CurrPoint.Y - PrevPoint.Y;
                SelectedFig.Move(dx, dy);
                FigList.ReadrawFigures();
                PrevPoint = CurrPoint;
                MainView.Image = Canvas;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "Dat Files |*.dat";
                openFileDialog.Title = "Открыть фигуры";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    FigList.Clear();
                    lvFigures.Items.Clear();                 
                    FigList = FigSerializator.Deserialize(openFileDialog.FileName);
                    FigList.ResetColors();
                    FigList.SetPainter(Painter);
                    for (int i = 0; i< FigList.Count(); i++)
                    {
                        lvFigures.Items.Add(FigList.GetItem(i).GetName());
                    }
                    FigList.ReadrawFigures();
                    MainView.Image = Canvas;

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Dat Files |*.dat";
                saveFileDialog.Title = "Сохранить фигуры";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "")
                {
                    FigSerializator.Serialize(saveFileDialog.FileName, FigList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
