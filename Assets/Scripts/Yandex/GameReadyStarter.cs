using Agava.YandexGames;
using UnityEngine;

namespace YandexSystem
{
    public class GameReadyStarter : MonoBehaviour
    {
        private void Start()
        {
            YandexGamesSdk.GameReady();
        }
    }
}

