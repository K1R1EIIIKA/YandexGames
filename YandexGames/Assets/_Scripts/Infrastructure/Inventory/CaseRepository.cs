using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Infrastructure.Inventory
{
    [CreateAssetMenu(fileName = "CaseRepository", menuName = "ScriptableObjects/Case Repository")]
    public class CaseRepository : ScriptableObject
    {
        [SerializeField] private CaseData[] _mainScreenCases;
        [SerializeField] private CaseData[] _shopCases;
        [SerializeField] private CaseData[] _specialCases;
        [SerializeField] private CaseData[] _adCases;
        [SerializeField] private CaseData _limitedCase;

        public CaseData[] MainScreenCases => _mainScreenCases;
        public CaseData[] ShopCases => _shopCases;
        public CaseData[] SpecialCases => _specialCases;
        public CaseData[] AdCases => _adCases;
        public CaseData LimitedCase => _limitedCase;
    }
}