using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using nGwentCard;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Gwent
{
    
    public partial class Battlefield : UserControl
    {
        public MainWindow MainWindow { get; set; }
        private Battleground Battlegrnd { get; set; }
        private Connection Connection { get; set; }
        private List<Grid> LinesGrids { get; set; }
        private List<Border> LinesBorder { get; set; }
        private DispatcherTimer timer = new DispatcherTimer();
        private List<string> MessageQuery = new List<string>();
        private delegate void BindDelegatesHandler(UIElement Element);


        public Battlefield(Battleground Battle)
        {
            InitializeComponent();
            Battlegrnd = Battle;
            Battlegrnd.LineCardsChanged += CardsChanged;
            Battlegrnd.ShowNotificationMessage += ShowNotificationMessage;
            Battlegrnd.BattleEnd += EndBattle;
            Battlegrnd.InHandCardsAdded += AddedToInHandCards;
            Battlegrnd.OponentPassed += OponentPassed;
            Battlegrnd.SyncCommandArived += Sync;
            Battlegrnd.RoundEnded += OnRoundEnded;
            Battlegrnd.OponentUsedCardsChanged += OnOponentUsedChanged;
            Battlegrnd.UsedCardsChanged += OnUserUsedChanged;
            Battlegrnd.PlayGroundGrid = grdPlayGround;
            Battlegrnd.WeatherChanged += OnWeatherChanged;       
            Battlegrnd.Control = this;
            Battlegrnd.PlayGroundGrid.IsEnabled = false;
            timer.Tick += timerTick;
            timer.Interval = TimeSpan.FromMilliseconds(3000);
            LinesGrids = new List<Grid>();
            LinesBorder = new List<Border>();
            InitLineGrids(LinesGrids);
            InitLineBorders(LinesBorder);
            BindBorderDelegates(LinesBorder);                              
        }

        private void OnWeatherChanged()
        {
            grdWeather.ColumnDefinitions.Clear();
            grdWeather.Children.Clear();
            foreach (GwentCard Card in Battlegrnd.CurrWeatherCard)
            {
                AddNewCardToGrid(Card, grdWeather, BindLineCardsDelegates);          
            }
            for (int i = 0; i < LinesGrids.Count; i++)
            {
                RecalcStrength(i+1);
            }
        }

        private void InitLineBorders(List<Border> Borders)
        {
            Borders.Add(brdLine1);
            Borders.Add(brdLine2);
            Borders.Add(brdLine3);
            Borders.Add(brdLine4);
            Borders.Add(brdLine5);
            Borders.Add(brdLine6);
        }

        private void BindBorderDelegates(List<Border> Borders)
        {
            foreach (Border brd in Borders)
            {
                brd.MouseLeftButtonUp += BorderLeftMouse_Up;
            }
        }

        private GwentCard GetGwentCard(UIElement Element)
        {
            if (Element.GetType() == typeof(Card))
            {
                Card Card = Element as Card;
                return Card.Tag as GwentCard;
            }
            else
            {
                Image img = Element as Image;
                return img.Tag as GwentCard;
            }
            
        }       

        private void BorderLeftMouse_Up(object sender, EventArgs e)
        {
            if (Battlegrnd.SelectedCardID != -1)
            {
                UIElement element = grdInHandCards.Children[Battlegrnd.SelectedCardID];
                GwentCard Card = GetGwentCard(element);
                Border brd = sender as Border;              
                int Line = LinesBorder.IndexOf(brd);
                if (((Card.CardLine-1) == Line) || (Card.CardLine == 0))
                {
                    grdPlayGround.IsEnabled = false;                  
                    if (Card is IPlaceable)
                    {
                        Battlegrnd.InHandCards.RemoveAt(grdInHandCards.Children.IndexOf(element));
                        AddedToInHandCards();
                        (Card as IPlaceable).PlaceCard(Battlegrnd);
                        Battlegrnd.EndTurn();
                        Battlegrnd.SelectedCardID = -1;
                        Battlegrnd.AffectedCardID = -1;
                    }
                    else if (Card is Iimpact)
                    {
                        int Ind = grdInHandCards.Children.IndexOf(element);
                        if ((Card as Iimpact).Impact(Battlegrnd, Line, Battlegrnd.AffectedCardID))
                        {
                            Battlegrnd.InHandCards.RemoveAt(Ind);
                            AddedToInHandCards();
                            Battlegrnd.EndTurn();
                            Battlegrnd.SelectedCardID = -1;
                            Battlegrnd.AffectedCardID = -1;
                        }
                        else grdPlayGround.IsEnabled = true;
                    }
                    else
                    {
                        Battlegrnd.InHandCards.RemoveAt(grdInHandCards.Children.IndexOf(element));
                        AddedToInHandCards();
                        Battlegrnd.AddWeatherCard(Card);
                        Battlegrnd.EndTurn();
                        Battlegrnd.SelectedCardID = -1;
                        Battlegrnd.AffectedCardID = -1;
                    }                   
                }
                lblUserCardsPower.Content = Battlegrnd.UserCardsPower;
                lblInHandCardCount.Content = Battlegrnd.InHandCards.Count;            
            }            
        }

        private void AddedToInHandCards()
        {
            grdInHandCards.Children.Clear();
            grdInHandCards.ColumnDefinitions.Clear();
            foreach (GwentCard Card in Battlegrnd.InHandCards)
            {
                AddNewCardToGrid(Card, grdInHandCards, BindInHandCardsDelegates);
            }
        }

        private void RecalcStrength(int Line)
        {
            foreach (UIElement element in LinesGrids[Line - 1].Children)
            {
                Card Card = element as Card;
                PlaceableCard GwentCard = Card.Tag as PlaceableCard;
                GwentCard.CardCurrStrength = GwentCard.CardDefaultStrength;
            }
            int i = 0;
            ApplyWeatherEffect();
            while (i< LinesGrids[Line-1].Children.Count)
            {
                UIElement element = LinesGrids[Line - 1].Children[i];
                Card Card = element as Card;
                PlaceableCard GwentCard = Card.Tag as PlaceableCard;
                GwentCard.PerformSpecialAbility(Battlegrnd);
                i++;
            }

            foreach (UIElement element in LinesGrids[Line-1].Children)
            {
                Card Card = element as Card;
                PlaceableCard GwentCard = Card.Tag as PlaceableCard;                
                Card.SetCardPower(GwentCard.CardCurrStrength);
            }
            lblInDeckCards.Content = Battlegrnd.InStackCards.Count;            
            RecalcScope();
        }

        private void RecalcScope()
        {
            int YouCardStrength = 0;
            int OponentCardStrength = 0;
            for (int i = 0; i<LinesGrids.Count; i++)
            {                
                if (i<3)
                {
                    foreach(UIElement element in LinesGrids[i].Children)
                    {
                        Card Card = element as Card;
                        PlaceableCard GwentCard = Card.Tag as PlaceableCard;
                        YouCardStrength += GwentCard.CardCurrStrength;
                    }
                }
                else
                {
                    foreach (UIElement element in LinesGrids[i].Children)
                    {
                        Card Card = element as Card;
                        PlaceableCard GwentCard = Card.Tag as PlaceableCard;
                        OponentCardStrength += GwentCard.CardCurrStrength;
                    }
                }
            }
            Battlegrnd.UserCardsPower = YouCardStrength;
            lblOponentCardsPower.Content = OponentCardStrength;
            lblUserCardsPower.Content = YouCardStrength;

        }

        private void ApplyWeatherEffect()
        {
            GwentCard ClearSkyCard = null;
            foreach (GwentCard Card in Battlegrnd.CurrWeatherCard)
            {
                if (Card.GetType() == typeof(ClearSkyCard))
                {
                    ClearSkyCard = Card;
                }
            }

            if (ClearSkyCard != null)
            {
                ClearSkyCard.PerformSpecialAbility(Battlegrnd);             
            }
            else
                foreach (GwentCard Card in Battlegrnd.CurrWeatherCard)
                {
                    Card.PerformSpecialAbility(Battlegrnd);
                }
        }

        private void InitLineGrids(List<Grid> LinesGrids)
        {
            LinesGrids.Add(grdLine1);
            LinesGrids.Add(grdLine2);
            LinesGrids.Add(grdLine3);
            LinesGrids.Add(grdLine4);
            LinesGrids.Add(grdLine5);
            LinesGrids.Add(grdLine6);
        }

        public void InitBattle()
        {
            grdPlayGround.IsEnabled = false;
            Battlegrnd.InitStrength(Battlegrnd.UserCards);
            Battlegrnd.InitSpAbility(Battlegrnd.UserCards);
            Connection net = new Connection();
            Battlegrnd.Net = net;
            Battlegrnd.SplitUserCards(Battlegrnd.UserCards);
            Battlegrnd.GetInHandCards();
            AddedToInHandCards();
            net.battlegnd = Battlegrnd;
            net.InitConnection();
            lblPassed.Visibility = Visibility.Hidden;
            lblOponentPassed.Visibility = Visibility.Hidden;
            InitLables();
            lblOponentCardsPower.Content = 0;
            lblOponentInDeckCards.Content = 0;
            lblOponentInHandCardCount.Content = 0;
            Battlegrnd.UserCardsPower = 0;
            ShowNotificationMessage("Ожидание подключения соперника");                            
        }

        private void InitLables()
        {
            lblInDeckCards.Content = Battlegrnd.InStackCards.Count;
            lblInHandCardCount.Content = Battlegrnd.InHandCards.Count;
            lblUserCardsPower.Content = 0;
        }

        private void CardsChanged(int Line)
        {
            LinesGrids[Line - 1].Children.Clear();
            LinesGrids[Line - 1].RowDefinitions.Clear();
            foreach(GwentCard Card in Battlegrnd.Lines[Line-1])
            {
                AddNewCardToGrid(Card, LinesGrids[Line - 1], BindLineCardsDelegates);
            }
            RecalcStrength(Line);
        }

        private void BindLineCardsDelegates(UIElement Element)
        {
            Element.MouseEnter += Mouse_EnterLineCard;
            Element.MouseLeave += Mouse_LeaveLineCard;
        }

        private void BindInHandCardsDelegates(UIElement Element)
        {
            Element.MouseEnter += Mouse_EnterInHandCard;
            Element.MouseLeave += Mouse_LeaveInHandCard;
            Element.MouseLeftButtonUp += Mouse_LeftButtonUp;
        }

        private void AddNewCardToGrid(GwentCard Card, Grid Grid, BindDelegatesHandler BindDelegates)
        {
            BitmapImage bti = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + Card.ToBattleImgPath, UriKind.Absolute));
            Image img = new Image()
            {
                Stretch = Stretch.Fill,
                Source = bti,
            };

            UIElement MyControl;

            if (Card is IPlaceable)
            {
                Card NewCard = new Card();
                NewCard.Tag = Card;
                NewCard.SetImage(img);
                NewCard.SetCardPower((Card as IPlaceable).CardDefaultStrength);
                MyControl = NewCard;

            }
            else
            {
                Card NewCard = new Card();
                NewCard.Tag = Card;
                NewCard.SetImage(img);
                NewCard.HideEllipse();
                MyControl = NewCard;
            }
            MyControl.RenderTransformOrigin = new Point(0.5, 0.5);
            MyControl.RenderTransform = new ScaleTransform(0.9, 0.9);
            BindDelegates(MyControl);
            AddToGrid(Grid, MyControl);
        }

        private void ShowNotificationMessage(string Message)
        {
            grdPlayGround.IsEnabled = false;
            tbNotification.Text = Message;
            grdNotification.Visibility = Visibility.Visible;
            if (timer.IsEnabled)
            {
                MessageQuery.Add(Message);
            }
            else timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            timer.Stop();
            if (MessageQuery.Count >= 1)
            {
                ShowNotificationMessage(MessageQuery[0]);
                MessageQuery.RemoveAt(0);
            }
            else
            {
                grdNotification.Visibility = Visibility.Hidden;
                if (Battlegrnd.IsUserTurn) grdPlayGround.IsEnabled = true;
            }
                           
        }
        private int GetGridChildrenIndex(UIElement Element)
        {
            foreach(Grid grd in LinesGrids)
            {
                int ind = grd.Children.IndexOf(Element);
                if (ind != -1)
                {
                    return ind;
                }                
            }
            return -1;
        }

        private void ZoomInAnimation(UIElement element)
        {                    
            ScaleTransform scTransform = new ScaleTransform();
            DoubleAnimation anim = new DoubleAnimation(0.9, 1, TimeSpan.FromMilliseconds(100));
            scTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            scTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            element.RenderTransform = scTransform;
        }

        private void ZoomOutAnimation(UIElement element)
        {            
            ScaleTransform scTransform = new ScaleTransform();
            DoubleAnimation anim = new DoubleAnimation(1, 0.9, TimeSpan.FromMilliseconds(100));
            scTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            scTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
            element.RenderTransform = scTransform;
        }

        private void Mouse_EnterLineCard(object sender, RoutedEventArgs args)
        {
            UIElement element = sender as UIElement;
            Battlegrnd.AffectedCardID = GetGridChildrenIndex(element);
            ZoomInAnimation(element);
        }

        private void Mouse_LeaveLineCard(object sender, RoutedEventArgs args)
        {
            UIElement element = sender as UIElement;
            Battlegrnd.AffectedCardID = -1;
            ZoomOutAnimation(element);
        }


        private void Mouse_EnterInHandCard(object sender, RoutedEventArgs args)
        {
            UIElement element = sender as UIElement;
            
            ZoomInAnimation(element);
            element.Effect = new DropShadowEffect()
            {
                Color = Colors.Yellow,
                ShadowDepth = 0,
                BlurRadius = 25
            };
        }

        private void Mouse_LeaveInHandCard(object sender, RoutedEventArgs args)
        {
            UIElement element = sender as UIElement;
           
            ZoomOutAnimation(element);
            if (Battlegrnd.SelectedCardID != grdInHandCards.Children.IndexOf(element)) element.Effect = null;
        }      

        private void Mouse_LeftButtonUp(object obj, RoutedEventArgs args)
        {
            UIElement element = obj as UIElement;
            Battlegrnd.SelectedCardID = grdInHandCards.Children.IndexOf(element);
            foreach (UIElement CurrElem in grdInHandCards.Children)
            {
                if (CurrElem != element)
                {
                    CurrElem.Effect = null;
                }
            }
        }

        private void OponentPassed()
        {
            lblOponentPassed.Visibility = Visibility.Visible;
        }

        private void AddToGrid(Grid grid, UIElement Element)
        {
            int columnCount = grid.ColumnDefinitions.Count;
            int childrenCount = grid.Children.Count; 
            if (columnCount <= childrenCount)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            Grid.SetColumn(Element, grid.Children.Count);
            grid.Children.Add(Element);
        }

        private void EndBattle()
        {
            ClearAllGrids();
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Battlefield)].Visibility = Visibility.Collapsed;
            MainWindow.MainGrid.Children[MainWindow.MainGrid.Children.IndexOf(MainWindow.Menu)].Visibility = Visibility.Visible;
        }

        private void ClearAllGrids()
        {
            foreach  (Grid grd in LinesGrids)
            {
                grd.RowDefinitions.Clear();
                grd.Children.Clear();
            }
            grdInHandCards.RowDefinitions.Clear();
            grdInHandCards.Children.Clear();
            grdOponentUsedCards.Children.Clear();
            grdOponentUsedCards.RowDefinitions.Clear();
            grdUserUsedCards.RowDefinitions.Clear();
            grdUserUsedCards.Children.Clear();
            grdWeather.RowDefinitions.Clear();
            grdWeather.Children.Clear();         
        }

        public void Sync(string OpScope,
            string OpInStackCardCount, string OpInHandCardCount)
        {
            lblOponentCardsPower.Content = OpScope;
            lblOponentInDeckCards.Content = OpInStackCardCount;
            lblOponentInHandCardCount.Content = OpInHandCardCount;
        }

        private void OnRoundEnded()
        {
            foreach (Grid Grid in LinesGrids)
            {              
                InitLables();
                Grid.Children.Clear();
                Grid.ColumnDefinitions.Clear();
                lblOponentPassed.Visibility = Visibility.Hidden;
                lblPassed.Visibility = Visibility.Hidden;
            }
        }

        private void OnOponentUsedChanged()
        {
            grdOponentUsedCards.RowDefinitions.Clear();
            grdOponentUsedCards.Children.Clear();
            foreach (GwentCard Card in Battlegrnd.OponentUsedCards)
            {
                AddNewCardToGrid(Card, grdOponentUsedCards, BindLineCardsDelegates);
            }
        }

        private void OnUserUsedChanged()
        {
            grdUserUsedCards.RowDefinitions.Clear();
            grdUserUsedCards.Children.Clear();
            foreach (GwentCard Card in Battlegrnd.UsedCards)
            {
                AddNewCardToGrid(Card, grdUserUsedCards, BindLineCardsDelegates);
            }
        }

        private void btnPassGame_Click(object sender, RoutedEventArgs e)
        {
            Battlegrnd.Net.SendPassedCommand();
            lblPassed.Visibility = Visibility.Visible;
            grdPlayGround.IsEnabled = false;
            Battlegrnd.EndTurn();
        }

        private void btnLeaveGame_Click(object sender, RoutedEventArgs e)
        {
            Battlegrnd.Net.SendLeaveCommand();
            Battlegrnd.EndBattle();
        }
    }
}
