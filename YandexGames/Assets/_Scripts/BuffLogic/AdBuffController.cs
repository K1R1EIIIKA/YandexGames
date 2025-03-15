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
        private const int InstantMoneyBuffMultiplier = 100;

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
            _adRewardController.Discount10AdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(10), BuffDuration * value));
            _adRewardController.Discount20AdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(20), BuffDuration * value));
            _adRewardController.Discount50AdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new DiscountBuff(50), BuffDuration / 2 * value));

            _adRewardController.OneAndHalfMoneyAdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(1.5f), BuffDuration * value));
            _adRewardController.DoubleMoneyAdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(2f), BuffDuration * value));
            _adRewardController.TripleMoneyAdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new MoneyClickBuff(3f), BuffDuration / 2 * value));

            _adRewardController.AutoClickAdId += value => _buffController.AddBuff(new TemporaryBuff(_buffController, new AutoClickBuff(), BuffDuration * value));

            _adRewardController.AutoMoneyPerClickAdId += value => _buffController.AddBuff(new InstantBuff(_buffController, new InstantMoneyBuff(_transactionController, InstantMoneyBuffMultiplier * value)));
       }
    }
}