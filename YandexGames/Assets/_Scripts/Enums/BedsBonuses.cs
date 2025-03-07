using UnityEngine.Localization;

namespace _Scripts.Enums
{
    public static class BedsBonuses
    {
        private const string TableReference = "BuffsTable";

        private static readonly LocalizedString Discount5 = new() { TableReference = TableReference, TableEntryReference = "Discount5" };

        private static readonly LocalizedString Discount10 = new() { TableReference = TableReference, TableEntryReference = "Discount10" };

        private static readonly LocalizedString Discount15 = new() { TableReference = TableReference, TableEntryReference = "Discount15" };

        private static readonly LocalizedString Discount20 = new() { TableReference = TableReference, TableEntryReference = "Discount20" };

        private static readonly LocalizedString Discount25 = new() { TableReference = TableReference, TableEntryReference = "Discount25" };

        public static LocalizedString ConvertBuffToString(BedBuffType buffType)
        {
            return buffType switch
            {
                BedBuffType.Discount5 => Discount5,
                BedBuffType.Discount10 => Discount10,
                BedBuffType.Discount15 => Discount15,
                BedBuffType.Discount20 => Discount20,
                BedBuffType.Discount25 => Discount25,
                _ => new LocalizedString()
            };
        }
    }
}