using System;
using System.Collections.Generic;
using _Scripts.Data.Backgrounds;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public OmNomData LevelsProgress;

        public PlayerProgress(string sceneName)
        {
            LevelsProgress = new OmNomData(sceneName);
        }

        public override string ToString()
        {
            return $"PlayerProgress: {LevelsProgress.SceneName}, Money: {LevelsProgress.Money}, TotalAdsWatched: {LevelsProgress.TotalAdsWatched}, TotalCasesOpened: {LevelsProgress.TotalCasesOpened}, PlayerMoneyCases: {string.Join(",", LevelsProgress.PlayerMoneyCases)}";
        }
    }

    [Serializable]
    public class OmNomData
    {
        public String SceneName;

        public List<PlayerCardData> PlayerCards;
        public PlayerCardData SelectedCard;
        public List<CardData> AllCardsSet;

        public List<PlayerMoneyCaseOpenedData> PlayerMoneyCases;

        public List<BedData> PlayerBeds;
        public BedData SelectedBed;
        public List<BedData> AllBedsSet;

        public List<BackgroundData> PlayerBackgrounds;
        public BackgroundData SelectedBackground;
        public List<BackgroundData> AllBackgroundsSet;

        public int TotalAdsWatched;
        public int TotalCasesOpened;

        public bool IsSoundOn;
        public bool IsMusicOn;

        public float Money;

        public OmNomData(string sceneName)
        {
            SceneName = sceneName;
            PlayerCards = new List<PlayerCardData>();

            var card = Resources.Load<CardObject>("Cards/classic");
            var cardData = new CardData(card)
            {
                IsOpen = true
            };
            var playerCardData = new PlayerCardData(cardData)
            {
                IsOpen = true
            };
            PlayerCards.Add(playerCardData);

            SelectedCard = PlayerCards[0];

            AllCardsSet = new List<CardData>();

            PlayerMoneyCases = new List<PlayerMoneyCaseOpenedData>();
            var moneyCases = Resources.LoadAll<MoneyCaseData>("Cases/MoneyCases/");
            foreach (var moneyCase in moneyCases)
            {
                var playerMoneyCase = new PlayerMoneyCaseOpenedData(moneyCase);
                PlayerMoneyCases.Add(playerMoneyCase);
            }

                Debug.Log("PLAYER MONEY CASES COUNT: " + PlayerMoneyCases);

            PlayerBeds = new List<BedData>();

            var bed = Resources.Load<BedObject>("Beds/Обычная лежанка");
            var bedData = new BedData(bed)
            {
                IsOpen = true
            };
            var playerBedData = bedData.Copy();
            PlayerBeds.Add(playerBedData);

            SelectedBed = PlayerBeds[0];

            AllBedsSet = new List<BedData>();

            PlayerBackgrounds = new List<BackgroundData>();

            var background = Resources.Load<BackgroundObject>("Backgrounds/Обычный фон");
            var backgroundData = new BackgroundData(background)
            {
                IsOpen = true
            };
            var playerBackgroundData = backgroundData.Copy();
            PlayerBackgrounds.Add(playerBackgroundData);

            SelectedBackground = PlayerBackgrounds[0];

            Money = 0;

            TotalAdsWatched = 0;
            TotalCasesOpened = 0;

            IsSoundOn = true;
            IsMusicOn = true;
        }
    }
}