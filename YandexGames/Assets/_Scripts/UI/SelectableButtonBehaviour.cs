using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SelectableButtonBehaviour : MonoBehaviour
    {
        [Header("Compontnts")]
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Deselected")]
        [SerializeField] private Sprite _deselectedImage;
        [SerializeField] private Color _deselectedTextColor;

        [Header("Selected")]
        [SerializeField] private Sprite _selectedImage;
        [SerializeField] private Color _selectedTextColor;

        [SerializeField] private Button _button;
        public Button Button => _button;

        public void Select()
        {
            _image.sprite = _selectedImage;
            _text.color = _selectedTextColor;
        }

        public void Deselect()
        {
            _image.sprite = _deselectedImage;
            _text.color = _deselectedTextColor;
        }
    }
}