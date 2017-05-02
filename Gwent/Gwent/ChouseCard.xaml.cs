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

namespace Gwent
{
    
    public partial class ChouseCard : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public Battleground battlegnd { get; set; }
        public List<string> Fractions { get; set; }
        private List<Image> LeftImages;
        private List<Image> RightImages;
        private int CurrFractionID = 1;
        private int UserCardsCount = 0;
        private List<GwentCard> AllCards;
        private List<GwentCard> UserCards;
        private Thickness OnMouseImageMagrin = new Thickness(0, 0, 0, 0);
        private Thickness StdImageMagrin = new Thickness(10, 10, 10, 10);


        public ChouseCard()
        {
            InitializeComponent();
            grdAllCards.MouseEnter += Grid_MouseEnter;
            grdAllCards.MouseLeave += Grid_MouseLeave;
            grdUserCards.MouseEnter += Grid_MouseEnter;
            grdUserCards.MouseLeave += Grid_MouseLeave;
            lblCardsCount.Content = UserCardsCount;
        }

        public void Init(List<GwentCard> Cards)
        {
            AllCards = Cards;
            LeftImages = InitFractionCards(AllCards);
            lblFraction.Content = Fractions[CurrFractionID - 1];                    
            DisplayImages(grdAllCards,LeftImages);                                           
        }

        private void btnToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (UserCards.Count<22 || UserCardsCount > 32)
            {
                MessageBox.Show("Вы выбрали недопустимое количество карт\n 
                    Карт должно быть не менее 22 и не более 32\n
                    Вы не сможете играть с неверны м колиеством карт",
                    "Предупреждение",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
            battlegnd.UserCards = UserCards;
            MainWindow.Chouse.Visibility = Visibility.Collapsed;
            MainWindow.Menu.Visibility = Visibility.Visible;
        }

        private void AddToGrid(Grid MyGrid, Image Image, int Row, int Colum)
        {
            Grid.SetRow(Image, Row);
            Grid.SetColumn(Image, Colum);
            MyGrid.Children.Add(Image);          
        }

        private void DisplayImages(Grid Grid, List<Image> Images)
        {
            const int CARDS_ON_ROW = 3;
            int Counter = 0;
            Grid.RowDefinitions.Clear();
            while (Counter < Images.Count)
            {
                RowDefinition row = new RowDefinition();              
                Grid.RowDefinitions.Add(row);
                for (int i = 0; i < CARDS_ON_ROW; i++)
                {
                    if (Counter < Images.Count)
                    {
                        AddToGrid(Grid, Images[Counter], (Counter / CARDS_ON_ROW) , i); 
                    }
                    Counter++;
                }
            }
                
        }

        private List<Image> InitFractionCards(List<GwentCard> Cards)
        {
            List<Image> Images = new List<Image>();
            foreach (GwentCard Card in Cards)
            {
                if ((Card.FractionID == CurrFractionID) || (Card.FractionID == 0))
                {
                    Image img = new Image();
                    BitmapImage bti = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + Card.ToImgPath, UriKind.Absolute));
                    img.Stretch = Stretch.Fill;
                    img.Source = bti;
                    img.Tag = Card;
                    img.MouseEnter += Mouse_EnterImage;
                    img.MouseLeave += Mouse_LeaveImage;
                    img.MouseDown += Mouse_LeftImagesClick;
                    img.Margin = StdImageMagrin;
                    Images.Add(img);
                }
            }
            return Images; 
        }

        private void Mouse_RightImagesClick(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.MouseDown += Mouse_LeftImagesClick;
            img.MouseDown -= Mouse_RightImagesClick;
            int ind = RightImages.IndexOf(img);
            LeftImages.Add(RightImages[ind]);
            RightImages.RemoveAt(ind);
            DisplayImages(grdAllCards, LeftImages);
            DisplayImages(grdUserCards, RightImages);
            UserCards.Remove(img.Tag as GwentCard);
            UserCardsCount--;
            lblValueChanged(UserCardsCount);
            lblCardsCount.Content = UserCardsCount;
        }

        private void Mouse_LeftImagesClick(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.MouseDown -= Mouse_LeftImagesClick;
            img.MouseDown += Mouse_RightImagesClick;
            int ind = LeftImages.IndexOf(img);
            RightImages.Add(LeftImages[ind]);
            LeftImages.RemoveAt(ind);
            DisplayImages(grdAllCards, LeftImages);
            DisplayImages(grdUserCards, RightImages);
            UserCards.Add(img.Tag as GwentCard);
            UserCardsCount++;
            lblValueChanged(UserCardsCount);
            lblCardsCount.Content = UserCardsCount;
        }

        private void lblValueChanged(int Value)
        {
            if (Value < 22 || Value > 32)
            {
                lblCardsCount.Foreground = System.Windows.Media.Brushes.Red;
            }           
            else
            {
                lblCardsCount.Foreground = System.Windows.Media.Brushes.White;
            }               
        }

        private void Mouse_EnterImage(object sender, RoutedEventArgs e)
        {            
            Image img = sender as Image;
            img.Margin = OnMouseImageMagrin;
        }

        private void Mouse_LeaveImage(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            img.Margin = StdImageMagrin;
        }

        private void btnPrevFraction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID > 1)
            {
                CurrFractionID--;
                UserCards.Clear();
                LeftImages.Clear();
                RightImages.Clear();
                Init(AllCards);
            }            
        }

        private void btnNextFraction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID < Fractions.Count)
            {
                CurrFractionID++;
                UserCards.Clear();
                LeftImages.Clear();
                RightImages.Clear();
                Init(AllCards);
            }
            
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grd = sender as Grid;
            grd.MaxHeight = grd.ActualHeight;       
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grd = sender as Grid;
            grd.MaxHeight = MaxHeight;
        }
    }
}
