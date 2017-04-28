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
    }
}
