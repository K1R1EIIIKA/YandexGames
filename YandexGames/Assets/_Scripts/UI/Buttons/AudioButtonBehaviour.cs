using _Scripts.Controllers;
using _Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons
{
    public class AudioButtonBehaviour : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            AudioController.Instance.PlaySound(SoundName.ButtonClick);
        }
    }
}