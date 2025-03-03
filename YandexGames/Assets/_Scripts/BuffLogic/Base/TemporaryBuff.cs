using DG.Tweening;

namespace _Scripts.BuffLogic.Buffs
{
    public class TemporaryBuff : IBuff
    {
        private readonly IBuffable _owner;
        private readonly IBuff _coreBuff;
        private readonly float _duration;
        private readonly MonoTimer _timer;

        public TemporaryBuff(IBuffable owner, IBuff coreBuff, float duration)
        {
            _owner = owner;
            _coreBuff = coreBuff;
            _duration = duration;

            _timer = MonoTimer.CreateInstance;
            _timer.OnCompleted += RemoveSelf;
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            var newStats = _coreBuff.ApplyBuff(baseStats);

            _timer.StartTimer(_duration);


            return newStats;
        }

        private void RemoveSelf()
        {
            _owner.RemoveBuff(this);
            _timer.OnCompleted -= RemoveSelf;
        }
    }
}