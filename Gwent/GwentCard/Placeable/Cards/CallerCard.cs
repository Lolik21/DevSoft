using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class CallerCard : PlaceableCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            if (!IsSpecialAbilitiPerformed)
            {
                List<PlaceableCard> CallCards = new List<PlaceableCard>();
                foreach (GwentCard Card in Battleground.InHandCards)
                {
                    PlaceCallCard(Card, Battleground, CallCards);
                }
                foreach (GwentCard Card in Battleground.InStackCards)
                {
                    PlaceCallCard(Card, Battleground, CallCards);
                }

                foreach (CallerCard Card in CallCards)
                {
                    Battleground.InHandCards.Remove(Card);
                    Battleground.InHandTrigger();
                    Battleground.InStackCards.Remove(Card);
                    Card.IsSpecialAbilitiPerformed = true;
                }

                foreach (PlaceableCard Card in CallCards)
                {
                    Card.PlaceCard(Battleground);
                }
                CallCards.Clear();
            }      
        }
        
        private void PlaceCallCard(GwentCard Card, Battleground Battleground, List<PlaceableCard> Cards)
        {
            if (Card is IPlaceable)
            {
                PlaceableCard PlCard = Card as PlaceableCard;
                if (PlCard.CardID == this.CardID)
                {                   
                    Cards.Add(PlCard);
                }
                    
            }
        }
    }
}
