using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuCharacterController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _characterButton;
        [SerializeField] private Image _characterImage;

        private TransactionController _transactionController;
        private GameFactory _gameFactory;

        private PlayerCardData _selectedCard;

        private int _moneyGain = 1;

        [Inject]
        public void Construct(TransactionController transactionController, GameFactory gameFactory)
        {
            _transactionController = transactionController;
            _gameFactory = gameFactory;

            _gameFactory.Register(this);
        }

        private void OnEnable()
        {
            _characterButton.onClick.AddListener(OnCharacterClick);

            _moneyText.text = _transactionController.Money.ToString();
            Debug.Log(_characterImage);
        }

        private void OnDisable()
        {
            _characterButton.onClick.RemoveListener(OnCharacterClick);
        }

        private void OnCharacterClick()
        {
            _transactionController.AddMoney(_selectedCard.TotalMoneyPerClick);
            _moneyText.text = _transactionController.Money.ToString();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _selectedCard = progress.LevelsProgress.SelectedCard;

            if (_selectedCard != null && _characterImage != null)
            {
                _characterImage.sprite = _selectedCard.ToCardObject().Image;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // nope
        }
    }
}