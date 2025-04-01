using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private int _startSceneIndex = 2;

        public void PlayGame()
        {
            if (PlayerPrefs.HasKey(StringConstValues.SceneName))
            {
                SceneManager.LoadScene(PlayerPrefs.GetString(StringConstValues.SceneName));
            }
            else
            {
                SceneManager.LoadScene(_startSceneIndex);
            }
        }
    }
}