using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons
{
    public class CheckButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private Button _button;

        public Button Button => _button;

        public bool IsChecked { get; private set; }

        public void SetChecked(bool isChecked)
        {
            IsChecked = isChecked;
            _checkMark.SetActive(isChecked);
        }
    }
}