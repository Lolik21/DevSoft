using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Markup;
using nGwentCard;

namespace Gwent
{
    
    public partial class ChouseCard : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public Battleground battlegnd { get; set; }
        public List<FractionInfo> Fractions { get; set; }
        private int CurrFractionID = 1;
        private int UserCardsCount = 0;        
        private List<GwentCard> UserCards = new List<GwentCard>();
        private List<GwentCard> AllCards;

        public ChouseCard()
        {
            InitializeComponent();
            lblCardsCount.Content = UserCardsCount;
            lblCurrCardCount.Visibility = Visibility.Hidden;
        }

        public void Init(List<GwentCard> Cards)
        {
            AllCards = Cards;
            List<Image> LeftImages = InitFractionCards(AllCards);
            lblFraction.Content = Fractions[CurrFractionID - 1].Name;                    
            foreach (Image img in LeftImages)
            {
                AddToGrid(grdAllCards, img);
            }                                     
        }

        private void btnToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (UserCardsCount < 22 || UserCardsCount > 32)
            {
                MessageBox.Show("Вы выбрали недопустимое количество карт: \n"+
                    "Карт должно быть не менее 22 и не более 32.\n"+
                    "Вы не сможете играть с неверным количеством карт!",
                    "Предупреждение",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
            battlegnd.UserCards = UserCards;
            battlegnd.UserCardsCount = UserCardsCount;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Chouse)].Visibility = Visibility.Collapsed;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Menu)].Visibility = Visibility.Visible;
        }

        private void IncImageTagCount(Grid grid,object tag)
        {
            foreach (UIElement element in grid.Children)
            {
                Image img = element as Image;
                GwentCard card = img.Tag as GwentCard;
                if (card.CardID == (tag as GwentCard).CardID)
                {
                    card.Count++;
                }
            }
        }

        private void AddToGrid(Grid grid, UIElement element)
        {
            Image img = element as Image;
            if (GridHasCard(img.Tag as GwentCard, grid))
            {
                IncImageTagCount(grid, img.Tag);
            }
            else
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
            
        }

        private void RemoveFromGrid(Grid grid, UIElement element)
        {
            Image img = element as Image;
            if ((img.Tag as GwentCard).Count > 1)
            {
                (img.Tag as GwentCard).Count--;
            }
            else
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
                    BindDelegates(img);
                    Images.Add(img);
                }
            }
            return Images; 
        }

        private void BindDelegates(Image img)
        {
            img.MouseLeftButtonUp += LeftMouse_Up;
            img.MouseRightButtonUp += RightMouse_Up;
            img.MouseEnter += Mouse_EnterImage;
            img.MouseLeave += Mouse_LeaveImage;
        }

        private void LeftMouse_Up(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            int ind = grdAllCards.Children.IndexOf(img);
            if (ind != -1)
            {
                LeftImageTriggered(img);                        
            }
            else
            {
                RightImageTriggered(img);                   
            }
            lblValueChanged(UserCardsCount);
            lblCurrCardCount.Content = (img.Tag as GwentCard).Count;
        }

        private void LeftImageTriggered(Image img)
        {
            if (IsNeedCopy(img, grdUserCards))
            {
                Image NewImg = DeepImgCopy(img);
                AddToGrid(grdUserCards, NewImg);
                UserCards.Add(NewImg.Tag as GwentCard);
            }
            else
            {
                if (!GridHasCard(img.Tag as GwentCard, grdUserCards))
                {
                    UserCards.Add(img.Tag as GwentCard);
                }
                RemoveFromGrid(grdAllCards, img);
                AddToGrid(grdUserCards, img);              
            }
            UserCardsCount++;
            scrvUserCards.ScrollToEnd();
        }

        private void RightImageTriggered(Image img)
        {
            if (IsNeedCopy(img, grdAllCards))
            {
                Image NewImg = DeepImgCopy(img);
                AddToGrid(grdAllCards, NewImg);
            }
            else
            {
                if ((img.Tag as GwentCard).Count == 1)
                {
                    RemoveFromCardList(UserCards,(img.Tag as GwentCard).CardID);
                }
                RemoveFromGrid(grdUserCards, img);
                AddToGrid(grdAllCards, img);         
            }
            UserCardsCount--;
        }

        private void RemoveFromCardList(List<GwentCard> UserCards, int CardID)
        {
            foreach (GwentCard card in UserCards)
            {
                if (card.CardID == CardID)
                {
                    UserCards.Remove(card);
                    return;
                }
            }
        }

        private bool GridHasCard(GwentCard UserCard, Grid Grid)
        {
            foreach (UIElement element in Grid.Children)
            {
                Image img = element as Image;
                GwentCard card = img.Tag as GwentCard;
                if (card.CardID == UserCard.CardID)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsNeedCopy(Image Image,Grid Grid)
        {
            GwentCard card = Image.Tag as GwentCard;
            if ((card.Count > 1) && (!GridHasCard(card,Grid)))
            {             
                return true;
            }
            else return false;
            
        } 

        private GwentCard CopyCard(GwentCard Source)
        {
            string XmlCard = XamlWriter.Save(Source);         
            GwentCard NewCard = XamlReader.Parse(XmlCard) as GwentCard;
            return NewCard;
        }

        private Image DeepImgCopy(Image Source)
        {                      
            GwentCard NewCard = CopyCard(Source.Tag as GwentCard);
            NewCard.Count = 1;
            (Source.Tag as GwentCard).Count--;
            Image NewImg = new Image()
            {
                Stretch = Stretch.Fill,
                Source = Source.Source.CloneCurrentValue(),
                Tag = NewCard,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new ScaleTransform(0.9, 0.9)
            };           
            BindDelegates(NewImg);
            return NewImg;
        }

        private void ApplyBackBlurEffect(Grid grd)
        {
            BlurEffect blr = new BlurEffect();
            blr.Radius = 10;
            grd.Effect = blr;
        }

        private void RightMouse_Up(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            ApplyBackBlurEffect(grdChouseCard);
            Image bigimg = new Image();
            bigimg.Source = img.Source.CloneCurrentValue();
            bigimg.Height = img.ActualHeight;
            bigimg.Width = img.ActualWidth;
            bigimg.VerticalAlignment = VerticalAlignment.Center;
            bigimg.HorizontalAlignment = HorizontalAlignment.Center;         
            bigimg.RenderTransformOrigin = new Point(0.5, 0.5);
            bigimg.RenderTransform = new ScaleTransform(2.2,2.2);
            grdChouseCard.IsEnabled = false;
            grdCard.Children.Add(bigimg);
            grdCard.MouseUp += BigImgGrid_MouseUp; 
        }

        private void BigImgGrid_MouseUp(object sender, RoutedEventArgs e)
        {
            grdCard.Children.Clear();
            grdChouseCard.Effect = null;
            grdChouseCard.IsEnabled = true;
            grdCard.MouseUp -= BigImgGrid_MouseUp;
        }

        private void lblValueChanged(int Value)
        {
            if (Value < 22 || Value > 32)
            {
                lblCardsCount.Foreground = Brushes.Red;
            }           
            else
            {
                lblCardsCount.Foreground = Brushes.Green;
            }
            lblCardsCount.Content = Value;
        }

        private void Mouse_EnterImage(object sender, RoutedEventArgs e)
        {            
            Image img = sender as Image;           
            ScaleTransform scTransform = new ScaleTransform();
            DoubleAnimation anim = new DoubleAnimation(0.9, 1, TimeSpan.FromMilliseconds(100));
            scTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            scTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            img.RenderTransform = scTransform;
            lblCurrCardCount.Visibility = Visibility.Visible;
            lblCurrCardCount.Content = (img.Tag as GwentCard).Count;
            img.Effect = new DropShadowEffect()
            {
                Color = Colors.Blue,
                ShadowDepth = 0,
                BlurRadius = 25             
            };

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
            lblCurrCardCount.Visibility = Visibility.Hidden;
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
            lblFraction.Content = Fractions[CurrFractionID - 1].Name;
            UserCards.Clear();
            grdAllCards.Children.Clear();
            grdUserCards.Children.Clear();
            lblCardsCount.Content = 0;
            foreach(GwentCard card in AllCards)
            {
                card.Count = card.DefaultCount;
            }
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
