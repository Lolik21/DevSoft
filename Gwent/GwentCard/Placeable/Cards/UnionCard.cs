using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class UnionCard : PlaceableCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            if (!IsSpecialAbilitiPerformed)
            {
                List<PlaceableCard> UnionCards = new List<PlaceableCard>();
                foreach (PlaceableCard Card in Battleground.Lines[this.CardLine - 1])
                {
                    if (Card.CardID == this.CardID)
                    {
                        UnionCards.Add(Card);
                    }
                    else
                    {
                        UpStrength(UnionCards);
                    }
                }
                UpStrength(UnionCards);
            }          
        }

        public void UpStrength(List<PlaceableCard> UnionCards)
        {
            if (UnionCards.Count >= 2)
            {
                for (int i = 1; i< UnionCards.Count; i++)
                {
                    UnionCards[i].IsSpecialAbilitiPerformed = true;
                }
                foreach (PlaceableCard EnCard in UnionCards)
                {
                    EnCard.CardCurrStrength = EnCard.CardCurrStrength * UnionCards.Count;
                }
                UnionCards.Clear();
            }
        }
    }
}
