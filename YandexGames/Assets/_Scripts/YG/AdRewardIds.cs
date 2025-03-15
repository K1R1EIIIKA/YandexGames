using System;
using System.Collections.Generic;
using _Scripts.BuffLogic.Base;

namespace _Scripts.YG
{
    public static class AdRewardIds
    {
        public const int OneAndHalfMoneyAdId = 1;
        public const int DoubleMoneyAdId = 2;
        public const int TripleMoneyAdId = 3;
        public const int Discount10AdId = 4;
        public const int Discount20AdId = 5;
        public const int Discount50AdId = 6;
        public const int BigCharacterAdId = 7;
        public const int SmallCharacterAdId = 8;
        public const int CrazyCharacterAdId = 9;
        public const int MoreLoot1AdId = 10;
        public const int MoreLoot2AdId = 11;
        public const int AutoClickAdId = 12;
        public const int AutoMoneyPerClickAdId = 13;
        public const int AdCaseId = 14;
        public static int Count => 13;

        public static List<int> GetAdRewardIdsList()
        {
            return new List<int>
            {
                OneAndHalfMoneyAdId,
                DoubleMoneyAdId,
                TripleMoneyAdId,
                Discount10AdId,
                Discount20AdId,
                Discount50AdId,
                BigCharacterAdId,
                SmallCharacterAdId,
                CrazyCharacterAdId,
                MoreLoot1AdId,
                MoreLoot2AdId,
                AutoClickAdId,
                AutoMoneyPerClickAdId
            };
        }

        public static string GetAdRewardName(int id)
        {
            return id switch
            {
                OneAndHalfMoneyAdId => "1.5x Money",
                DoubleMoneyAdId => "2x Money",
                TripleMoneyAdId => "3x Money",
                Discount10AdId => "10% Discount",
                Discount20AdId => "20% Discount",
                Discount50AdId => "50% Discount",
                BigCharacterAdId => "Big Character",
                SmallCharacterAdId => "Small Character",
                CrazyCharacterAdId => "Crazy Character",
                MoreLoot1AdId => "More Loot 1",
                MoreLoot2AdId => "More Loot 2",
                AutoClickAdId => "Auto Click",
                AutoMoneyPerClickAdId => "Auto Money Per Click",
                _ => "Unknown"
            };
        }

        public static (BuffCategory, string, float) GetRewardBuff(int id)
        {
            return id switch
            {
                OneAndHalfMoneyAdId => (BuffCategory.Temporary, "1.5x Money",  10f),
                DoubleMoneyAdId => (BuffCategory.Temporary, "2x Money", 10f),
                TripleMoneyAdId => (BuffCategory.Temporary, "3x Money", 5f),
                Discount10AdId => (BuffCategory.Temporary, "10% Discount",  10f),
                Discount20AdId => (BuffCategory.Temporary, "20% Discount",  10f),
                Discount50AdId => (BuffCategory.Temporary, "50% Discount",  5f),
                BigCharacterAdId => (BuffCategory.Temporary, "Big Character",  10f),
                SmallCharacterAdId => (BuffCategory.Temporary, "Small Character",  10f),
                CrazyCharacterAdId => (BuffCategory.Temporary, "Crazy Character", 10f),
                MoreLoot1AdId => (BuffCategory.Temporary, "More Loot 1",  10f),
                MoreLoot2AdId => (BuffCategory.Temporary, "More Loot 2", 10f),
                AutoClickAdId => (BuffCategory.Temporary, "Auto Click", 10f),
                AutoMoneyPerClickAdId => (BuffCategory.Instant, "Auto Money Per Click", 100f),
                _ => (BuffCategory.Temporary, "Unknown", 1f)
            };
        }

        public static (BuffCategory, string, float) GetAdRewardBuff(int id)
        {
            return id switch
            {
                OneAndHalfMoneyAdId => (BuffCategory.Temporary, "1.5x Money",  60f),
                DoubleMoneyAdId => (BuffCategory.Temporary, "2x Money", 60f),
                TripleMoneyAdId => (BuffCategory.Temporary, "3x Money", 30f),
                Discount10AdId => (BuffCategory.Temporary, "10% Discount",  60f),
                Discount20AdId => (BuffCategory.Temporary, "20% Discount",  60f),
                Discount50AdId => (BuffCategory.Temporary, "50% Discount",  20f),
                BigCharacterAdId => (BuffCategory.Temporary, "Big Character",  60f),
                SmallCharacterAdId => (BuffCategory.Temporary, "Small Character",  60f),
                CrazyCharacterAdId => (BuffCategory.Temporary, "Crazy Character", 60f),
                MoreLoot1AdId => (BuffCategory.Temporary, "More Loot 1",  60f),
                MoreLoot2AdId => (BuffCategory.Temporary, "More Loot 2", 60f),
                AutoClickAdId => (BuffCategory.Temporary, "Auto Click", 60f),
                AutoMoneyPerClickAdId => (BuffCategory.Instant, "Auto Money Per Click", 600f),
                _ => (BuffCategory.Temporary, "Unknown", 1f)
            };
        }
    }
}