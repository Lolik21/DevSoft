using System.Windows;
using System.Windows.Controls;

namespace Gwent
{
    public partial class MainMenu : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Close();
        }

        private void btnChouseCard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Menu)].Visibility = Visibility.Collapsed;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Chouse)].Visibility = Visibility.Visible;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.battlegrd.UserCardsCount >= 22 && MainWindow.battlegrd.UserCardsCount <= 32)
            {
                MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Menu)].Visibility = Visibility.Collapsed;
                MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Battlefield)].Visibility = Visibility.Visible;
                MainWindow.Battlefield.InitBattle();
            }               
        }
    }
}
