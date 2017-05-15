using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using nGwentCard;

namespace Battlefield
{

    public partial class Battlefield : UserControl
    {
        public MainWindow MainWindow { get; set; }
        private Battleground Battlegrnd { get; set; }
        private Connection Connection { get; set; }
        public Battlefield()
        {
            InitializeComponent();
        }

        private void InitBattle(Battleground Battlegrnd)
        {
            this.Battlegrnd = Battlegrnd;
            Connection net = new Connection();
        }
    }
}
