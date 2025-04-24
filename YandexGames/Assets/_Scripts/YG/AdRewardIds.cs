using System;
using System.Collections.Generic;
using _Scripts.BuffLogic.Base;
using _Scripts.Enums;
using UnityEngine.Localization;
using YG;

namespace _Scripts.YG
{
    public static class AdRewardIds
    {
        private const int OneAndHalfMoneyAdId = 1;
        private const int DoubleMoneyAdId = 2;
        private const int TripleMoneyAdId = 3;
        private const int Discount10AdId = 4;
        private const int Discount20AdId = 5;
        private const int Discount50AdId = 6;
        private const int BigCharacterAdId = 7;
        private const int SmallCharacterAdId = 8;
        private const int CrazyCharacterAdId = 9;
        private const int MoreLoot1AdId = 10;
        private const int MoreLoot2AdId = 11;
        private const int AutoClickAdId = 12;
        private const int AutoMoneyPerClickAdId = 13;
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

        public static (BuffCategory, string, float) GetRewardBuff(int id)
        {
            return id switch
            {
                OneAndHalfMoneyAdId => (BuffCategory.Temporary, $"1.5x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}",  10f),
                DoubleMoneyAdId => (BuffCategory.Temporary, $"2x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}", 10f),
                TripleMoneyAdId => (BuffCategory.Temporary, $"3x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}", 5f),
                Discount10AdId => (BuffCategory.Temporary, $"10% {LocalizedStrings.Discount.GetLocalizedString()}",  10f),
                Discount20AdId => (BuffCategory.Temporary, $"20% {LocalizedStrings.Discount.GetLocalizedString()}",  10f),
                Discount50AdId => (BuffCategory.Temporary, $"50% {LocalizedStrings.Discount.GetLocalizedString()}",  5f),
                BigCharacterAdId => (BuffCategory.Temporary, $"{LocalizedStrings.BigChar.GetLocalizedString()}",  10f),
                SmallCharacterAdId => (BuffCategory.Temporary, $"{LocalizedStrings.SmallChar.GetLocalizedString()}",  10f),
                CrazyCharacterAdId => (BuffCategory.Temporary, $"{LocalizedStrings.CrazyChar.GetLocalizedString()}", 10f),
                MoreLoot1AdId => (BuffCategory.Temporary, $"More Loot 1",  10f),
                MoreLoot2AdId => (BuffCategory.Temporary, $"More Loot 2", 10f),
                AutoClickAdId => (BuffCategory.Temporary, $"{LocalizedStrings.AutoClick.GetLocalizedString()}", 10f),
                AutoMoneyPerClickAdId => (BuffCategory.Instant, $"Auto Money Per Click", 100f),
                _ => (BuffCategory.Temporary, "Unknown", 1f)
            };
        }

        public static (BuffCategory, string, float) GetAdRewardBuff(int id)
        {
            return id switch
            {
                OneAndHalfMoneyAdId => (BuffCategory.Temporary,$"1.5x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}",  60f),
                DoubleMoneyAdId => (BuffCategory.Temporary,$"2x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}", 60f),
                TripleMoneyAdId => (BuffCategory.Temporary,$"3x {LocalizedStrings.ClickPowerBuff.GetLocalizedString()}", 30f),
                Discount10AdId => (BuffCategory.Temporary,$"10% {LocalizedStrings.Discount.GetLocalizedString()}",  60f),
                Discount20AdId => (BuffCategory.Temporary,$"20% {LocalizedStrings.Discount.GetLocalizedString()}",  60f),
                Discount50AdId => (BuffCategory.Temporary,$"50% {LocalizedStrings.Discount.GetLocalizedString()}",  20f),
                BigCharacterAdId => (BuffCategory.Temporary,$"{LocalizedStrings.BigChar.GetLocalizedString()}",  60f),
                SmallCharacterAdId => (BuffCategory.Temporary,$"{LocalizedStrings.SmallChar.GetLocalizedString()}",  60f),
                CrazyCharacterAdId => (BuffCategory.Temporary,$"{LocalizedStrings.CrazyChar.GetLocalizedString()}", 60f),
                MoreLoot1AdId => (BuffCategory.Temporary,$"More Loot 1",  60f),
                MoreLoot2AdId => (BuffCategory.Temporary,$"More Loot 2", 60f),
                AutoClickAdId => (BuffCategory.Temporary,$"{LocalizedStrings.AutoClick.GetLocalizedString()}", 60f),
                AutoMoneyPerClickAdId => (BuffCategory.Instant,$"Auto Money Per Click", 600f),
                _ => (BuffCategory.Temporary,$"Unknown", 1f)
            };
        }
    }
}