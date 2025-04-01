using System;
using UnityEngine;
using ConstValues;

namespace YandexSystem
{
    public static class VideoAdd
    {
        public static bool IsAdvertismetPlayed;

        public static void Show(Action rewardCallback = null)
        {
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, rewardCallback, OnCloseCallback);
        }

        public static void Show()
        {
            Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
        }

        private static void OnOpenCallback()
        {
            IsAdvertismetPlayed = true;
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
        }

        private static void OnCloseCallback()
        {
            IsAdvertismetPlayed = false;
            Time.timeScale = 1f;
            AudioListener.volume = PlayerPrefs.GetInt(StringConstValues.VolumeMusic, 1);
        }

        private static void OnCloseCallback(bool flag)
        {
            IsAdvertismetPlayed = false;
            Time.timeScale = 1f;
            AudioListener.volume = PlayerPrefs.GetInt(StringConstValues.VolumeMusic, 1);
        }
    }
}