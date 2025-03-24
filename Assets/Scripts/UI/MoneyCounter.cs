using UnityEngine;
using TMPro;
using PlayerSystem;

namespace UI
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start()
        {
            UpgradePlayer.Instance.ChangedMoney += OnShowMoney;
            UpgradePlayer.Instance.ChangedScore += OnShowScore;

            UpgradePlayer.Instance.Refresh();
        }

        private void OnDestroy()
        {
            UpgradePlayer.Instance.ChangedMoney -= OnShowMoney;
            UpgradePlayer.Instance.ChangedScore -= OnShowScore;
        }

        private void OnShowMoney(int money)
        {
            _moneyText.text = money.ToString();
        }

        private void OnShowScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}

