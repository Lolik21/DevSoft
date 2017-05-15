using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nGwentCard;

namespace nDBLoader
{
    class Fabric
    {
        public GwentCard GetCard(string SpecialAbilityName)
        {
            switch (SpecialAbilityName)
            {
                case "SpyCard": return new SpyCard();
                case "CallerCard": return new CallerCard();
                case "EnspireCard": return new EnspireCard();
                case "KillerCard": return new KillerCard();
                case "MedicCard": return new MedicCard();
                case "WeatherCard": return new WeatherCard();
                case "HornCard": return new HornCard();
                case "ScarecrowCard": return new ScarecrowCard();
                case "HealCard": return new HealCard();
                case "UnionCard": return new UnionCard();
                default: return new SimpleCard();
            }              
        }
    }
}
