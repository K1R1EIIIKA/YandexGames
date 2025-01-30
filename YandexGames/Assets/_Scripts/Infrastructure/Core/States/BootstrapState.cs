
using _Scripts.Infrastructure.Core.SceneTransitions;

namespace _Scripts.Infrastructure.Core.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            //Use like Project Installer
            
            
           //  _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
           //  _allServices.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
           //
           //  _allServices.RegisterSingle<IStaticDataService>(new StaticDataService());
           //
           // _allServices.RegisterSingle<IGameFactory>(new GameFactory(
           //               (IAssetProvider) _allServices.Single<IAssetProvider>(),
           //               (IStaticDataService) _allServices.Single<IStaticDataService>()));
           //
           //  _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
           //      (PersistantProgressService) _allServices.Single<IPersistantProgressService>(),
           //      (GameFactory) _allServices.Single<IGameFactory>()));
           //  
           //  _allServices.RegisterSingle<ILocalizationService>(new LocalizationService());
        }
    }
}