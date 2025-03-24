using TMPro;
using UnityEngine;

namespace UI.LeaderBoardSystem
{
    public class LeaderBoardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        public void Initialize(string name, int rank, int score)
        {
            _playerName.text = name;
            _playerRank.text = rank.ToString();
            _playerScore.text = score.ToString();
        }
    }
}

