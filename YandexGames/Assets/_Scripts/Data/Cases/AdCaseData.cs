using System.Collections.Generic;
using _Scripts.Data.Cards;

namespace _Scripts.Data.Cases
{
    public class AdCaseData : CaseData
    {
        public int AdCount { get; }

        public AdCaseData(string id, string name, List<CardData> cardPool, int adCount)
            : base(id, name, cardPool, CaseType.Advertisement)
        {
            AdCount = adCount;
        }
    }
}