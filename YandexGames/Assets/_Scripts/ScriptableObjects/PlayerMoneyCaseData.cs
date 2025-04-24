using System;
using System.Linq;
using _Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    public class PlayerMoneyCaseOpenedData
    {
        [SerializeField] private string caseObjectLocation;
        public string CaseObjectLocation => caseObjectLocation;

        public string Id => ToCaseData().Id;
        public int OpenedCount;

        private const float PriceMultiplierPerOpen   = 0.15f;
        private const float TierStepFractionPerTier  = 0.1f;

        [Inject] private TransactionController _transactionController;

        public float TotalPrice
        {
            get
            {
                var baseData = ToCaseData();
                float price = baseData.Price;

                for (int i = 0; i < OpenedCount; i++)
                    price *= 1f + PriceMultiplierPerOpen;

                foreach (var other in _transactionController.PlayerMoneyCases
                                .Where(d => (int)d.ToCaseData().Tier > (int)baseData.Tier))
                {
                    int diffTiers = (int)other.ToCaseData().Tier - (int)baseData.Tier;

                    float stepFraction = diffTiers * TierStepFractionPerTier;

                    for (int k = 0; k < other.OpenedCount; k++)
                    {
                        price *= 1f + stepFraction;
                    }
                }

                return price;
            }
        }

        public PlayerMoneyCaseOpenedData(MoneyCaseData caseData)
        {
            OpenedCount        = 0;
            caseObjectLocation = caseData.name;
        }

        private MoneyCaseData ToCaseData()
            => Resources.Load<MoneyCaseData>($"Cases/MoneyCases/{caseObjectLocation}");

        public override string ToString()
            => $"OpenedCount: {OpenedCount}, Location: {caseObjectLocation}";
    }
}
