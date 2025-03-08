using System;
using System.Net.NetworkInformation;

namespace _Scripts.BuffLogic
{
    [Serializable]
    public class BuffStats
    {
        public float ClickBonus = 1;
        public int DiscountBonus = 0;
        public bool IsAutoClick = false;
        public bool IsCharacterSizeChanged;
        public bool IsCharacterSizeCrazy;
        public float CharacterScale = 1;

        public BuffStats Copy()
        {
            return new BuffStats
            {
                ClickBonus = ClickBonus,
                DiscountBonus = DiscountBonus,
                IsAutoClick = IsAutoClick,
                IsCharacterSizeChanged = IsCharacterSizeChanged,
                IsCharacterSizeCrazy = IsCharacterSizeCrazy,
                CharacterScale = CharacterScale
            };
        }
    }
}