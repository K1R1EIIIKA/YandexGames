using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;
using Zenject;

namespace _Scripts.Controllers
{
    public class AccountController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _leaderBoardText;

        private LeaderBoardController _leaderBoardController;

        [Inject]
        public void Construct(LeaderBoardController leaderBoardController)
        {
            _leaderBoardController = leaderBoardController;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseAccount);
            YandexGame.onGetLeaderboard += OnGetLeaderBoard;
        }

        private void OnGetLeaderBoard(LBData obj)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in obj.players)
            {
                sb.Append(item.name + " " + item.score + "\n");
            }

            _leaderBoardText.text = sb.ToString();
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
        }

        public void OpenAccount()
        {
            gameObject.SetActive(true);
            _leaderBoardController.GetLeaderBoard();

        }



        public void CloseAccount()
        {
            gameObject.SetActive(false);
        }
    }
}