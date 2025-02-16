using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class RareToColorExtension
    {
        public static string ToHexColor(this Rare rare)
        {
            switch (rare)
            {
                case Rare.Common:
                    return "#97A7CA";
                case Rare.Uncommon:
                    return "#293B85";
                case Rare.Epic:
                    return "#8F104E";
                case Rare.Legendary:
                    return "#CAD714";
                case Rare.Advertisement:
                    return "#1A8E33";
                default:
                    Debug.LogWarning("RareToColorExtension::ToColor: Unknown Rare");
                    return "#FFFFFF";
            }
        }
    }
}