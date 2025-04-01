using UnityEngine;
using Agava.YandexGames;

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