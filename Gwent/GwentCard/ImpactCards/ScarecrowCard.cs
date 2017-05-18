using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class ScarecrowCard : GwentCard, Iimpact
    {

        public void Impact(Battleground Battlegrnd, int CardLine, int CardPos)
        {
            Battlegrnd.AddToInHandCards(Battlegrnd.Lines[CardLine][CardPos]);
            Battlegrnd.RemoveFromLine(CardLine, Battlegrnd.Lines[CardLine][CardPos]);
        }

        public override void PerformSpecialAbility(Battleground Battleground)
        {
        }
    }
}
