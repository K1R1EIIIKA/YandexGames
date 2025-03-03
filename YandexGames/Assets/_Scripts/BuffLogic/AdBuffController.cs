using _Scripts.BuffLogic.Buffs;
using _Scripts.YG;
using Zenject;

namespace _Scripts.BuffLogic
{
    public class AdBuffController
    {
        private AdRewardController _adRewardController;
        private BuffController _buffController;

        [Inject]
        public void Construct(AdRewardController adRewardController, BuffController buffController)
        {
            _adRewardController = adRewardController;
            _buffController = buffController;

            Initialize();
        }

        private void Initialize()
        {
            _adRewardController.Discount10AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(10), 10f));
            _adRewardController.Discount20AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(20), 10f));
            _adRewardController.Discount50AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(50), 10f));

            _adRewardController.OneAndHalfMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(1.5f),10f));
            _adRewardController.DoubleMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(2f), 10f));
            _adRewardController.TripleMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(3f), 10f));
        }
    }
}