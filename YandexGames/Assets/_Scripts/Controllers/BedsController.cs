using System.Collections.Generic;
using _Scripts.BuffLogic;
using _Scripts.BuffLogic.Buffs;
using _Scripts.Data;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class BedsController : ISavedProgress, IInitializable
    {
        private List<BedData> _allBedsSet = new();
        private List<BedData> _playerBedsData = new();

        private List<GameObject> _beds = new();

        private IGameFactory _gameFactory;
        private TransactionController _transactionController;
        private readonly BedBigView _bedBigView;
        private BuffController _buffController;

        private GridLayoutGroup _gridLayout;

        private PlayerCardData _selectedCard;

        [Inject]
        public BedsController(ISaveLoadService saveLoadService, IGameFactory gameFactory,
            TransactionController transactionController, BuffController buffController,
            BedBigView bedBigView)
        {
            _gameFactory = gameFactory;
            _transactionController = transactionController;
            _buffController = buffController;
            _bedBigView = bedBigView;

            _gameFactory.Register(this);
            Debug.Log("Card Controller Initialized");
        }

        public ISavedProgress GetSavedProgress()
        {
            return this;
        }

        public void Construct(GridLayoutGroup gridLayout)
        {
            _gridLayout = gridLayout;
        }

        public void Initialize()
        {
        }

        public void ShowPlayerBeds()
        {
            ClearBeds();
            CreatePlayerBedsView();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // progress.LevelsProgress.PlayerCards = _playerCardsData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerBedsData = progress.LevelsProgress.PlayerBeds;
            _allBedsSet = progress.LevelsProgress.AllBedsSet;

            ApplyBedBuff(_transactionController.SelectedBed);
        }

        private void CreatePlayerBedsView()
        {
            _allBedsSet.Sort();

            foreach (BedData bedData in _allBedsSet)
            {
                bool isOpen = false;
                foreach (var data in _playerBedsData)
                {
                    if (data.Id == bedData.Id)
                    {
                        isOpen = true;
                    }
                }

                GameObject card = CreateBedView(bedData);
                _beds.Add(card);
                card.transform.SetParent(_gridLayout.transform);
                if (isOpen)
                {
                    card.GetComponent<Button>().onClick.AddListener(() => _bedBigView.OpenBoughtBed(bedData));
                }
                else
                {
                    card.GetComponent<BedView>().SetViewToClosed();
                    card.GetComponent<Button>().onClick.AddListener(() => _bedBigView.OpenUnbougthBed(bedData));
                }
            }
        }

        public bool TryBuyBed(BedData bedData)
        {
            if (_transactionController.SpendMoney(bedData.ToBedObject().Price))
            {
                bedData.IsOpen = true;
                _playerBedsData.Add(bedData);
                ShowPlayerBeds();

                return true;
            }

            return false;
        }

        public void OnBedClick(BedData bedData)
        {
            _transactionController.ChooseSelectedBed(bedData);
            ApplyBedBuff(bedData);
        }

        private void ApplyBedBuff(BedData bed)
        {
            if (bed == null)
            {
                return;
            }

            _buffController.RemoveBuffByType<DiscountBuff>();

            switch (bed.BuffType)
            {
                case BedBuffType.Discount5:
                    _buffController.AddBuff(new DiscountBuff(5));
                    break;
                case BedBuffType.Discount10:
                    _buffController.AddBuff(new DiscountBuff(10));
                    break;
                case BedBuffType.Discount15:
                    _buffController.AddBuff(new DiscountBuff(15));
                    break;
                case BedBuffType.Discount20:
                    _buffController.AddBuff(new DiscountBuff(20));
                    break;
                case BedBuffType.Discount25:
                    _buffController.AddBuff(new DiscountBuff(25));
                    break;
            }
        }

        private GameObject CreateBedView(BedData cardData)
        {
            GameObject card = _gameFactory.CreateObjectBed();
            BedView bedView = card.GetComponent<BedView>();

            bedView.Initialize(cardData);

            if (!cardData.IsOpen)
                bedView.SetViewToClosed();

            return card;
        }

        private void ClearBeds()
        {
            if (_beds == null) return;

            foreach (GameObject card in _beds)
            {
                Object.Destroy(card);
            }

            _beds.Clear();
        }
    }
}