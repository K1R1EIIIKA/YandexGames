using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Scripts.Infrastructure.Core.SceneTransitions
{
    public class SceneLoader
    {
        // private readonly ICoroutineRunner _coroutineRunner;
        private string _currentSceneName;

        // [Inject]
        // public SceneLoader(ICoroutineRunner coroutineRunner)
        // {
        //     _coroutineRunner = coroutineRunner;
        // }
        //
        // public void Load(string name, Action onLoaded = null)
        // {
        //     _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        // }

        // private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        // {
        //     if (SceneManager.GetActiveScene().name == nextScene)
        //     {
        //         onLoaded?.Invoke();
        //         yield break;
        //     }
        //
        //     var waitNextScene = SceneManager.LoadSceneAsync(nextScene);
        //
        //     while (!waitNextScene.isDone)
        //         yield return null;
        //
        //     onLoaded?.Invoke();
        // }

        private async UniTask SwitchSceneWithoutUnload(string nextSceneName)
        {
            if (SceneManager.GetActiveScene().name == _currentSceneName)
            {
                return;
            }

            Scene currentScene = SceneManager.GetSceneByName(SceneManager.GetActiveScene().name);

            var operation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            await operation;

            SetSceneRootsActive(currentScene, active: false);

            Scene newScene = SceneManager.GetSceneByName(nextSceneName);
            SceneManager.SetActiveScene(newScene);
            SetSceneRootsActive(newScene, active: true);
        }

        public async UniTask SwitchSceneWithUnload(string nextSceneName)
        {
            //await UniTask.WaitForSeconds(2);
            if (SceneManager.GetActiveScene().name == nextSceneName)
            {
                return;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            _currentSceneName = currentScene.name;

            var loadOperation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            await loadOperation;

            SetSceneRootsActive(currentScene, false);

            Scene newScene = SceneManager.GetSceneByName(nextSceneName);
            SceneManager.SetActiveScene(newScene);

            SetSceneRootsActive(newScene, true);

            var unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            await unloadOperation;

            _currentSceneName = SceneManager.GetActiveScene().name;
        }

        private void SetSceneRootsActive(Scene currentScene, bool active)
        {
            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            foreach (GameObject go in rootObjects)
            {
                go.SetActive(active);
            }
        }
    }
}