using System.Collections;
using _Scripts.Plugins;
using _Scripts.Tools;
using _Scripts.UI;
using _Scripts.View;
using _Scripts.YG;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using YG.Utils.LB;
using Zenject;

namespace _Scripts.Controllers
{
    public class AccountController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LeaderboardPlayerView _leaderboardPlayerView;

        [SerializeField] private RawImage _playerAvatar;
        [SerializeField] private Image _playerCharacter;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _totalCharacters;
        [SerializeField] private Transform _container;

        [SerializeField] private SelectableButtonBehaviour _accountButton;
        [SerializeField] private SelectableButtonBehaviour _bestScoreButton;

        [SerializeField] private GameObject _accountPanel;
        [SerializeField] private GameObject _bestScorePanel;

        private LeaderBoardController _leaderBoardController;
        [Inject] private TransactionController _transactionController;

        [Inject]
        public void Construct(LeaderBoardController leaderBoardController)
        {
            _leaderBoardController = leaderBoardController;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseAccount);

            _accountButton.Button.onClick.AddListener(OnAccountButtonClick);
            _bestScoreButton.Button.onClick.AddListener(OnBestScoreButtonClick);

            OnAccountButtonClick();
        }

        private void OnAccountButtonClick()
        {
            _accountPanel.SetActive(true);

            _accountButton.Select();
            _bestScoreButton.Deselect();

            _playerCharacter.sprite = _transactionController.SelectedCard.ToCardObject().Image;
            _totalCharacters.text = "Всего Амнямов: " + _transactionController.GetPlayerCardsCount();
            _bestScorePanel.SetActive(false);
        }

        private void OnBestScoreButtonClick()
        {
            _bestScoreButton.Select();
            _accountButton.Deselect();

            _accountPanel.SetActive(false);
            _bestScorePanel.SetActive(true);
        }

        public void SetPlayerName(string name)
        {
            _playerName.text = name + " (Главный Амнямер)";
        }

        public void SetPlayerAvatar(string url)
        {
            StartCoroutine(DownloadPlayerImage(url));
        }

        private void OnGetLeaderBoard(LBData obj)
        {
            var rectContainer = _container as RectTransform;
            Debug.Log(11);
            rectContainer.DestroyAllChildren();

            for (int i = 0; i < obj.players.Length; i++)
            {
                LeaderboardPlayerView view = Instantiate(_leaderboardPlayerView, _container);
                view.SetData(obj.players[i].name, obj.players[i].score);

                StartCoroutine(DownloadOtherPlayerImage(obj.players[i].photo, view));
            }
        }

        private IEnumerator DownloadPlayerImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                _playerAvatar.texture = texture;
            }
        }

        private IEnumerator DownloadOtherPlayerImage(string url, LeaderboardPlayerView view)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                view.SetAvatar(texture);
            }
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseAccount);

            _accountButton.Button.onClick.RemoveListener(OnAccountButtonClick);
            _bestScoreButton.Button.onClick.RemoveListener(OnBestScoreButtonClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAccount();
            }
        }

        public void OpenAccount()
        {
            gameObject.SetActive(true);
            _leaderBoardController.GetLeaderBoard(OnGetLeaderBoard);

#if !UNITY_EDITOR
            JsLib.GetPlayerData();
#endif
        }

        private void CloseAccount()
        {
            gameObject.SetActive(false);
        }
    }
}