using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class RareToColorExtension
    {
        public static string ToHexColor(this Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return "#C7CFE1";
                case Rarity.Rare:
                    return "#1969BF";
                case Rarity.SuperRare:
                    return "#D51A14";
                case Rarity.SuperMegaRare:
                    return "#FFC300";
                case Rarity.Special:
                    return "#9A04EA";
                default:
                    Debug.LogWarning("RareToColorExtension::ToColor: Unknown Rare");
                    return "#FFFFFF";
            }
        }
    }
}