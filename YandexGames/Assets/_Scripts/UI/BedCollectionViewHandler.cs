using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class BedCollectionViewHandler : MonoBehaviour
    {
        [Inject] BedsController _bedsController;

        [SerializeField] GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _bedsController.Construct(_gridLayoutGroup);
        }

        private void OnEnable()
        {
            _bedsController.ShowPlayerBeds();
        }
    }
}