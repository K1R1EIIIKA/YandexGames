using _Scripts.ScriptableObjects;
using Zenject;

namespace _Scripts.Controllers
{
    public class CaseController
    {
        [Inject] private TransactionController _transactionController;

        public float GetTotalMoneyPrice(MoneyCaseData moneyCaseData)
        {
            foreach (var caseData in _transactionController.PlayerMoneyCases)
            {
                if (caseData.Id == moneyCaseData.Id)
                {
                    return caseData.TotalPrice;
                }
            }

            return moneyCaseData.Price;
        }

        public float GetTotalLimitedPrice(LimitedCaseData limitedCaseData)
        {
            foreach (var caseData in _transactionController.PlayerLimitedCases)
            {
                if (caseData.Id == limitedCaseData.Id)
                {
                    return caseData.TotalPrice;
                }
            }

            return limitedCaseData.Price;
        }
    }
}