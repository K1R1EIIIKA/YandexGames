using System;

namespace _Scripts.BuffLogic
{
    [Serializable]
    public class BuffStats
    {
        public float ClickBonus = 1;
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