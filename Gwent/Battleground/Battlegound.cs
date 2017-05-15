using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent;
using nGwentCard;
using System.Windows.Markup;
using System.Windows.Controls;


namespace Battleground
{
    public class Battleground
    {
        public List<GwentCard> UserCards { get; set; }
        public List<GwentCard> AllCards { get; set; }
        public List<GwentCard> InHandCards { get; set; }
        public List<GwentCard> InStackCards { get; set; }
        private List<GwentCard> SplitedUserCards { get; set; }
        public List<FractionInfo> Fractions { get; set; }
        public Battlefield Battlefield { get; set; }


        public int OponentStackCardCount { get; set; }
        public int OponentInHandCardCount { get; set; }


        public List<Grid> LinesGrids { get; set; }

        public Battleground()
        {
            UserCards = new List<GwentCard>();
            InHandCards = new List<GwentCard>();
            InStackCards = new List<GwentCard>();
            LinesGrids = new List<Grid>();
        }

        private GwentCard CopyGwentCard(GwentCard Source)
        {
            string CardXamlString = XamlWriter.Save(Source);
            GwentCard SplitedCard = XamlReader.Parse(CardXamlString) as GwentCard;
            SplitedCard.Count = 1;
            return SplitedCard;
        }

        public List<GwentCard> SplitUserCards(List<GwentCard> UserCards)
        {
            this.SplitedUserCards = new List<GwentCard>();

            foreach (GwentCard card in UserCards)
            {
                if (card.Count > 1)
                {
                    for (int i = 0; i < card.Count; i++)
                    {
                        GwentCard SplitedCard = CopyGwentCard(card);
                        this.SplitedUserCards.Add(SplitedCard);
                    }
                }
                else
                {
                    this.SplitedUserCards.Add(card);
                }
            }


            return this.SplitedUserCards;
        }

        public void InitStrength(List<GwentCard> UserCards)
        {
            foreach (GwentCard Card in UserCards)
            {
                if (Card is IPlaceable)
                {
                    IPlaceable PlasiableCard = Card as IPlaceable;
                    PlasiableCard.CardCurrStrength = PlasiableCard.CardDefaultStrength;
                }
            }
        }



        public void MedicAbility()
        {

        }

        public void PlaceCard()
        {

        }

        public void RemoveCard()
        {

        }

        public void SpyAbility()
        {

        }

        public void WeatherAbility()
        {

        }

        public void HornAbility()
        {

        }

        public void EnspireAbility()
        {

        }


    }
}

}
