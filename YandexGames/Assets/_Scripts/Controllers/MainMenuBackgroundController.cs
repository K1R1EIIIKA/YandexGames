using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuBackgroundController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Image _backgroundImage;

        private GameFactory _gameFactory;

        private BackgroundData _selectedBackground;


        [Inject]
        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _gameFactory.Register(this);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _selectedBackground = progress.LevelsProgress.SelectedBackground;

            if (_selectedBackground != null && _backgroundImage != null)
            {
                _backgroundImage.sprite = _selectedBackground.ToBackgroundObject().BackgroundImage;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // nope
        }
    }
}