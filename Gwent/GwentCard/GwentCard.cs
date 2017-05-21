
using System.Windows.Controls;


namespace nGwentCard
{
    public abstract class GwentCard
    {       
        public int CardID { get; set; }
        public string CardName { get; set; }
        public string CardDescription { get; set; }
        public int FractionID { get; set; }
        public string FractionName { get; set; }
        public string FractionDescription { get; set; }
        public int SpAbilityID { get; set; }
        public string SpAbilityName { get; set; }
        public string SpAbilityDescription { get; set; }
        public string ToImgPath { get; set; }
        public string ToBattleImgPath { get; set; }
        public int Count { get; set; }
        public int DefaultCount { get; set; }
        public int CardLine { get; set; }
        public bool Invinsible { get; set; }
        public Image Image { get; set; }
        public bool IsSpecialAbilitiPerformed { get; set; }
        public bool WhenSendIsPerformed { get; set; }
        public abstract void PerformSpecialAbility(Battleground Battleground);
    }
}
