using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class MedicCard : PlaceableCard
    {        
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            if (!IsSpecialAbilitiPerformed)
            {
                Random rnd = new Random();

                List<PlaceableCard> PlaceableCards = new List<PlaceableCard>();

                foreach (GwentCard Card in Battleground.UsedCards)
                {
                    if (Card is IPlaceable && !(Card.Invinsible))
                    {
                        PlaceableCards.Add(Card as PlaceableCard);
                    }
                }

                if (PlaceableCards.Count != 0)
                {
                    int RandomNumber = rnd.Next(PlaceableCards.Count);
                    PlaceableCards[RandomNumber].PlaceCard(Battleground);                
                }
                PlaceableCards.Clear();
                IsSpecialAbilitiPerformed = true;
            }                     
        }
    }
}
