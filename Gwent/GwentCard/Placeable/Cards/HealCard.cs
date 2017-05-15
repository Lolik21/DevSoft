using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class HealCard : PlaceableCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            foreach (PlaceableCard Card in Battleground.Lines[this.CardLine - 1])
            {
                if (Card != this && !(Card.Invinsible))
                    Card.CardCurrStrength = Card.CardCurrStrength + 1;
            }      
        }
    }
}
