using UnityEngine;

namespace _Scripts.BuffLogic.Buffs
{
    public class MoneyClickBuff : IBuff
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
    }
}