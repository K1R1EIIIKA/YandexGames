using System;
using System.Collections.Generic;

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
        public List<CardData> Cards;

        public ExampleDataClass(string sceneName)
        {
            SceneName = sceneName;
            Cards = new List<CardData>();
        }
    }
}