using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            var bootstrapper = FindFirstObjectByType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                _gameBootstrapper.enabled = true;
                _container.InstantiatePrefab(_gameBootstrapper.gameObject);
            }
        }
    }
}