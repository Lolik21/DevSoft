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
        private Fabric CurrFabric;
        private Pen PenColor;
        private List<Point> points;                    


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

        
        private void btnLine_Click(object sender, EventArgs e)
        {
            int i;
            if ( i = CurrFabric.CheckPoints(ref points) > 0)
            {
                
            }
            
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            CurrFabric = new RectangleFabric();
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            CurrFabric = new EllipseFabric();
        }
        private void btnCurveLine_Click(object sender, EventArgs e)
        {
            CurrFabric = new CurveLineFabric();
        }
        private void btnPoligon_Click(object sender, EventArgs e)
        {
            CurrFabric = new PoligonFabric();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            
            MainView.Image = Canvas;
        }

        private void btnBrushColor_Click(object sender, EventArgs e)
        {

        }

        private void btnPenColor_Click(object sender, EventArgs e)
        {
            if (this.ColorPenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PenColor.Color = this.ColorPenDialog.Color;
            }
        }
    }
}
