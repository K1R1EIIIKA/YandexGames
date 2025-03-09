using System.Collections.Generic;
using _Scripts.BuffLogic;
using _Scripts.BuffLogic.Buffs;
using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.ScriptableObjects;
using _Scripts.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class BackgroundsController : ISavedProgress, IInitializable
    {
        private List<BackgroundData> _allBackgroundsSet = new();
        private List<BackgroundData> _playerBackgroundsData = new();

        private List<GameObject> _backgrounds = new();

        private IGameFactory _gameFactory;
        private TransactionController _transactionController;
        private readonly BackgroundBigView _backgroundBigView;
        private BuffController _buffController;

        private GridLayoutGroup _gridLayout;

        [Inject]
        public BackgroundsController(ISaveLoadService saveLoadService, IGameFactory gameFactory,
            TransactionController transactionController, BuffController buffController,
            BackgroundBigView bedBigView)
        {
            _gameFactory = gameFactory;
            _transactionController = transactionController;
            _buffController = buffController;
            _backgroundBigView = bedBigView;

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

        public void ShowPlayerBackgrounds()
        {
            ClearBackgrounds();
            CreatePlayerBackgroundsView();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // progress.LevelsProgress.PlayerCards = _playerCardsData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerBackgroundsData = progress.LevelsProgress.PlayerBackgrounds;
            _allBackgroundsSet = progress.LevelsProgress.AllBackgroundsSet;

            ApplyBackgroundBuff(_transactionController.SelectedBackground);
        }

        private void CreatePlayerBackgroundsView()
        {
            foreach (BackgroundData backgroundData in _allBackgroundsSet)
            {
                bool isOpen = false;
                foreach (var data in _playerBackgroundsData)
                {
                    if (data.Id == backgroundData.Id)
                    {
                        isOpen = true;
                    }
                }

                GameObject card = CreateBackgroundView(backgroundData);
                _backgrounds.Add(card);
                if (isOpen)
                {
                    card.GetComponent<Button>().onClick.AddListener(() => _backgroundBigView.OpenBoughtBed(backgroundData));
                }
                else
                {
                    card.GetComponent<BackgroundView>().SetViewToClosed();
                    card.GetComponent<Button>().onClick.AddListener(() => _backgroundBigView.OpenUnbougthBed(backgroundData));
                }
            }
        }

        public bool TryBuyBackground(BackgroundData backgroundData)
        {
            if (_transactionController.SpendMoney(backgroundData.ToBackgroundObject().Price))
            {
                backgroundData.IsOpen = true;
                _playerBackgroundsData.Add(backgroundData);
                ShowPlayerBackgrounds();

                return true;
            }

            return false;
        }

        public void OnBackgroundClick(BackgroundData backgroundData)
        {
            _transactionController.ChooseSelectedBackground(backgroundData);
            ApplyBackgroundBuff(backgroundData);
        }

        private void ApplyBackgroundBuff(BackgroundData background)
        {
            if (background == null)
            {
                return;
            }

            _buffController.RemoveBuffByType<MoneyClickBuff>();

            switch (background.BuffType)
            {
                case BackgroundBuffType.Click10:
                    _buffController.AddBuff(new MoneyClickBuff(1.1f));
                    break;
                case BackgroundBuffType.Click20:
                    _buffController.AddBuff(new MoneyClickBuff(1.2f));
                    break;
                case BackgroundBuffType.Click30:
                    _buffController.AddBuff(new MoneyClickBuff(1.3f));
                    break;
                case BackgroundBuffType.Click40:
                    _buffController.AddBuff(new MoneyClickBuff(1.4f));
                    break;
                case BackgroundBuffType.Click50:
                    _buffController.AddBuff(new MoneyClickBuff(1.5f));
                    break;
            }
        }

        private GameObject CreateBackgroundView(BackgroundData cardData)
        {
            GameObject card = _gameFactory.CreateObjectBackground(_gridLayout);
            BackgroundView backgroundView = card.GetComponent<BackgroundView>();

            backgroundView.Initialize(cardData);

            if (!cardData.IsOpen)
                backgroundView.SetViewToClosed();

            return card;
        }

        private void ClearBackgrounds()
        {
            if (_backgrounds == null) return;

            foreach (GameObject card in _backgrounds)
            {
                Object.Destroy(card);
            }

            _backgrounds.Clear();
        }

        public void SortBackgrounds(CollectionSortType sortType)
        {
            switch (sortType)
            {
                case CollectionSortType.ByRareAsc:
                    _allBackgroundsSet.Sort((a, b) => a.Rarity.CompareTo(b.Rarity));
                    break;
                case CollectionSortType.ByRareDesc:
                    _allBackgroundsSet.Sort((a, b) => b.Rarity.CompareTo(a.Rarity));
                    break;
                case CollectionSortType.ByHasAsc:
                    _allBackgroundsSet.Sort((a, b) => a.IsOpen.CompareTo(b.IsOpen));
                    break;
                case CollectionSortType.ByHasDesc:
                    _allBackgroundsSet.Sort((a, b) => b.IsOpen.CompareTo(a.IsOpen));
                    break;
            }

            ShowPlayerBackgrounds();
        }
    }
}