using System.Collections.Generic;
using System.Linq;
using _Scripts.BuffLogic;
using _Scripts.Data;
using _Scripts.Data.Backgrounds;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.EventsLogic;
using _Scripts.EventsLogic.Events;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace _Scripts.Controllers
{
    public class TransactionController : ISavedProgress
    {
        private BuffController _buffController;

        private GameFactory _gameFactory;

        public float Money { get; private set; }

        public List<PlayerCardData> PlayerCards { get; private set; } = new();

        public List<CardData> AllCards { get; private set; } = new();

        public List<BedData> PlayerBeds { get; private set; } = new();

        public List<PlayerMoneyCaseOpenedData> PlayerMoneyCases { get; private set; } = new();
        public List<PlayerLimitedCaseOpenedData> PlayerLimitedCases { get; private set; } = new();

        public List<BedData> AllBeds { get; private set; } = new();

        public PlayerCardData SelectedCard { get; private set; }

        public BedData SelectedBed { get; private set; }

        public BackgroundData SelectedBackground { get; private set; }

        public List<BackgroundData> PlayerBackgrounds { get; private set; } = new();

        public List<BackgroundData> AllBackgrounds { get; private set; } = new();

        public int AdCounter { get; private set; }

        public int CaseCounter { get; private set; }

        public bool IsMusicOn { get; private set; }
        public bool IsSoundOn { get; private set; }
        public bool IsFirstGameStarted { get; private set; }

        public CaseTier CurrentCaseTier { get; private set; }

        public void LoadProgress(PlayerProgress progress)
        {
            Money = progress.LevelsProgress.Money;
            PlayerCards = progress.LevelsProgress.PlayerCards;
            AllCards = progress.LevelsProgress.AllCardsSet;
            SelectedCard = progress.LevelsProgress.SelectedCard;

            PlayerBeds = progress.LevelsProgress.PlayerBeds;
            AllBeds = progress.LevelsProgress.AllBedsSet;
            SelectedBed = progress.LevelsProgress.SelectedBed;

            PlayerBackgrounds = progress.LevelsProgress.PlayerBackgrounds;
            AllBackgrounds = progress.LevelsProgress.AllBackgroundsSet;
            SelectedBackground = progress.LevelsProgress.SelectedBackground;

            PlayerMoneyCases = progress.LevelsProgress.PlayerMoneyCases;
            PlayerLimitedCases = progress.LevelsProgress.PlayerLimitedCases;

            CurrentCaseTier = progress.LevelsProgress.CurrentCaseTier;

            CheckForNewCards(Resources.LoadAll<CardObject>("Cards").ToList());
            CheckForNewBeds(Resources.LoadAll<BedObject>("Beds").ToList());
            CheckForNewBackgrounds(Resources.LoadAll<BackgroundObject>("Backgrounds").ToList());

            AdCounter = progress.LevelsProgress.TotalAdsWatched;
            CaseCounter = progress.LevelsProgress.TotalCasesOpened;

            IsMusicOn = progress.LevelsProgress.IsMusicOn;
            IsSoundOn = progress.LevelsProgress.IsSoundOn;
            IsFirstGameStarted = progress.LevelsProgress.IsFirstGameStarted;

            EventBus<OnMusicSettingsChanged>.Raise(new OnMusicSettingsChanged(IsMusicOn));
            EventBus<OnSoundSettingsChanged>.Raise(new OnSoundSettingsChanged(IsSoundOn));
            EventBus<OnTransactionsLoadedEvent>.Raise(new OnTransactionsLoadedEvent());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LevelsProgress.Money = Money;
            progress.LevelsProgress.PlayerCards = PlayerCards;
            progress.LevelsProgress.AllCardsSet = AllCards;
            progress.LevelsProgress.SelectedCard = SelectedCard;

            progress.LevelsProgress.PlayerBeds = PlayerBeds;
            progress.LevelsProgress.AllBedsSet = AllBeds;
            progress.LevelsProgress.SelectedBed = SelectedBed;

            progress.LevelsProgress.PlayerBackgrounds = PlayerBackgrounds;
            progress.LevelsProgress.AllBackgroundsSet = AllBackgrounds;
            progress.LevelsProgress.SelectedBackground = SelectedBackground;

            progress.LevelsProgress.PlayerMoneyCases = PlayerMoneyCases;
            progress.LevelsProgress.PlayerLimitedCases = PlayerLimitedCases;

            progress.LevelsProgress.TotalAdsWatched = AdCounter;
            progress.LevelsProgress.TotalCasesOpened = CaseCounter;

            progress.LevelsProgress.IsMusicOn = IsMusicOn;
            progress.LevelsProgress.IsSoundOn = IsSoundOn;
            progress.LevelsProgress.IsFirstGameStarted = IsFirstGameStarted;

            progress.LevelsProgress.CurrentCaseTier = CurrentCaseTier;
        }

        [Inject]
        public void Construct(GameFactory gameFactory, BuffController buffController)
        {
            _gameFactory = gameFactory;
            _buffController = buffController;

            _gameFactory.Register(this);

            _buffController.Initialize(new BuffStats());
        }

        public void AddMoney(float amount)
        {
            Money += amount;
            EventBus<OnMoneyChangedEvent>.Raise(new OnMoneyChangedEvent());
        }

        public bool SpendMoney(float amount)
        {
            if (Money < amount) return false;

            Money -= amount;
            return true;
        }

        public void AddPlayerCards(List<CardData> cards)
        {
            if (PlayerCards == null) PlayerCards = new List<PlayerCardData>();

            foreach (var card in cards)
            {
                var isCardExist = false;

                foreach (var playerCard in PlayerCards)
                    if (playerCard.Id == card.Id)
                    {
                        playerCard.Count++;
                        isCardExist = true;

                        if (!card.IsOpen) OpenCard(card);

                        break;
                    }

                if (!isCardExist)
                {
                    PlayerCards.Add(new PlayerCardData(card));
                    OpenCard(card);
                }
            }

            // Debug.Log("Player cards: ");
            // foreach (var playerCard in PlayerCards)
            // {
            //     if (SelectedCard != null && playerCard.Id == SelectedCard.Id) SelectedCard = playerCard;
            //
            //     Debug.Log(playerCard.Name + " " + playerCard.Count);
            // }
        }

        public void AddMoneyCase(MoneyCaseData moneyCase)
        {
            if (PlayerMoneyCases == null) PlayerMoneyCases = new List<PlayerMoneyCaseOpenedData>();

            var isCaseExist = false;

            foreach (var playerCase in PlayerMoneyCases)
                if (playerCase.Id == moneyCase.Id)
                {
                    playerCase.OpenedCount++;
                    isCaseExist = true;
                    break;
                }

            if (!isCaseExist)
            {
                var pc = new PlayerMoneyCaseOpenedData(moneyCase)
                {
                    OpenedCount = 1
                };
                PlayerMoneyCases.Add(pc);
            }

            // Debug.Log("Player cases: ");
            // foreach (var playerCase in PlayerMoneyCases)
            // {
            //     Debug.Log(playerCase.Id + " " + playerCase.OpenedCount);
            // }
        }

        public void AddLimitedCase(LimitedCaseData limitedCase)
        {
            if (PlayerLimitedCases == null) PlayerLimitedCases = new List<PlayerLimitedCaseOpenedData>();

            var isCaseExist = false;

            foreach (var playerCase in PlayerLimitedCases)
                if (playerCase.Id == limitedCase.Id)
                {
                    playerCase.OpenedCount++;
                    isCaseExist = true;
                    break;
                }

            if (!isCaseExist)
            {
                var pc = new PlayerLimitedCaseOpenedData(limitedCase)
                {
                    OpenedCount = 1
                };
                PlayerLimitedCases.Add(pc);
            }

            Debug.Log("Player cases: ");
            foreach (var playerCase in PlayerLimitedCases)
            {
                Debug.Log(playerCase.Id + " " + playerCase.OpenedCount);
            }
        }

        private void OpenCard(CardData card)
        {
            card.IsOpen = true;
        }

        public int GetPlayerCardsCount()
        {
            var count = 0;

            foreach (var playerCard in PlayerCards) count += playerCard.Count;

            return count;
        }

        public void ChooseSelectedCard(PlayerCardData card)
        {
            SelectedCard = card;
        }

        public void ChooseSelectedBed(BedData bed)
        {
            SelectedBed = bed;
        }

        public void ChooseSelectedBackground(BackgroundData background)
        {
            SelectedBackground = background;
        }

        public void CheckForNewCards(List<CardObject> cardPool)
        {
            if (AllCards == null) AllCards = new List<CardData>();

            foreach (var card in cardPool)
            {
                var isNewCard = !AllCards.Exists(c => c.Id == card.Id);

                if (isNewCard)
                {
                    AllCards.Add(new CardData(card));
                    Debug.Log($"New card added: {card.Name}");
                }
                else
                {
                    foreach (var playerCard in PlayerCards)
                        if (playerCard.Id == card.Id)
                            AllCards.Find(c => c.Id == card.Id).IsOpen = true;
                }
            }
        }

        private void CheckForNewBeds(List<BedObject> bedPool)
        {
            if (AllBeds == null) AllBeds = new List<BedData>();

            foreach (var bed in bedPool)
            {
                var isNewBed = !AllBeds.Exists(b => b.Id == bed.Id);

                if (isNewBed)
                {
                    AllBeds.Add(new BedData(bed));
                    Debug.Log($"New bed added: {bed.Name}");
                }
                else
                {
                    foreach (var playerBed in PlayerBeds)
                        if (playerBed.Id == bed.Id)
                            AllBeds.Find(b => b.Id == bed.Id).IsOpen = true;
                }
            }
        }

        private void CheckForNewBackgrounds(List<BackgroundObject> backgroundPool)
        {
            if (AllBackgrounds == null) AllBackgrounds = new List<BackgroundData>();

            foreach (var background in backgroundPool)
            {
                var isNewBackground = !AllBackgrounds.Exists(b => b.Id == background.Id);

                if (isNewBackground)
                {
                    AllBackgrounds.Add(new BackgroundData(background));
                    Debug.Log($"New background added: {background.Name}");
                }
                else
                {
                    foreach (var playerBackground in PlayerBackgrounds)
                        if (playerBackground.Id == background.Id)
                            AllBackgrounds.Find(b => b.Id == background.Id).IsOpen = true;
                }
            }
        }

        public void AddAdCounter(int count = 1)
        {
            AdCounter += count;
        }

        public void AddCaseCounter(int count = 1)
        {
            CaseCounter += count;
        }

        public void SetMusic(bool isOn)
        {
            IsMusicOn = isOn;
        }

        public void SetSound(bool isOn)
        {
            IsSoundOn = isOn;
        }

        public void SetCaseTier(CaseTier caseTier)
        {
            CurrentCaseTier = caseTier;
        }

        public void SetFirstGameStarted(bool isFirstGameStarted)
        {
            IsFirstGameStarted = isFirstGameStarted;
        }

        public bool IsCardClosed(CardObject cardData)
        {
            if (PlayerCards == null) return true;

            foreach (var playerCard in PlayerCards)
                if (playerCard.Id == cardData.Id)
                    return !playerCard.IsOpen;

            return true;
        }
    }
}