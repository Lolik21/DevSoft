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
        private Figure DrawingFig ;            


        public mainForm()
        {
            InitializeComponent();
            Init();          
        }

        private void Init()
        {
            Canvas = new Bitmap(MainView.Width, MainView.Height);
            Painter = Graphics.FromImage(Canvas);    
        }

        private void tbPointsCount_MouseLeave(object sender, EventArgs e)
        {
            cbChousePoints.Items.Clear();
            string tmpStr = tbPointsCount.Text;
            if (tmpStr.Trim(' ') != "")
            {
                try
                {
                    int PointsCount = Convert.ToInt32(tmpStr);
                    if (PointsCount <= 10) InitCbPoints(PointsCount);
                    else
                    {
                        MessageBox.Show("Число не долно быть больше 10");
                        tbPointsCount.Clear();
                    }                      
                }
                catch
                {
                    MessageBox.Show("Введите корректное число");
                    tbPointsCount.Clear();
                }
            }                  
        }

        private void InitCbPoints(int PointsCount)
        {
            for (int i = 0; i<PointsCount; i++)
            {
                cbChousePoints.Items.Add("Точка " + Convert.ToString(i+1));
            }
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {          
            DrawingFig = new Rectangle(0, 0, 0, 0);
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            DrawingFig = new Ellipse(0, 0, 0, 0);
        }
        private void btnCurveLine_Click(object sender, EventArgs e)
        {
            DrawingFig = new CurveLine();
        }
        private void btnPoligon_Click(object sender, EventArgs e)
        {
            DrawingFig = new Poligon();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            
            MainView.Image = Canvas;
        }
    }
}
