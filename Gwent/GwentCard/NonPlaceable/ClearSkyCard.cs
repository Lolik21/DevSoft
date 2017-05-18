using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class ClearSkyCard : GwentCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            Battleground.CurrWeatherCard.Clear();
        }
    }
}
