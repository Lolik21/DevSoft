using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class WeatherCard: GwentCard
    {
        public override void PerformSpecialAbility(Battleground Battleground)
        {
            foreach (PlaceableCard Card in Battleground.Lines[this.CardLine-1])
            {
                if (!Card.Invinsible)
                    Card.CardCurrStrength = 1;
            }
            foreach (PlaceableCard Card in Battleground.Lines[(this.CardLine - 1) + 3])
            {
                if (!Card.Invinsible)
                    Card.CardCurrStrength = 1;
            }
        }
    }
}
