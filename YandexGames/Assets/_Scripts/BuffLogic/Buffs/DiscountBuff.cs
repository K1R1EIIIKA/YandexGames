using _Scripts.BuffLogic.Base;
using UnityEngine;

namespace _Scripts.BuffLogic.Buffs
{
    public class DiscountBuff : IIdentifiedBuff
    {
        private readonly int _discount;

        public DiscountBuff(int discount)
        {
            _discount = discount;
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var newStats = baseStats;
            newStats.DiscountBonus = Mathf.Max(newStats.DiscountBonus + _discount, 0);

            return newStats;
        }

        public string Id => "discount";
    }
}