using _Scripts.Data;
using _Scripts.Data.Beds;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuBedController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Image _bedImage;

        private GameFactory _gameFactory;

        private BedData _selectedBed;

        [Inject]
        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _gameFactory.Register(this);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _selectedBed = progress.LevelsProgress.SelectedBed;

            if (_selectedBed != null && _bedImage != null)
            {
                _bedImage.sprite = _selectedBed.BedImage;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            // nope
        }
    }
}