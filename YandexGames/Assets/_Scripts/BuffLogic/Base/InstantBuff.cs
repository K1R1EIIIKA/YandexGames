namespace _Scripts.BuffLogic.Base
{
    public class InstantBuff : IBuff
    {
        private readonly IBuffable _owner;
        private readonly IBuff _coreBuff;

        public InstantBuff(IBuffable owner, IBuff coreBuff)
        {
            _owner = owner;
            _coreBuff = coreBuff;
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var stats = _coreBuff.ApplyBuff(baseStats);

            MonoTimer timer = MonoTimer.CreateInstance;
            timer.OnCompleted += () => _owner.RemoveBuff(this);
            timer.StartTimer(0.5f);

            return stats;
        }
    }
}