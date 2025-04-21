using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Controllers
{
    public class NewGameController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(HideNewGame);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HideNewGame);
        }

        private void HideNewGame()
        {
            gameObject.SetActive(false);
        }

        public void ShowNewGame()
        {
            gameObject.SetActive(true);
        }
    }
}