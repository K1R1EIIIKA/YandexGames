using _Scripts.BuffLogic.Base;
using _Scripts.BuffLogic.Buffs;
using _Scripts.Controllers;
using _Scripts.YG;
using Zenject;

namespace _Scripts.BuffLogic
{
    public class AdBuffController
    {
        private AdRewardController _adRewardController;
        private BuffController _buffController;
        private TransactionController _transactionController;

        private const float BuffDuration = 10f;
        private const int InstantMoneyBuffMultiplier = 1000;

        [Inject]
        public void Construct(AdRewardController adRewardController, BuffController buffController,
            TransactionController transactionController)
        {
            _adRewardController = adRewardController;
            _buffController = buffController;
            _transactionController = transactionController;

            Initialize();
        }

        private void Initialize()
        {
            _adRewardController.Discount10AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(10), BuffDuration));
            _adRewardController.Discount20AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(20), BuffDuration));
            _adRewardController.Discount50AdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(50), BuffDuration));

            _adRewardController.OneAndHalfMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(1.5f),BuffDuration));
            _adRewardController.DoubleMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(2f), BuffDuration));
            _adRewardController.TripleMoneyAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(3f), BuffDuration));

            _adRewardController.AutoClickAdId += () => _buffController.AddBuff(new TemporaryBuff(_buffController, new AutoClickBuff(), BuffDuration));

            _adRewardController.AutoMoneyPerClickAdId += () => _buffController.AddBuff(new InstantBuff(_buffController, new InstantMoneyBuff(_transactionController, InstantMoneyBuffMultiplier)));
        }
    }
}