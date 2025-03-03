namespace _Scripts.BuffLogic.Buffs
{
    public class AutoClickBuff : IBuff
    {
        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var newStats = baseStats;
            newStats.IsAutoClick = true;

            return newStats;
        }
    }
}