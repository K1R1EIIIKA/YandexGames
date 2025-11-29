using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Controllers;
using _Scripts.Data.Cards;
using _Scripts.Infrastructure.Core.States;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Plugins;
using _Scripts.YG;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
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
        private LeaderBoardController _leaderBoardController;

        private float _elapsedTime;
        private float _leaderboardElapsedTime;
        private float _saveInterval = 10f;
        private float _leaderBoardSaveInterval = 20f;

        private bool _isSettingLanguage = false;

        [Inject]
        public void Construct(InventoryController inventoryController, ISaveLoadService saveLoadService,
            TransactionController transactionController, AdRewardController adRewardController,
            LeaderBoardController leaderBoardController)
        {
            _inventoryController = inventoryController;
            _saveLoadService = saveLoadService;
            _transactionController = transactionController;
            _adRewardController = adRewardController;
            _leaderBoardController = leaderBoardController;

            Debug.Log("Bootstrapper initialized");
        }

        private void Awake()
        {
            _game = ProjectContext.Instance.Container.Instantiate<Game>();

            _game.StateMachine.Enter<BootstrapState>();
            _adRewardController.Initialize();
            _leaderBoardController.Initialize();

            DontDestroyOnLoad(this);
            Debug.Log("Bootstrapper made his deal");

            SetLanguage();
        }

        private void SetLanguage()
        {
            var lang = "ru"; // JsLib.GetLanguage();
            // Преобразуем язык Yandex SDK в формат Unity
            string unityLangCode = ConvertYandexLangToUnity(lang);

            // Устанавливаем язык
            StartCoroutine(SetLocale(unityLangCode));
        }

        private IEnumerator SetLocale(string localeCode)
        {
            if (_isSettingLanguage) yield break; // Защита от повторных вызовов

            _isSettingLanguage = true;

            // Дожидаемся загрузки настроек локализации
            if (!LocalizationSettings.InitializationOperation.IsDone)
                yield return LocalizationSettings.InitializationOperation;

            // Ищем соответствующую локаль
            Locale targetLocale = LocalizationSettings.AvailableLocales.Locales.Find(locale => locale.Identifier.Code == localeCode);

            if (targetLocale != null)
            {
                LocalizationSettings.SelectedLocale = targetLocale;
                Debug.Log($"Язык установлен: {targetLocale.LocaleName} ({localeCode})");
            }
            else
            {
                Debug.LogWarning($"Локаль {localeCode} не найдена, используется стандартная.");
            }

            _isSettingLanguage = false;
        }

        private string ConvertYandexLangToUnity(string yandexLang)
        {
            switch (yandexLang)
            {
                case "ru": return "ru"; // Русский
                case "en": return "en"; // Английский
                case "tr": return "tr"; // Турецкий
                default: return "ru"; // Язык по умолчанию
            }
        }
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _saveInterval)
            {
                _saveLoadService.SaveProgress();
                _elapsedTime = 0;
            }

            _leaderboardElapsedTime += Time.deltaTime;
            if (_leaderboardElapsedTime >= _leaderBoardSaveInterval)
            {
                _leaderBoardController.SaveLeaderBoardScore();
                _leaderboardElapsedTime = 0;
            }
        }
    }
}