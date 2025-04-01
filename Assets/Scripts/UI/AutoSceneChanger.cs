using UnityEngine;
using ConstValues;

namespace UI
{
    public class AutoSceneChanger : MonoBehaviour
    {
        private const string NeedChange = StringConstValues.NeedChangeYes;

        [SerializeField] private EndLevel _endLevel;

        private void Awake()
        {
            if (PlayerPrefs.GetString(StringConstValues.NeedChange) == NeedChange)
            {
                _endLevel.NextLevel();
            }
        }
    }
}