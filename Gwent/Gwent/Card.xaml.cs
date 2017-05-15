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
            } else
            if (CardPower > (Tag as IPlaceable).CardDefaultStrength)
            {
                tbCardPower.Foreground = Brushes.Green;
            }
            else
            {
                tbCardPower.Foreground = Brushes.Red;
            }
        }
    }
}
