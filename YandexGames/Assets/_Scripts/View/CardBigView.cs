using System.Linq;
using _Scripts.Controllers;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.View
{
    public class CardBigView : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _moneyGainText;
        [SerializeField] private TextMeshProUGUI _totalMoneyGainText;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _placeButton;

        [Header("Images")]
        [SerializeField] private Image _characterImage;
        [SerializeField] private Image _rarityBackgroundImage;
        [SerializeField] private Material _specialMaterial;

        [Inject] private TransactionController _transactionController;
        [Inject] private GameStateMachine _gameStateMachine;

        private PlayerCardData _cardData;

        public void OpenCard(PlayerCardData cardObject)
        {
            _cardData = cardObject;

            var nameWithLine = cardObject.GetName().Split(' ').Aggregate("", (current, next) => current + next + "\n");
            _nameText.text = nameWithLine;
            _descriptionText.text = cardObject.GetDescription();
            _countText.gameObject.SetActive(true);
            _countText.text = "x" + BigNumberFormatter.FormatBigNumber(cardObject.Count);
            _moneyGainText.text = LocalizedStrings.ClickPower.GetLocalizedString() + BigNumberFormatter.FormatBigNumber(cardObject.MoneyPerClick);
            _totalMoneyGainText.text = LocalizedStrings.GeneralClickPower.GetLocalizedString() + BigNumberFormatter.FormatBigNumber(cardObject.TotalMoneyPerClick);

            if (cardObject.Rarity == Rarity.Special)
            {
                _rarityBackgroundImage.color = Color.white;
                _rarityBackgroundImage.material = _specialMaterial;
            }
            else
            {
                _rarityBackgroundImage.material = null;
                _rarityBackgroundImage.color = cardObject.Rarity.ToHexColor().ToColor();
            }

            _characterImage.sprite = cardObject.ToCardObject().Image;
            _characterImage.color = Color.white;

            _placeButton.gameObject.SetActive(true);
            _placeButton.onClick.AddListener(OnPlaceButtonClick);

            gameObject.SetActive(true);
            GetComponent<LayoutUpdater>().UpdateAllLayouts();
        }

        public void OpenCard(CardData cardData)
        {
            _nameText.text = "???";
            _descriptionText.text = cardData.GetDescription();
            _countText.gameObject.SetActive(false);
            _moneyGainText.text = "???";
            _totalMoneyGainText.text = "???";

            if (cardData.Rarity == Rarity.Special)
            {
                _rarityBackgroundImage.color = Color.white;
                _rarityBackgroundImage.material = _specialMaterial;
            }
            else
            {
                _rarityBackgroundImage.material = null;
                _rarityBackgroundImage.color = cardData.Rarity.ToHexColor().ToColor();
            }

            _characterImage.sprite = cardData.ToCardObject().Image;
            _characterImage.color = Color.black;

            _placeButton.gameObject.SetActive(false);

            gameObject.SetActive(true);
            GetComponent<LayoutUpdater>().UpdateAllLayouts();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _placeButton.onClick.RemoveAllListeners();
        }

        private void OnPlaceButtonClick()
        {
            _transactionController.ChooseSelectedCard(_cardData);
            gameObject.SetActive(false);
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.MainScreen);
        }
    }
}