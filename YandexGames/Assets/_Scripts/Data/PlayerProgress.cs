using System;
using System.Collections.Generic;
using _Scripts.Data.Cards;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerProgress         //Class with game data, that must be saved
    {
        public ExampleDataClass LevelsProgress;

        public PlayerProgress(string sceneName)
        {
            LevelsProgress = new ExampleDataClass(sceneName);
        }
    }

    [Serializable]
    public class ExampleDataClass
    {
        public String SceneName;
        public List<CardData> PlayerCards;
        public List<CardData> AllCardsSet;

        public ExampleDataClass(string sceneName)
        {
            SceneName = sceneName;
            PlayerCards = new List<CardData>();
            AllCardsSet = new List<CardData>();
        }
    }
}