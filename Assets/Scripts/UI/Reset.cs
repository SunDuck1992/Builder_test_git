using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;

namespace UI
{
    public class Reset : MonoBehaviour
    {
        public void ReLoad(int scene)
        {
            SceneManager.LoadScene(scene);
            PlayerPrefs.DeleteKey(StringConstValues.House);
        }
    }
}

