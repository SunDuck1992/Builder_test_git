using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuildPointSystem;
using ConstValues;
using HouseSystem;
using PlayerSystem;
using UI.LeaderBoardSystem;

namespace UI
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private string[] _sceneNames;
        [SerializeField] private BuildPoint _buildPoint;
        [SerializeField] private GameObject _root;
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private TextMeshProUGUI _score;

        private ConstructionSite _construction;

        private void Awake()
        {
            _buildPoint = FindObjectOfType<BuildPoint>();
            _buildPoint.Building += OnSetup;
            _root.SetActive(false);
        }

        private void OnDestroy()
        {
            _buildPoint.Building -= OnSetup;
        }

        public void NextLevel()
        {
            LeaderBoard.SetPlayer(UpgradePlayer.Instance.Score);
            UpgradePlayer.Instance.StatisticMoney = 0;
            UpgradePlayer.Instance.StatisticScore = 0;
            PlayerPrefs.DeleteKey(StringConstValues.StatisticMoney);
            PlayerPrefs.DeleteKey(StringConstValues.StatisticScore);
            PlayerPrefs.DeleteKey(StringConstValues.House);
            PlayerPrefs.DeleteKey(StringConstValues.HouseNumber);
            PlayerPrefs.SetString(StringConstValues.NeedChange, StringConstValues.NeedChangeNo);
            Time.timeScale = 1f;
            var scene = _sceneNames[Random.Range(0, _sceneNames.Length)];
            SceneManager.LoadScene(scene);
            PlayerPrefs.SetString(StringConstValues.SceneName, scene);
        }

        private void OnSetup(ConstructionSite constructionSite)
        {
            constructionSite.CompletedBuild += OnShowPanel;
            _construction = constructionSite;
        }

        private void OnShowPanel()
        {
            _money.text = UpgradePlayer.Instance.StatisticMoney.ToString();
            _score.text = UpgradePlayer.Instance.StatisticScore.ToString();
            Time.timeScale = 0f;
            _root.SetActive(true);
            PlayerPrefs.SetString(StringConstValues.NeedChange, StringConstValues.NeedChangeYes);
            _construction.CompletedBuild -= OnShowPanel;
        }
    }
}