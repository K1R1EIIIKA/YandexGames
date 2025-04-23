using _Scripts.BuffLogic.Base;

namespace _Scripts.BuffLogic.Buffs
{
    public class AutoClickBuff : IIdentifiedBuff
    {
        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var newStats = baseStats;
            newStats.IsAutoClick = true;

            return newStats;
        }

        public string Id => "autoclick";
    }
}