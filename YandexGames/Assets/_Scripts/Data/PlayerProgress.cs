using System;

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
        public String SceneNameName;

        public ExampleDataClass(string sceneName)
        {
            SceneNameName = sceneName;
        }
    }
}