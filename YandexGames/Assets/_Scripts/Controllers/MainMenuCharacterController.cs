using System;
using _Scripts.BuffLogic;
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
        private BuffController _buffController;

        private PlayerCardData _selectedCard;

        private int _moneyGain = 1;
        private float _autoclicksInterval = 0.1f;
        private float _autoclicksTime = 0;

        [Inject]
        public void Construct(TransactionController transactionController, GameFactory gameFactory,
            BuffController buffController)
        {
            _transactionController = transactionController;
            _gameFactory = gameFactory;
            _buffController = buffController;

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
            var money = Mathf.RoundToInt(_selectedCard.TotalMoneyPerClick * _buffController.CurrentStats.ClickBonus);

            _transactionController.AddMoney(money);
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

        private void Update()
        {
            if (_buffController.CurrentStats.IsAutoClick)
            {
                _autoclicksTime += Time.deltaTime;

                if (_autoclicksTime >= _autoclicksInterval)
                {
                    _autoclicksTime = 0;

                    var money = Mathf.RoundToInt(_selectedCard.TotalMoneyPerClick * _buffController.CurrentStats.ClickBonus);

                    _transactionController.AddMoney(money);
                    _moneyText.text = _transactionController.Money.ToString();
                }
            }
        }
    }
}