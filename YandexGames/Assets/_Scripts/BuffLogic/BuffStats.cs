using System;

namespace _Scripts.BuffLogic
{
    [Serializable]
    public class BuffStats
    {
        public int ClickBonus = 0;
        public int DiscountBonus = 0;
        public bool IsAutoClick = false;

        public BuffStats Copy()
        {
            return new BuffStats
            {
                ClickBonus = ClickBonus,
                DiscountBonus = DiscountBonus,
                IsAutoClick = IsAutoClick
            };
        }
    }
}