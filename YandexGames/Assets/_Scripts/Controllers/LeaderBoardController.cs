using UnityEngine;
using YG;
using YG.Utils.LB;
using Zenject;

namespace _Scripts.Controllers
{
    public class LeaderBoardController
    {
        private const string YandexLeaderBoardName = "OmNomsScore";
        private const int MaxQuantityPlayers = 10;
        private const int QuantityTopPlayers = 3;
        private const int QuantityAroundPlayer = 5;

        private TransactionController _transactionController;

        [Inject]
        public void Construct(TransactionController transactionController)
        {
            _transactionController = transactionController;
        }

        public void Initialize()
        {
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        public void GetLeaderBoard()
        {
            YandexGame.GetLeaderboard(YandexLeaderBoardName, MaxQuantityPlayers, QuantityTopPlayers, QuantityAroundPlayer, "score");
        }

        private void OnGetLeaderboard(LBData obj)
        {
            Debug.Log("OnGetLeaderboard");
            foreach (var item in obj.players)
            {
                Debug.Log($"Player: {item.name}, score: {item.score}, rank: {item.rank}");
            }
        }

        public void SaveLeaderBoardScore()
        {
            var score = _transactionController.GetPlayerCardsCount();
            Debug.Log($"Saving score {score} to Yandex Leaderboard");
            YandexGame.NewLeaderboardScores(YandexLeaderBoardName, score);
        }
    }
}