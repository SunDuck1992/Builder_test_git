using Agava.YandexGames;
using UnityEngine;

namespace UI.LeaderBoardSystem
{
    public class Auuthorization : MonoBehaviour
    {
        public void AuthorizePlayer()
        {
#if !UNITY_EDITOR
        PlayerAccount.Authorize();
        RequestDataPermission();

        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }
#endif
        }

        private void RequestDataPermission()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }
        }
    }
}

