using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using nGwentCard;

namespace Gwent
{
    /// <summary>
    /// Логика взаимодействия для GwentCard.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        public void SetCardPower(int Power)
        {
            tbCardPower.Text = Convert.ToString(Power);
            CardPowerChanged();
        }

        public void HideEllipse()
        {
            Ellipse.Visibility = System.Windows.Visibility.Hidden;
            tbCardPower.Visibility = System.Windows.Visibility.Hidden;
        }

        public int GetCardPower()
        {
            return Convert.ToInt32(tbCardPower.Text);
        }

        public void SetImage(Image Img)
        {
            imgCardImage.Source = Img.Source;
        }

        public void CardPowerChanged()
        {                   
            int CardPower = Convert.ToInt32(tbCardPower.Text);
            if (CardPower == (Tag as IPlaceable).CardDefaultStrength)
            {
                tbCardPower.Foreground = Brushes.Black;
                this.Effect = null;
            } else
            if (CardPower > (Tag as IPlaceable).CardDefaultStrength)
            {
                tbCardPower.Foreground = Brushes.Green;
                this.Effect = new DropShadowEffect()
                {
                    Color = Colors.Green,
                    ShadowDepth = 0,
                    BlurRadius = 25
                };
            }
            else
            {
                tbCardPower.Foreground = Brushes.Red;
                this.Effect = new DropShadowEffect()
                {
                    Color = Colors.Red,
                    ShadowDepth = 0,
                    BlurRadius = 25
                };
            }
        }
    }
}
