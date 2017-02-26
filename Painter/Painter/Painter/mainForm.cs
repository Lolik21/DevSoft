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
        private Figure DrawingFig;
        private PatternFabric Fabric;               


        public mainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Canvas = new Bitmap(MainView.Width, MainView.Height);
            Painter = Graphics.FromImage(Canvas);
            Fabric = new PatternFabric();          
        }

        
    }
}
