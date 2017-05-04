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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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
        private int CurrFractionID = 1;
        private int UserCardsCount = 0;        
        private List<GwentCard> UserCards = new List<GwentCard>();
        private List<GwentCard> AllCards;

        public ChouseCard()
        {
            InitializeComponent();
            lblCardsCount.Content = UserCardsCount;
        }

        public void Init(List<GwentCard> Cards)
        {
            AllCards = Cards;
            List<Image> LeftImages = InitFractionCards(AllCards);
            lblFraction.Content = Fractions[CurrFractionID - 1];                    
            foreach (Image img in LeftImages)
            {
                AddToGrid(grdAllCards, img);
            }                                     
        }

        private void btnToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (UserCards.Count<22 || UserCardsCount > 32)
            {
                MessageBox.Show("Вы выбрали недопустимое количество карт\n"+
                    "Карт должно быть не менее 22 и не более 32\n"+
                    "Вы не сможете играть с неверны м колиеством карт",
                    "Предупреждение",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
            battlegnd.UserCards = UserCards;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Chouse)].Visibility = Visibility.Collapsed;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Menu)].Visibility = Visibility.Visible;
        }

        private void AddToGrid(Grid grid, UIElement element)
        {
            int columnCount = grid.ColumnDefinitions.Count;
            int childrenCount = grid.Children.Count;
            if (childrenCount % columnCount == 0)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            Grid.SetRow(element, childrenCount / columnCount);
            Grid.SetColumn(element, childrenCount % columnCount);
            grid.Children.Add(element);
        }

        private void RemoveFromGrid(Grid grid, UIElement element)
        {
            int columnCount = grid.ColumnDefinitions.Count;
            int elementRow = Grid.GetRow(element);
            int elementColumn = Grid.GetColumn(element);
            grid.Children.Remove(element);
            foreach (UIElement child in grid.Children)
            {
                int childRow = Grid.GetRow(child);
                int childColumn = Grid.GetColumn(child);
                if ((childRow > elementRow) || (childRow == elementRow && childColumn > elementColumn))
                {
                    if (childColumn == 0)
                    {
                        Grid.SetRow(child, childRow - 1);
                        Grid.SetColumn(child, columnCount - 1);
                    }
                    else
                    {
                        Grid.SetColumn(child, childColumn - 1);
                    }
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
                    BitmapImage bti = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + Card.ToImgPath, UriKind.Absolute));
                    Image img = new Image()
                    {
                        Stretch = Stretch.Fill,
                        Source = bti,
                        Tag = Card,
                        RenderTransformOrigin = new Point(0.5, 0.5),
                        RenderTransform = new ScaleTransform(0.9, 0.9)                      
                    };
                    img.MouseLeftButtonUp += LeftMouse_Up;
                    img.MouseRightButtonUp += RightMouse_Up;
                    img.MouseEnter += Mouse_EnterImage;
                    img.MouseLeave += Mouse_LeaveImage;
                    Images.Add(img);
                }
            }
            return Images; 
        }

        private void LeftMouse_Up(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            int ind = grdAllCards.Children.IndexOf(img);
            if (ind != -1)
            {
                RemoveFromGrid(grdAllCards, img);
                AddToGrid(grdUserCards, img);
                UserCards.Add(img.Tag as GwentCard);
                UserCardsCount++;
                lblValueChanged(UserCardsCount);
                scrvUserCards.ScrollToEnd();          
            }
            else
            {
                RemoveFromGrid(grdUserCards, img);
                AddToGrid(grdAllCards, img);
                UserCards.Remove(img.Tag as GwentCard);
                UserCardsCount--;
                lblValueChanged(UserCardsCount);
            }
        }

        private void ApplyBackBlurEffect(Grid grd)
        {
            BlurEffect blr = new BlurEffect();
            blr.Radius = 4;
            grd.Effect = blr;
        }

        private void UnSetEffect(MainWindow window)
        {
            window.Effect = null;
        }

        private void RightMouse_Up(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            ApplyBackBlurEffect(grdChouseCardChild);
            Image bigimg = new Image();
            bigimg.VerticalAlignment = VerticalAlignment.Center;
            bigimg.HorizontalAlignment = HorizontalAlignment.Center;
            bigimg.Source = img.Source;
            bigimg.Height = img.ActualHeight;
            bigimg.Width = img.ActualWidth;
            bigimg.RenderTransform = new ScaleTransform(2, 2);
            
            grdChouseCard.Children.Add(bigimg);
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
            lblCardsCount.Content = Value;
        }

        private void Mouse_EnterImage(object sender, RoutedEventArgs e)
        {            
            Image img = sender as Image;
            img.Effect = new DropShadowEffect()
            {
                Color = Colors.Blue,
                ShadowDepth = 0,
                BlurRadius = 25                
            };

            ScaleTransform scTransform = new ScaleTransform();
            DoubleAnimation anim = new DoubleAnimation(0.9, 1, TimeSpan.FromMilliseconds(100));
            scTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            scTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            img.RenderTransform = scTransform;         
        }

        private void Mouse_LeaveImage(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            ScaleTransform scTransform = new ScaleTransform();
            DoubleAnimation anim = new DoubleAnimation(1, 0.9, TimeSpan.FromMilliseconds(100));
            scTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            scTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            img.RenderTransform = scTransform;
            img.Effect = null;
        }

        private void btnPrevFraction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID > 1)
            {
                CurrFractionID--;
                UserCards.Clear();
                Init(AllCards);
            }            
        }

        private void btnNextFraction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID < Fractions.Count)
            {
                CurrFractionID++;
                UserCards.Clear();
                Init(AllCards);
            }
            
        }


        private void btnPrevFraction_Click_1(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID > 1)
            {
                CurrFractionID--;
                NextFractionInit();
            }
        }

        private void NextFractionInit()
        {
            lblFraction.Content = Fractions[CurrFractionID - 1];
            UserCards.Clear();
            lblCardsCount.Content = 0;
            Init(AllCards);
        }

        private void btnNextFraction_Click_1(object sender, RoutedEventArgs e)
        {
            if (CurrFractionID < Fractions.Count)
            {
                CurrFractionID++;
                NextFractionInit();
            }
        }
    }
}
