using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Markup;


namespace nGwentCard
{
    public class Battleground
    {
        public List<GwentCard> UserCards { get; set; }
        public List<GwentCard> AllCards { get; set; }
        public List<GwentCard> InHandCards { get; set; }
        public List<GwentCard> InStackCards { get; set; }
        public List<GwentCard> UsedCards { get; set; }
        public List<GwentCard> CurrWeatherCard { get; set; }
        public List<FractionInfo> Fractions { get; set; }
        public List<List<GwentCard>> Lines { get; set; }
        public Grid PlayGroundGrid { get; set; }
        public Connection Net { get; set; }
        public UserControl Control { get; set; }
        public int UserCardsPower { get; set; }
        public int UserCardsCount { get; set; }
        public List<GwentCard> OponentUsedCards { get; set; }


        public delegate void BattleEndHandler();
        public event BattleEndHandler BattleEnd;

        public delegate void LinesCardStateHandler(int Line);
        public event LinesCardStateHandler LineCardsChanged;

        public delegate void InHandCardHandler();
        public event InHandCardHandler InHandCardsAdded;

        public delegate void RoundEndHandler();
        public event RoundEndHandler RoundEnded;

        public delegate void WeatherCardHandler();
        public event WeatherCardHandler WeatherChanged;

        public delegate void UsedCardChangedHandler();
        public event UsedCardChangedHandler UsedCardsChanged;
        public event UsedCardChangedHandler OponentUsedCardsChanged;


        public delegate void MessageStateHandler(string Message);
        public event MessageStateHandler ShowNotificationMessage;

        public delegate void SyncCommandHandler(string OpScope, 
            string OpInStackCardCount, string OpInHandCardCount);
        public event SyncCommandHandler SyncCommandArived;

        public delegate void PassHandler();
        public event PassHandler OponentPassed;    

        public bool IsUserTurn { get; set; }

        public int SelectedCardID { get; set; }
        public int AffectedCardID { get; set; }

        public Battleground()
        {
            UserCards = new List<GwentCard>();
            UsedCards = new List<GwentCard>();
            InHandCards = new List<GwentCard>();
            InStackCards = new List<GwentCard>();
            CurrWeatherCard = new List<GwentCard>();
            OponentUsedCards = new List<GwentCard>();
            Lines = new List<List<GwentCard>>();
            for (int i = 0; i < 6; i++)
            {
                Lines.Add(new List<GwentCard>());
            }        
            SelectedCardID = -1;
            AffectedCardID = -1;
        }

        public void UserUsedCardsChanged()
        {
            UsedCardsChanged();
        }

        public void OponentUsedCardsChangedTrigger()
        {
            OponentUsedCardsChanged();
        }

        public void AddWeatherCard(GwentCard AddingCard)
        {
            bool IsAlreadyIn = false;

            if (this.IsUserTurn)
            {
                Net.SendSimpleCommand(this.AffectedCardID, AddingCard.CardLine, 
                    AddingCard.CardID, AddingCard.IsSpecialAbilitiPerformed, false, false);
            }

            foreach (GwentCard Card in CurrWeatherCard)
            {
                if (AddingCard.CardID == Card.CardID)
                {
                    IsAlreadyIn = true;
                }
            }
            if (!IsAlreadyIn)
            {
                CurrWeatherCard.Add(AddingCard);
                ChangedWeatherTrigger();
            }
        }

        public void ChangedWeatherTrigger()
        {
            WeatherChanged();
        }

        public void Passed()
        {
            OponentPassed();
        }

        public void EndRound()
        {
            foreach(List<GwentCard> Cards in Lines)
            {
                foreach (GwentCard Card in Cards)
                {
                    if (Card.CardLine > 3)
                    {
                        OponentUsedCards.Add(Card);
                    }
                    else
                    {
                        UsedCards.Add(Card);
                    }
                }
                Cards.Clear();
            }
            OponentUsedCardsChanged();
            UsedCardsChanged();
            this.UserCardsPower = 0;
            RoundEnded();
        }

        public void ShowNotMessage(string Message)
        {
            ShowNotificationMessage(Message);
        }

        public void Sync(string OpScope,
            string OpInStackCardCount, string OpInHandCardCount)
        {
            SyncCommandArived(OpScope, OpInStackCardCount, OpInHandCardCount);
        }

        public void GetInHandCards()
        {
            Random rnd = new Random();
            for (int i = 0; i<10;  i++)
            {
                int RandomNumber = rnd.Next(InStackCards.Count - 1);
                InHandCards.Add(InStackCards[RandomNumber]);
                InStackCards.RemoveAt(RandomNumber);
            }
        }

        public void AddToInHandCards(GwentCard Card)
        {
            this.InHandCards.Add(Card);
            InHandTrigger();
        }

