using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class KillerCard : PlaceableCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            if (!IsSpecialAbilitiPerformed)
            {
                int LineCardStrength = 0;
                foreach (PlaceableCard Card in Battleground.Lines[this.CardLine - 1])
                {
                    LineCardStrength += Card.CardDefaultStrength;
                }

                if (LineCardStrength >= 10)
                {
                    int Max = 0;
                    List<GwentCard> MaxCards = new List<GwentCard>();
                    foreach (PlaceableCard OponentCard in Battleground.Lines[(this.CardLine - 1) + 3])
                    {
                        if (!(OponentCard.Invinsible))
                        {
                            if (OponentCard.CardCurrStrength > Max)
                            {
                                MaxCards.Clear();
                                MaxCards.Add(OponentCard);
                            }
                            if (OponentCard.CardCurrStrength == Max)
                            {
                                MaxCards.Add(OponentCard);
                            }
                        }
                    }

                    foreach (PlaceableCard Card in MaxCards)
                    {
                        Battleground.RemoveFromLine((this.CardLine - 1) + 3, Card);
                    }
                    IsSpecialAbilitiPerformed = true;
                }           
            }
        }
    }
}
