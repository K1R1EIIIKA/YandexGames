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
    }
}