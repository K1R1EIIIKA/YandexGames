using _Scripts.BuffLogic.Base;
using UnityEngine;

namespace _Scripts.BuffLogic.Buffs
{
    public class MoneyClickBuff : IIdentifiedBuff
    {
        private readonly float _multiplier;

        public MoneyClickBuff(float multiplier)
        {
            _multiplier = multiplier;
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var newStats = baseStats;
            newStats.ClickBonus = Mathf.Max(newStats.ClickBonus * _multiplier, 0);

            return newStats;
        }

        public string Id => "click";
    }
}