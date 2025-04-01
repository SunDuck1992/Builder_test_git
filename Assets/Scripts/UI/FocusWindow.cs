using UnityEngine;
using ConstValues;
using YandexSystem;
using PlayerPrefs = UnityEngine.PlayerPrefs;

namespace UI
{
    public class FocusWindow : MonoBehaviour
    {
        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
        }

        private void MuteAudio(bool value)
        {
            if (value)
            {
                if (VideoAdd.IsAdvertismetPlayed)
                {
                    AudioListener.volume = 0;
                }
                else
                {
                    AudioListener.volume = PlayerPrefs.GetInt(StringConstValues.VolumeMusic, 1);
                }
            }
            else
            {
                AudioListener.volume = 0;
            }
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (!VideoAdd.IsAdvertismetPlayed)
            {
                MuteAudio(inApp);
                PauseGame(!inApp);
            }
        }
    }
}