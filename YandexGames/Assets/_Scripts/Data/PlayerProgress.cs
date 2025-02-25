using System;
using System.Collections.Generic;
using _Scripts.Data.Cards;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public ExampleDataClass LevelsProgress;

        public PlayerProgress(string sceneName)
        {
            LevelsProgress = new ExampleDataClass(sceneName);
        }

        public override string ToString()
        {
            return $"PlayerProgress: {LevelsProgress.SceneName}, Money: {LevelsProgress.Money}, PlayerCards: {LevelsProgress.PlayerCards.Count}, AllCardsSet: {LevelsProgress.AllCardsSet.Count}";
        }
    }

    [Serializable]
    public class ExampleDataClass
    {
        public String SceneName;
        public List<CardData> PlayerCards;
        public List<CardData> AllCardsSet;
        public int Money;

        public ExampleDataClass(string sceneName)
        {
            SceneName = sceneName;
            PlayerCards = new List<CardData>();
            AllCardsSet = new List<CardData>();
            Money = 0;
        }
    }
}