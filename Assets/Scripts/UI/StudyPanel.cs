using UnityEngine;
using ConstValues;

namespace UI
{
    public class StudyPanel : MonoBehaviour
    {
        private void Awake()
        {
            if (PlayerPrefs.HasKey(StringConstValues.StartHouse))
            {
                gameObject.SetActive(false);
            }
        }
    }
}

