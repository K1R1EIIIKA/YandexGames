using _Scripts.BuffLogic;
using _Scripts.BuffLogic.Base;
using _Scripts.BuffLogic.Buffs;
using _Scripts.Data;
using _Scripts.Data.Cards;
using _Scripts.EventsLogic;
using _Scripts.EventsLogic.Events;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Tools;
using _Scripts.YG;
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

        [SerializeField] private Transform _characterTransform;

        private TransactionController _transactionController;
        private GameFactory _gameFactory;
        private BuffController _buffController;

        private PlayerCardData _selectedCard;

        private int _moneyGain = 1;
        private float _autoclicksInterval = 0.1f;
        private float _autoclicksTime = 0;

        [Inject] private AdBuffController _adBuffController;
        [Inject] private AdRewardController _adRewardController;

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

            _moneyText.text = BigNumberFormatter.FormatBigNumber(_transactionController.Money);
            Debug.Log(_characterImage);

            _adRewardController.BigCharacterAdId += AddBigCharacterBuff;
            _adRewardController.SmallCharacterAdId += AddSmallCharacterBuff;
            _adRewardController.CrazyCharacterAdId += AddCrazyCharacterBuff;

            EventBus<OnMoneyChangedEvent>.OnEvent += UpdateMoneyText;
        }

        private void AddBigCharacterBuff(float value)
        {
            _buffController.AddBuff(new TemporaryBuff(_buffController, new CharacterSizeBuff(1.5f, false), 10 * value));
        }

        private void AddSmallCharacterBuff(float value)
        {
            _buffController.AddBuff(new TemporaryBuff(_buffController, new CharacterSizeBuff(0.5f, false), 10 * value));
        }

        private void AddCrazyCharacterBuff(float value)
        {
            _buffController.AddBuff(new TemporaryBuff(_buffController, new CharacterSizeBuff(1f, true), 10 * value));
        }

        private void OnDisable()
        {
            _characterButton.onClick.RemoveListener(OnCharacterClick);

            _adRewardController.BigCharacterAdId -= AddBigCharacterBuff;
            _adRewardController.SmallCharacterAdId -= AddSmallCharacterBuff;
            _adRewardController.CrazyCharacterAdId -= AddCrazyCharacterBuff;
        }

        private void OnCharacterClick()
        {
            var money = Mathf.Round(_selectedCard.TotalMoneyPerClick * _buffController.CurrentStats.ClickBonus);
            _transactionController.AddMoney(money);
        }

        private void UpdateMoneyText(OnMoneyChangedEvent e)
        {
            _moneyText.text = BigNumberFormatter.FormatBigNumber(_transactionController.Money);
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

                    var money = Mathf.Round(_selectedCard.TotalMoneyPerClick *
                                                 _buffController.CurrentStats.ClickBonus);

                    _transactionController.AddMoney(money);
                    _moneyText.text = BigNumberFormatter.FormatBigNumber(_transactionController.Money);
                }
            }

            if (_buffController.CurrentStats.IsCharacterSizeChanged)
            {
                _characterTransform.localScale = new Vector3(_buffController.CurrentStats.CharacterScale,
                    _buffController.CurrentStats.CharacterScale, 1);
            }
            else
            {
                _characterTransform.localScale = Vector3.one;
            }

            if (_buffController.CurrentStats.IsCharacterSizeCrazy)
            {
                _characterTransform.Rotate(Vector3.forward, 4);
            }
            else
            {
                _characterTransform.rotation = Quaternion.identity;
            }
        }
    }
}