using System.Linq;
using _Scripts.Controllers;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.YG;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;
        private InventoryController _inventoryController;
        private ISaveLoadService _saveLoadService;
        private TransactionController _transactionController;
        private AdRewardController _adRewardController;

        private float _elapsedTime;
        private float _saveInterval = 3f;

        [Inject]
        public void Construct(InventoryController inventoryController, ISaveLoadService saveLoadService,
            TransactionController transactionController, AdRewardController adRewardController)
        {
            _inventoryController = inventoryController;
            _saveLoadService = saveLoadService;
            _transactionController = transactionController;
            _adRewardController = adRewardController;

            Debug.Log("Bootstrapper initialized");
        }

        private void Awake()
        {
            _game = ProjectContext.Instance.Container.Instantiate<Game>();

            _game.StateMachine.Enter<BootstrapState>();
            _adRewardController.Initialize();
            // _inventoryController.Initialize();

            DontDestroyOnLoad(this);
            Debug.Log("Bootstrapper made his deal");
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _saveInterval)
            {
                _saveLoadService.SaveProgress();
                _elapsedTime = 0;
            }
        }
    }
}