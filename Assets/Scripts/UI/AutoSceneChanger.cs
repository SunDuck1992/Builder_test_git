using UnityEngine;
using ConstValues;

namespace UI
{
    public class AutoSceneChanger : MonoBehaviour
    {
        [SerializeField] private EndLevel _endLevel;

        private const string NeedChange = StringConstValues.NeedChangeYes;

        private void Awake()
        {
            if (PlayerPrefs.GetString(StringConstValues.NeedChange) == NeedChange)
            {
                _endLevel.NextLevel();
            }
        }
    }
}

