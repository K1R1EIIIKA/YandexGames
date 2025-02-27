using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuCharacterController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _characterButton;
        [SerializeField] private Image _characterImage;

        private TransactionController _transactionController;

        private int _moneyGain = 1;

        [Inject]
        public void Construct(TransactionController transactionController)
        {
            _transactionController = transactionController;
        }

        private void OnEnable()
        {
            _characterButton.onClick.AddListener(OnCharacterClick);

            _moneyText.text = _transactionController.Money.ToString();
        }

        private void OnDisable()
        {
            _characterButton.onClick.RemoveListener(OnCharacterClick);
        }

        private void OnCharacterClick()
        {
            _transactionController.AddMoney(_moneyGain);
            _moneyText.text = _transactionController.Money.ToString();
        }
    }
}