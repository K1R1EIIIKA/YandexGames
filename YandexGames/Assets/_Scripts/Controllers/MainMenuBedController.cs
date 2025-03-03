using _Scripts.BuffLogic;
using _Scripts.BuffLogic.Buffs;
using _Scripts.Data;
using _Scripts.Data.Beds;
using _Scripts.Data.Cards;
using _Scripts.Enums;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Controllers
{
    public class MainMenuBedController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Image _bedImage;

        private TransactionController _transactionController;
        private GameFactory _gameFactory;
        private BuffController _buffController;

        private BedData _selectedBed;


        [Inject]
        public void Construct(TransactionController transactionController, GameFactory gameFactory,
            BuffController buffController)
        {
            _transactionController = transactionController;
            _gameFactory = gameFactory;
            _buffController = buffController;

            _gameFactory.Register(this);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _selectedBed = progress.LevelsProgress.SelectedBed;

            if (_selectedBed != null && _bedImage != null)
            {
                _bedImage.sprite = _selectedBed.ToBedObject().BedImage;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
            {
                // nope
            }
        }
    }