using System;
using _Scripts.Controllers;
using UnityEngine;
using YG;
using YG.Utils.LB;
using Zenject;

namespace _Scripts.YG
{
    public class LeaderBoardController
    {
        private const string YandexLeaderBoardName = "OmNomsScore";
        private const int MaxQuantityPlayers = 10;
        private const int QuantityTopPlayers = 3;
        private const int QuantityAroundPlayer = 5;

        private TransactionController _transactionController;
        private Action<LBData> _onGetLeaderBoard;

        [Inject]
        public void Construct(TransactionController transactionController)
        {
            _transactionController = transactionController;
        }

        public void Initialize()
        {
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        public void GetLeaderBoard(Action<LBData> onGetLeaderBoard)
        {
            _onGetLeaderBoard = onGetLeaderBoard;
            YandexGame.GetLeaderboard(YandexLeaderBoardName, MaxQuantityPlayers, QuantityTopPlayers, QuantityAroundPlayer, "score");

        }

        private void OnGetLeaderboard(LBData obj)
        {
            _onGetLeaderBoard?.Invoke(obj);
        }

        public void SaveLeaderBoardScore()
        {
            var score = _transactionController.GetPlayerCardsCount();
            Debug.Log($"Saving score {score} to Yandex Leaderboard");
            YandexGame.NewLeaderboardScores(YandexLeaderBoardName, score);
        }
    }
}