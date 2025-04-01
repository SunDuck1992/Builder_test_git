using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using ConstValues;

namespace YandexSystem
{
    public sealed class YandexBridge : MonoBehaviour
    {        
        public static YandexBridge Instance;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(LoadSceneValues.MainMenuScene);
        }
    }
}