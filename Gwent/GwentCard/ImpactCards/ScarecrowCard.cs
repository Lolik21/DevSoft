using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class ScarecrowCard : GwentCard, Iimpact
    {

        public bool Impact(Battleground Battlegrnd, int CardLine, int CardPos)
        {
            if (CardPos != -1)
            {
                GwentCard Card = Battlegrnd.Lines[CardLine][CardPos];
                if (Card.CardLine > 3 || Card.Invinsible)
                {
                    return false;
                }
                else
                {
                    Battlegrnd.RemoveFromLine(Card.CardLine, Card, false);
                    Battlegrnd.AddToInHandCards(Card);
                    return true;
                }                         
            }
            else
            {
                return false;
            }
            
        }

        public override void PerformSpecialAbility(Battleground Battleground)
        {
        }
    }
}
