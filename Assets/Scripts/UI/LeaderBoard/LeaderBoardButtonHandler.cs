using UnityEngine;
using Agava.YandexGames;

namespace UI.LeaderBoardSystem
{
    public class LeaderBoardButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardWindow;
        [SerializeField] private GameObject _authorizationWindow;
        [SerializeField] private LeaderBoard _leaderBoard;

        public void OnButtonClick()
        {
#if !UNITY_EDITOR
        if(PlayerAccount.IsAuthorized == false)
        {
            _authorizationWindow.SetActive(true);
        }
        else if(PlayerAccount.IsAuthorized == true)
        {
            _leaderboardWindow.SetActive(true);
            _leaderBoard.Fill();
        }
#endif
        }
    }
}