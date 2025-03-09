using System;
using _Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        [SerializeField] private CheckButtonBehaviour _soundButton;
        [SerializeField] private CheckButtonBehaviour _musicButton;

        [Inject] private TransactionController _transactionController;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

            _soundButton.Button.onClick.AddListener(OnSoundButtonClicked);
            _musicButton.Button.onClick.AddListener(OnMusicButtonClicked);

            _musicButton.SetChecked(_transactionController.IsMusicOn);
            _soundButton.SetChecked(_transactionController.IsSoundOn);
        }

        private void OnMusicButtonClicked()
        {
            _musicButton.SetChecked(!_musicButton.IsChecked);

            _transactionController.SetMusic(_musicButton.IsChecked);
        }

        private void OnSoundButtonClicked()
        {
            _soundButton.SetChecked(!_soundButton.IsChecked);

            _transactionController.SetSound(_soundButton.IsChecked);
        }

        private void OnCloseButtonClicked()
        {
            CloseSettings();
        }

        public void OpenSettings()
        {
            gameObject.SetActive(true);
        }

        private void CloseSettings()
        {
            gameObject.SetActive(false);
        }
    }
}