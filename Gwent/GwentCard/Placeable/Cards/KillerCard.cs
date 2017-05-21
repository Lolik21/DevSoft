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
                    if (Card.CardID != this.CardID)
                        LineCardStrength += Card.CardCurrStrength;
                }

                if (LineCardStrength >= 10)
                {
                    int Max = 0;
                    List<GwentCard> MaxCards = new List<GwentCard>();
                    int CurrCardLine = ((this.CardLine - 1) + 3);
                    foreach (PlaceableCard OponentCard in Battleground.Lines[CurrCardLine])
                    {
                        if (!(OponentCard.Invinsible))
                        {
                            if (OponentCard.CardCurrStrength > Max)
                            {
                                MaxCards.Clear();
                                MaxCards.Add(OponentCard);
                                Max = OponentCard.CardCurrStrength;
                            }else
                            if (OponentCard.CardCurrStrength == Max)
                            {
                                MaxCards.Add(OponentCard);
                            }
                        }
                    }

                    foreach (PlaceableCard Card in MaxCards)
                    {
                        Battleground.RemoveFromLine(CurrCardLine + 1, Card, true);
                    }
                    IsSpecialAbilitiPerformed = true;
                }           
            }
        }
    }
}
