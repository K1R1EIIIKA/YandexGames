using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons
{
    public class SortTypeButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RectTransform _iconTransform;
        [SerializeField] private Button _button;

        public Button Button => _button;

        public void ChangeSortType(string text, bool isAscending)
        {
            _text.text = text;
            _iconTransform.localRotation = !isAscending ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
        }
    }
}