        public void InHandTrigger()
        {
            InHandCardsAdded();
        }

        private GwentCard CopyGwentCard(GwentCard Source)
        {
            string CardXamlString = XamlWriter.Save(Source);
            GwentCard SplitedCard = XamlReader.Parse(CardXamlString) as GwentCard;
            SplitedCard.Count = 1;
            return SplitedCard;
        }

        public void SplitUserCards(List<GwentCard> UserCards)
        {
            this.InStackCards = new List<GwentCard>();

            foreach (GwentCard card in UserCards)
            {
                if (card.Count > 1)
                {
                    for (int i = 0; i < card.Count; i++)
                    {
                        GwentCard SplitedCard = CopyGwentCard(card);
                        this.InStackCards.Add(SplitedCard);
                    }
                }
                else
                {
                    this.InStackCards.Add(card);
                }
            }
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

        public void InitSpAbility(List<GwentCard> UserCards)
        {
            foreach (GwentCard Card in UserCards)
            {
                Card.IsSpecialAbilitiPerformed = false;
            }
        }

        public void AddToLine(int Line, GwentCard Card)
        {
            Lines[Line-1].Add(Card);
            if (IsUserTurn)
                Net.SendSimpleCommand(this.AffectedCardID,Line ,Card.CardID, Card.IsSpecialAbilitiPerformed, false, false);
            LineCardsChanged(Line);
        }

        public void InsertToLine(int Line, int Ind, GwentCard Card)
        {
            Lines[Line - 1].Insert(Ind, Card);
            if (IsUserTurn)
                Net.SendSimpleCommand(this.AffectedCardID, Line, Card.CardID, Card.IsSpecialAbilitiPerformed, false, false);
            LineCardsChanged(Line);
        }

        private int GetInd(GwentCard Card, List<GwentCard> Cards)
        {
            foreach (GwentCard CurrCard in Cards)
            {
                if (CurrCard.CardID == Card.CardID) return Cards.IndexOf(CurrCard);
            }
            return -1;
        }

        public void RemoveFromLine(int Line, GwentCard Card, bool IsToUsed)
        {
            int Ind = GetInd(Card, Lines[Line - 1]);
            Lines[Line - 1].RemoveAt(Ind);
            if (IsUserTurn)
                Net.SendSimpleCommand(this.AffectedCardID, Line, Card.CardID, 
                    Card.IsSpecialAbilitiPerformed, true, IsToUsed);
            if (this.IsUserTurn)
            {
                if (IsToUsed)
                {
                    UsedCards.Add(Card);
                    UsedCardsChanged();
                }       
            }
            else
            {
                if (IsToUsed)
                {
                    OponentUsedCards.Add(Card);
                    OponentUsedCardsChanged();
                }               
            }
            LineCardsChanged(Line);
        }

        public void EndTurn()
        {
            Net.SendSyncCommand();
            Net.SendEndTurnCommand();
        }

        public void RightEndBattle()
        {
            Net.SendLeaveCommand();
            EndBattle();
        }

       private GwentCard GetCardByID(int CardID)
        {
            foreach (GwentCard Card in AllCards)
            {
                if (Card.CardID == CardID)
                {
                    return Card;
                }
            }
            return null;
        }

        public void CardArived(ISimple SimpleCard)
        {
            this.AffectedCardID = SimpleCard.AffectedCardPos;
            GwentCard FindedCard = GetCardByID(SimpleCard.CardID);
            GwentCard Card = CopyGwentCard(FindedCard);       
            Card.IsSpecialAbilitiPerformed = Card.WhenSendIsPerformed;
            PlaceFindedCard(Card, SimpleCard);
        }

        public void PlaceFindedCard(GwentCard Card, ISimple SimpleCard)
        {
            if (Card is IPlaceable)
            {
                Card.CardLine = (SimpleCard.CardLine + 3);
                if (Card.CardLine > 6)
                {
                    Card.CardLine = Card.CardLine % 6;
                }
                if (!SimpleCard.IsRemoved)
                {
                    (Card as IPlaceable).PlaceCard(this);
                }
                else
                {
                    RemoveFromLine(Card.CardLine, Card, SimpleCard.IsToUsed);
                }
            }
            else
            {
                AddWeatherCard(Card);
            }
            this.SelectedCardID = -1;
            this.AffectedCardID = -1;
        }

        public void EndBattle()
        {
            if (Net != null) this.Net.CloseConnection();
            this.InHandCards.Clear();
            this.InStackCards.Clear();
            this.UsedCards.Clear();
            this.OponentUsedCards.Clear();
            this.CurrWeatherCard.Clear();                    
            foreach (List<GwentCard> List in Lines)
            {
                List.Clear();
            }
            if (Control != null) BattleEnd();
        }
    }
}
