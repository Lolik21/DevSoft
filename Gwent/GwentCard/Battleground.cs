﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
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

        public delegate void MessageStateHandler(string Message);
        public event MessageStateHandler ShowNotificationMessage;

        public delegate void SyncCommandHandler(string OpScope, 
            string OpInStackCardCount, string OpInHandCardCount);
        public event SyncCommandHandler SyncCommandArived;

        public delegate void PassHandler();
        public event PassHandler OponentPassed;    

        public int OponentStackCardCount { get; set; }
        public int OponentInHandCardCount { get; set; }
        public int OponentCardPower { get; set; }

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
            Lines = new List<List<GwentCard>>();
            for (int i = 0; i < 6; i++)
            {
                Lines.Add(new List<GwentCard>());
            }        
            SelectedCardID = -1;
            AffectedCardID = -1;
        }

        public void AddWeatherCard(GwentCard AddingCard)
        {
            bool IsAlreadyIn = false;
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
                Cards.Clear();
            }
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
                Net.SendSimpleCommand(this.AffectedCardID, Card.CardID, Card.IsSpecialAbilitiPerformed, false);
            LineCardsChanged(Line);
        }

        public void InsertToLine(int Line, int Ind, GwentCard Card)
        {
            Lines[Line - 1].Insert(Ind, Card);
            if (IsUserTurn)
                Net.SendSimpleCommand(this.AffectedCardID, Card.CardID, Card.IsSpecialAbilitiPerformed, false);
            LineCardsChanged(Line);
        }

        public void RemoveFromLine(int Line, GwentCard Card)
        {
            Lines[Line - 1].Remove(Card);
            if (IsUserTurn)
                Net.SendSimpleCommand(this.AffectedCardID, Card.CardID, Card.IsSpecialAbilitiPerformed, true);
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
            GwentCard Card = GetCardByID(SimpleCard.CardID);
            int Line = Card.CardLine;
            Card.CardLine = (Card.CardLine + 3);
            if (Card.CardLine > 6)
            {
                Card.CardLine = Card.CardLine % 6;
            }

            Card.IsSpecialAbilitiPerformed = Card.WhenSendIsPerformed;
            if (Card is IPlaceable)
            {
                if (!SimpleCard.IsRemoved)
                {
                    (Card as IPlaceable).PlaceCard(this);
                }
                else
                {
                    RemoveFromLine(Card.CardLine, Card);
                }               
            }
            else
            {
                AddWeatherCard(Card);
            }
            Card.CardLine = Line;
        }

        public void EndBattle()
        {
            if (Net != null) this.Net.CloseConnection();
            this.InHandCards.Clear();
            this.InStackCards.Clear();
            this.UsedCards.Clear();          
            this.Lines.Clear();
            if (Control != null) BattleEnd();
        }
    }
}
