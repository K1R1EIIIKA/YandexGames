using _Scripts.Controllers;

namespace _Scripts.BuffLogic.Buffs
{
    public class InstantMoneyBuff : IBuff
    {
        private readonly int _multiplier;
        private readonly TransactionController _transactionController;

        public InstantMoneyBuff(TransactionController transactionController, int multiplier)
        {
            _multiplier = multiplier;
            _transactionController = transactionController;
        }

        public BuffStats ApplyBuff(BuffStats baseStats)
        {
            _transactionController.AddMoney(_transactionController.SelectedCard.TotalMoneyPerClick * _multiplier);

            return baseStats;
        }
    }
}