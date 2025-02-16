using System.Collections.Generic;
using _Scripts.Data.Cards;

namespace _Scripts.Data.Cases
{
    public class MoneyCaseData : CaseData
    {
        private int Price { get; }

        public MoneyCaseData(string id, string name, List<CardData> cardPool, int price)
            : base(id, name, cardPool, CaseType.Money)
        {
            Price = price;
        }
    }
}