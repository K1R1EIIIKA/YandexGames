using System.Collections;
using _Scripts.Plugins;
using _Scripts.View;
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
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private Transform _container;

        private LeaderBoardController _leaderBoardController;

        [Inject]
        public void Construct(LeaderBoardController leaderBoardController)
        {
            _leaderBoardController = leaderBoardController;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseAccount);
        }

        public void SetPlayerName(string name)
        {
            _playerName.text = name;
        }

        public void SetPlayerAvatar(string url)
        {
            StartCoroutine(DownloadPlayerImage(url));
        }
        private void OnGetLeaderBoard(LBData obj)
        {
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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _leaderBoardController.SaveLeaderBoardScore();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAccount();
            }
        }

        public void OpenAccount()
        {
            gameObject.SetActive(true);
            _leaderBoardController.GetLeaderBoard(OnGetLeaderBoard);

            JsLib.GetPlayerData();
        }


        private void CloseAccount()
        {
            gameObject.SetActive(false);
        }
    }
}