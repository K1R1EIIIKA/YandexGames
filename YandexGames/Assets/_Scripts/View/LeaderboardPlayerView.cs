using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.View
{
    public class LeaderboardPlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _playerScore;
        [SerializeField] private RawImage _playerAvatar;

        public void SetData(string playerName, int playerScore)
        {
            _playerName.text = playerName;
            _playerScore.text = playerScore.ToString();
        }

        public void SetAvatar(Texture2D avatar)
        {
            _playerAvatar.texture = avatar;
        }
    }
}