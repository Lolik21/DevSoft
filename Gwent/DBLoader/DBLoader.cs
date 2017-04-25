using System;
using System.Collections.Generic;
using nGwentCard;
using MySql.Data.MySqlClient;

namespace nDBLoader
{
    public class DBLoader : IDisposable
    {
        MySqlConnection MySqlDB;
        const string SERVER_IP = "127.0.0.1";
        const string DB_NAME = "gwent";
        const string USER_ID = "root";
        const string PASSWD = "HelliMomJasdo41631";

        private MySqlConnectionStringBuilder ConnectToDB()
        {
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = SERVER_IP;
            mysqlCSB.Database = DB_NAME;
            mysqlCSB.UserID = USER_ID;
            mysqlCSB.Password = PASSWD;
            return mysqlCSB;
        }

        private string GetByNameFromTable(int ID,string Name,string Table)
        {
            string queryString = @"SELECT "+Name+" FROM "+Table+" WHERE ID = "
                                    + Convert.ToString(ID);
            MySqlCommand Command = new MySqlCommand(queryString, MySqlDB);
            using (MySqlDataReader dr = Command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return dr[Name].ToString();
                    }
                }
            }
            return null;
        }

        public List<GwentCard> LoadCards()
        {
            List<GwentCard> Cards = new List<GwentCard>();

            string queryString = @"SELECT * FROM gwentcards";

            MySqlCommand Command = new MySqlCommand(queryString, MySqlDB);
            using (MySqlDataReader dr = Command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {                       
                        Cards.Add(CardCreator(dr, Cards));
                    }
                }
            }

            GetAdditionInfo(Cards);
            return Cards;
        }

        private void GetAdditionInfo(List<GwentCard> Cards)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                GetFractionInfo(Cards[i]);
                GetSpAbilityInfo(Cards[i]);
            }
        }

        private GwentCard CardCreator(MySqlDataReader dr, List<GwentCard> Cards)
        {
            GwentCard card;
            if (dr["Line"] == null && dr["FractionID"] == null)
            {
                card = new GwentCard();
            }
            else card = new PlaceableCard();
            GetMainInfo(dr, card);
            if (card is IPlaceable)
            {
                GetPlaceableCardInfo(dr, card as PlaceableCard);
            }
            return card;
        }

        private void GetPlaceableCardInfo(MySqlDataReader dr,PlaceableCard card)
        {
            card.CardDefaultStrength = (int)dr["CardPower"];
        }
        private void GetFractionInfo(GwentCard card)
        {
            if (card.FractionID != 0)
            {                          
                card.FractionName = GetByNameFromTable(card.FractionID, "Name", "fractions");
                card.FractionDescription = GetByNameFromTable(card.FractionID, "Description", "fractions");
            }
        }
        private void GetSpAbilityInfo(GwentCard card)
        {
            if (card.SpAbilityID != 0)
            {
                card.SpAbilityDescription = GetByNameFromTable(card.SpAbilityID, "Description", "specialabilites");
                card.SpAbilityName = GetByNameFromTable(card.SpAbilityID, "Name", "specialabilites");
            }
        }
        private void GetMainInfo(MySqlDataReader dr, GwentCard card)
        {
            card.CardID = (int)dr["CardID"];
            card.CardLine = (int)dr["Line"];
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
