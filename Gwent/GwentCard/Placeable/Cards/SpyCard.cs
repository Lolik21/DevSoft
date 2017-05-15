using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class SpyCard : PlaceableCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            if (!IsSpecialAbilitiPerformed)
            {
                Random rnd = new Random();
                for (int i = 0; i < 2; i++)
                {
                    if (Battleground.InStackCards.Count >= 1)
                    {
                        int RandomNumber = rnd.Next(Battleground.InStackCards.Count);
                        GwentCard Card = Battleground.InStackCards[RandomNumber];
                        Battleground.InStackCards.Remove(Card);
                        Battleground.AddToInHandCards(Card);
                    }
                }
                IsSpecialAbilitiPerformed = true;
            }             
        }
    }
}
