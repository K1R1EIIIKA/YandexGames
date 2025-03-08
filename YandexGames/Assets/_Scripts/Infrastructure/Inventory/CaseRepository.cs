using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Infrastructure.Inventory
{
    [CreateAssetMenu(fileName = "CaseRepository", menuName = "ScriptableObjects/Case Repository")]
    public class CaseRepository : ScriptableObject
    {
        [SerializeField] private CaseData[] _mainScreenCases;
        [SerializeField] private CaseData[] _shopCases;
        [SerializeField] private CaseData[] _adCases;

        public CaseData[] MainScreenCases => _mainScreenCases;
        public CaseData[] ShopCases => _shopCases;
        public CaseData[] AdCases => _adCases;
    }
}