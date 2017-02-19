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
        private Pen MainPen;
                    


        public mainForm()
        {
            InitializeComponent();
            Init();
            Draw();
        }

        private void Init()
        {
            Canvas = new Bitmap(MainView.Width, MainView.Height);
            Painter = Graphics.FromImage(Canvas);
            MainPen = new Pen(Color.Black);

        }

        private void Draw ()
        {

            MainPen.Width = 10;

            Painter.DrawRectangle(MainPen, 100, 100, 200, 200);

            MainView.Image = Canvas;
            
            
        }
    }
}
