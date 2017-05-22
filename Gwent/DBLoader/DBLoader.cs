using System;
using System.Collections.Generic;
using nGwentCard;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace nDBLoader
{
    public class DBLoader : IDisposable
    {
        MySqlConnection MySqlDB;
        string SERVER_IP = ConfigurationManager.AppSettings["DBServerIP"];
        string DB_NAME = ConfigurationManager.AppSettings["DBName"];
        string USER_ID = ConfigurationManager.AppSettings["DBUser"];
        string PASSWD = ConfigurationManager.AppSettings["DBUserPasswd"];
        Fabric fab = new Fabric();
        public List<FractionInfo> Fractions = new List<FractionInfo>();       

        private MySqlConnectionStringBuilder ConnectToDB()
        {
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = SERVER_IP;
            mysqlCSB.Database = DB_NAME;
            mysqlCSB.UserID = USER_ID;
            mysqlCSB.Password = PASSWD;
            return mysqlCSB;
        }
              
        public List<GwentCard> LoadCards()
        {
            List<GwentCard> Cards = new List<GwentCard>();
            List<AdditionInfo> SpecialAbilityInfo = new List<AdditionInfo>();
            Fractions = new List<FractionInfo>();

            GetAdditionInfo(SpecialAbilityInfo, Fractions);

            string queryString = @"SELECT * FROM gwentcards";
            MySqlCommand Command = new MySqlCommand(queryString, MySqlDB);
            using (MySqlDataReader dr = Command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {                       
                        Cards.Add(CardCreator(dr,SpecialAbilityInfo, Fractions));
                    }
                }
            }        
            return Cards;
        }

        private void GetAdditionInfo(List<AdditionInfo> SpecialAbilityInfo , List<FractionInfo> FractionInfo)
        {
            GetFractionInfo(FractionInfo);
            GetSpAbilityInfo(SpecialAbilityInfo);
        }
        

        private void FillWithAdditionInfo(GwentCard Card,
                                            List<AdditionInfo> SpecialAbilityInfo, List<FractionInfo> FractionInfo)
        {
            if (Card.FractionID > 0)
            {
                Card.FractionName = FractionInfo[Card.FractionID-1].Name;
                Card.FractionDescription = FractionInfo[Card.FractionID - 1].Description;
            }

            if (Card.SpAbilityID > 0)
            {
                Card.SpAbilityName = SpecialAbilityInfo[Card.SpAbilityID-1].Name;
                Card.SpAbilityDescription = SpecialAbilityInfo[Card.SpAbilityID - 1].Description;
                Card.WhenSendIsPerformed = SpecialAbilityInfo[Card.SpAbilityID - 1].IsPerformedAfterSend;
            }
        }

        private GwentCard CardCreator(MySqlDataReader dr, List<AdditionInfo>
                                            SpecialAbilityInfo, List<FractionInfo> FractionInfo)
        {
            GwentCard card;
            string SpAbiliteName = GetSpAbiliteName(dr, SpecialAbilityInfo);
     
            card = fab.GetCard(SpAbiliteName);
            GetMainInfo(dr, card);
            if (card is IPlaceable)
            {
                GetPlaceableCardInfo(dr, card as PlaceableCard);
            }
            FillWithAdditionInfo(card, SpecialAbilityInfo, FractionInfo);
         
            return card;
        }

        private string GetSpAbiliteName(MySqlDataReader dr, List<AdditionInfo> SpecialAbilityInfo)
        {
            int SpAbilityID;
            if (dr["SpecialAbilitesID"] != DBNull.Value)
            {
                SpAbilityID = (int)dr["SpecialAbilitesID"];
                return SpecialAbilityInfo[SpAbilityID - 1].Name;
            } else return null;
        }

        private void GetPlaceableCardInfo(MySqlDataReader dr,PlaceableCard card)
        {
            card.CardDefaultStrength = (int)dr["CardPower"];
        }


        private void GetFractionInfo(List<FractionInfo> FractionInfo)
        {
            string queryString = @"SELECT * FROM fractions";
            MySqlCommand Command = new MySqlCommand(queryString, MySqlDB);
            using (MySqlDataReader dr = Command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FractionInfo inf = new FractionInfo();
                        inf.Name = dr["Name"].ToString();
                        inf.ID = (int)dr["ID"];
                        inf.Description = dr["Description"].ToString();
                        inf.ToImgPath = dr["ImagePath"].ToString();
                        FractionInfo.Add(inf);                   
                    }
                }
            }
        }
        private void GetSpAbilityInfo(List<AdditionInfo> SpecialAbilityInfo)
        {
            string queryString = @"SELECT * FROM specialabilites";
            MySqlCommand Command = new MySqlCommand(queryString, MySqlDB);
            using (MySqlDataReader dr = Command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AdditionInfo inf = new AdditionInfo();
                        inf.Name = dr["Name"].ToString();
                        inf.ID = (int)dr["ID"];
                        inf.Description = dr["Description"].ToString();
                        inf.IsPerformedAfterSend = Convert.ToBoolean(dr["WhenSendIsPerformed"]);
                        SpecialAbilityInfo.Add(inf);
                    }
                }
            }
        }


        private void GetMainInfo(MySqlDataReader dr, GwentCard card)
        {
            card.CardID = (int)dr["CardID"];
            card.CardLine = (int)dr["Line"];
            card.Count = (int)dr["Count"];
            card.DefaultCount = card.Count;
            card.ToBattleImgPath = dr["BattleImagePath"].ToString();
            card.Invinsible = Convert.ToBoolean(dr["Invinsible"]);
            card.CardDescription = dr["Description"].ToString();
            card.CardName = dr["Name"].ToString();
            card.ToImgPath = dr["ImagePath"].ToString();
            if (dr["FractionID"] != DBNull.Value) card.FractionID = (int)dr["FractionID"];
            if (dr["SpecialAbilitesID"] != DBNull.Value) card.SpAbilityID = (int)dr["SpecialAbilitesID"];
        }

        public DBLoader()
        {
            MySqlDB = new MySqlConnection();        
            MySqlDB.ConnectionString = ConnectToDB().ConnectionString;
            MySqlDB.Open();         
        }

        public void Dispose()
        {
            if (MySqlDB != null)
            {
                MySqlDB.Close();
                MySqlDB = null;
            }
        }

    }
}
