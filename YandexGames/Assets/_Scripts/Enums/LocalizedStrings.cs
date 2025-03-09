using _Scripts.BuffLogic;
using _Scripts.Controllers;
using _Scripts.Data.Cards;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace _Scripts.Enums
{
    public static class LocalizedStrings
    {
        private const string BuffsTableReference = "BuffsTable";
        private const string MainTableReference = "Main";

        private static readonly LocalizedString BonusNone = new() { TableReference = BuffsTableReference, TableEntryReference = "BonusNone" };
        private static readonly LocalizedString Discount5 = new() { TableReference = BuffsTableReference, TableEntryReference = "Discount5" };
        private static readonly LocalizedString Discount10 = new() { TableReference = BuffsTableReference, TableEntryReference = "Discount10" };
        private static readonly LocalizedString Discount15 = new() { TableReference = BuffsTableReference, TableEntryReference = "Discount15" };
        private static readonly LocalizedString Discount20 = new() { TableReference = BuffsTableReference, TableEntryReference = "Discount20" };
        private static readonly LocalizedString Discount25 = new() { TableReference = BuffsTableReference, TableEntryReference = "Discount25" };

        private static readonly LocalizedString Click10 = new() { TableReference = BuffsTableReference, TableEntryReference = "Click10" };
        private static readonly LocalizedString Click20 = new() { TableReference = BuffsTableReference, TableEntryReference = "Click20" };
        private static readonly LocalizedString Click30 = new() { TableReference = BuffsTableReference, TableEntryReference = "Click30" };
        private static readonly LocalizedString Click40 = new() { TableReference = BuffsTableReference, TableEntryReference = "Click40" };
        private static readonly LocalizedString Click50 = new() { TableReference = BuffsTableReference, TableEntryReference = "Click50" };

        private static readonly LocalizedString SortByRarity = new() { TableReference = BuffsTableReference, TableEntryReference = "sort_rarity" };
        private static readonly LocalizedString SortByCount = new() { TableReference = BuffsTableReference, TableEntryReference = "sort_count" };
        private static readonly LocalizedString SortByHas = new() { TableReference = BuffsTableReference, TableEntryReference = "sort_has" };

        private static readonly LocalizedString RareCommon = new() { TableReference = BuffsTableReference, TableEntryReference = "rare_common" };
        private static readonly LocalizedString RareRare = new() { TableReference = BuffsTableReference, TableEntryReference = "rare_rare" };
        private static readonly LocalizedString RareSuperRare = new() { TableReference = BuffsTableReference, TableEntryReference = "rare_super_rare" };
        private static readonly LocalizedString RareSuperMegaRare = new() { TableReference = BuffsTableReference, TableEntryReference = "rare_super_mega_rare" };
        private static readonly LocalizedString RareSpecial = new() { TableReference = BuffsTableReference, TableEntryReference = "rare_special" };

        public static readonly LocalizedString ClickPower = new() { TableReference = MainTableReference, TableEntryReference = "click_power" };
        public static readonly LocalizedString GeneralClickPower = new() { TableReference = MainTableReference, TableEntryReference = "general_click_power" };

        public static string ConvertBedBuffToString(BedBuffType buffType)
        {
            return buffType switch
            {
                BedBuffType.None => BonusNone.GetLocalizedString(),
                BedBuffType.Discount5 => Discount5.GetLocalizedString(),
                BedBuffType.Discount10 => Discount10.GetLocalizedString(),
                BedBuffType.Discount15 => Discount15.GetLocalizedString(),
                BedBuffType.Discount20 => Discount20.GetLocalizedString(),
                BedBuffType.Discount25 => Discount25.GetLocalizedString(),
                _ => "???"
            };
        }

        public static string ConvertBackgroundBuffToString(BackgroundBuffType buffType)
        {
            return buffType switch
            {
                BackgroundBuffType.None => BonusNone.GetLocalizedString(),
                BackgroundBuffType.Click10 => Click10.GetLocalizedString(),
                BackgroundBuffType.Click20 => Click20.GetLocalizedString(),
                BackgroundBuffType.Click30 => Click30.GetLocalizedString(),
                BackgroundBuffType.Click40 => Click40.GetLocalizedString(),
                BackgroundBuffType.Click50 => Click50.GetLocalizedString(),
                _ => "???"
            };
        }

        public static string ConvertSortToString(SortType sortType)
        {
            return sortType switch
            {
                SortType.Rarity => SortByRarity.GetLocalizedString(),
                SortType.Count => SortByCount.GetLocalizedString(),
                SortType.Availability => SortByHas.GetLocalizedString(),
                _ => "???"
            };
        }

        public static string ConvertRarityToString(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.Common => RareCommon.GetLocalizedString(),
                Rarity.Rare => RareRare.GetLocalizedString(),
                Rarity.SuperRare => RareSuperRare.GetLocalizedString(),
                Rarity.SuperMegaRare => RareSuperMegaRare.GetLocalizedString(),
                Rarity.Special => RareSpecial.GetLocalizedString(),
                _ => "???"
            };
        }
    }
}