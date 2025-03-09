using System.Linq;
using _Scripts.Controllers;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Infrastructure.Core.States;
using _Scripts.ScriptableObjects;
using _Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.View
{
    public class BackgroundBigView : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private GameObject _playerMoneyObject;
        [SerializeField] private GameObject _backgroundPriceObject;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _playerMoneyText;
        [SerializeField] private TextMeshProUGUI _bonusText;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _placeButton;
        [SerializeField] private Button _buyButton;

        [Header("Images")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _rarityBackgroundImage;
        [SerializeField] private Material _specialMaterial;

        [Inject] private TransactionController _transactionController;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private LazyInject<BackgroundsController> _backgroundsController;

        private BackgroundData _backgroundData;

        public void OpenBoughtBed(BackgroundData backgroundData)
        {
            _backgroundData = backgroundData;

            var nameWithLine = backgroundData.GetName().Split(' ').Aggregate("", (current, next) => current + next + "\n");
            _nameText.text = nameWithLine;
            _descriptionText.text = backgroundData.GetDescription();
            _bonusText.text = LocalizedStrings.ConvertBackgroundBuffToString(backgroundData.BuffType);

            _buyButton.gameObject.SetActive(false);
            _placeButton.gameObject.SetActive(true);
            _playerMoneyObject.SetActive(false);
            _backgroundPriceObject.SetActive(false);

            if (backgroundData.Rarity == Rarity.Special)
            {
                _rarityBackgroundImage.color = Color.white;
                _rarityBackgroundImage.material = _specialMaterial;
            }
            else
            {
                _rarityBackgroundImage.material = null;
                _rarityBackgroundImage.color = backgroundData.Rarity.ToHexColor().ToColor();
            }

            _backgroundImage.sprite = backgroundData.ToBackgroundObject().BackgroundImage;
            _backgroundImage.color = Color.white;

            _placeButton.gameObject.SetActive(true);
            _placeButton.onClick.AddListener(OnPlaceButtonClick);

            gameObject.SetActive(true);
            GetComponent<LayoutUpdater>().UpdateAllLayouts();
        }

        public void OpenUnbougthBed(BackgroundData backgroundData)
        {
            _backgroundData = backgroundData;

            _nameText.text = "???";
            _descriptionText.text = backgroundData.GetDescription();
            _bonusText.text = LocalizedStrings.ConvertBackgroundBuffToString(backgroundData.BuffType);
            _playerMoneyText.text = _transactionController.Money.ToString();

            _buyButton.gameObject.SetActive(true);
            _placeButton.gameObject.SetActive(false);
            _playerMoneyObject.SetActive(true);
            _backgroundPriceObject.SetActive(true);

            if (backgroundData.Rarity == Rarity.Special)
            {
                _rarityBackgroundImage.color = Color.white;
                _rarityBackgroundImage.material = _specialMaterial;
            }
            else
            {
                _rarityBackgroundImage.material = null;
                _rarityBackgroundImage.color = backgroundData.Rarity.ToHexColor().ToColor();
            }

            _backgroundImage.sprite = backgroundData.ToBackgroundObject().BackgroundImage;
            _backgroundImage.color = Color.black;

            _priceText.text = backgroundData.ToBackgroundObject().Price.ToString();
            _buyButton.onClick.AddListener(OnBuyButtonClick);

            gameObject.SetActive(true);
            GetComponent<LayoutUpdater>().UpdateAllLayouts();
        }

        private void OnBuyButtonClick()
        {
            if (_backgroundsController.Value.TryBuyBackground(_backgroundData))
            {
                gameObject.SetActive(false);
                OpenBoughtBed(_backgroundData);
            }
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _placeButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
        }

        private void OnPlaceButtonClick()
        {
            _backgroundsController.Value.OnBackgroundClick(_backgroundData);
            gameObject.SetActive(false);
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.MainScreen);
        }
    }
}