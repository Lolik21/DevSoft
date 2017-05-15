using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public abstract class PlaceableCard : GwentCard, IPlaceable
    {    
        public int CardDefaultStrength { get; set; }
        public int CardCurrStrength { get; set; }
        public void PlaceCard(Battleground Battleground)
        {
            if (Battleground.AffectedCardID != -1)
            {
                Battleground.InsertToLine(this.CardLine, Battleground.AffectedCardID + 1, this);
            }
            else
            {
                Battleground.AddToLine(this.CardLine, this);
            }           
        }
    }
}
