using UnityEngine;

namespace _Scripts.Infrastructure.Core
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            var bootstrapper = FindFirstObjectByType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                _gameBootstrapper.enabled = true;
                Instantiate(_gameBootstrapper);
            }
        }
    }
}