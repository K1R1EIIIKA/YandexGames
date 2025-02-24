using _Scripts.Data.Cases;
using UnityEngine;

namespace _Scripts.Infrastructure.Inventory
{
    [CreateAssetMenu(fileName = "CaseRepository", menuName = "ScriptableObjects/Case Repository")]
    public class CaseRepository : ScriptableObject
    {
        [SerializeField] private CaseData[] _mainScreenCases;
        [SerializeField] private CaseData[] _shopCases;

        public CaseData[] MainScreenCases => _mainScreenCases;
        public CaseData[] ShopCases => _shopCases;
    }
}