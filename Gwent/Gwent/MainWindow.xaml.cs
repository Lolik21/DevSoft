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
using nDBLoader;
using nGwentCard;

namespace Gwent
{
    
    public partial class MainWindow : Window
    {
        DBLoader Loader = new DBLoader();
        List<GwentCard> UserCards = new List<GwentCard>();

        public MainWindow()
        {
            InitializeComponent();
            Image img = new Image();  
        }
        public void GetMenuControls()
        {
            MainGrid.Children.Clear();
            MainMenu Menu = new MainMenu();
            Menu.MainWindow = this;
            MainGrid.Children.Add(Menu);                 
        }
        public void GetChouseCardControls(List<GwentCard> Cards)
        {
            MainGrid.Children.Clear();
            ChouseCard Chouse = new ChouseCard();
            Chouse.MainWindow = this;
            Chouse.Init(Cards);
            MainGrid.Children.Add(Chouse);
        }
        private void GetBattleFieldCountrols()
        {
            MainGrid.Children.Clear();
            Battlefield Battlefield = new Battlefield();
            MainGrid.Children.Add(Battlefield);
        }

        public void StartGameInit()
        {
            GetBattleFieldCountrols();
        }

        public void ChouseCardInit()
        {
            List<GwentCard> Cards;
            Loader = new DBLoader();
            Cards = Loader.LoadCards();            
            GetChouseCardControls(Cards);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetMenuControls();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Loader.Dispose();
        }
    }

   
}
