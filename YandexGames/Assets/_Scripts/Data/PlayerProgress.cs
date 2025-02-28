using System;
using System.Collections.Generic;
using _Scripts.Data.Cards;
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
            return $"PlayerProgress: {LevelsProgress.SceneName}, Money: {LevelsProgress.Money}, PlayerCards: {LevelsProgress.PlayerCards}, AllCardsSet: {LevelsProgress.AllCardsSet}";
        }
    }

    [Serializable]
    public class OmNomData
    {
        public String SceneName;
        public List<PlayerCardData> PlayerCards;
        public PlayerCardData SelectedCard;
        public List<CardData> AllCardsSet;
        public int Money;

        public OmNomData(string sceneName)
        {
            SceneName = sceneName;
            PlayerCards = new List<PlayerCardData>();

            var card = Resources.Load<CardObject>("Cards/Амням");
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
            Money = 0;
        }
    }
}