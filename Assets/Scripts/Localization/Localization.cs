using System.IO;
using UnityEngine;

namespace LocalizationSystem
{
    public class Localization : MonoBehaviour
    {
        public TextAsset text;

        private void Start()
        {
            LocalizationData localization = new LocalizationData();
            string json = JsonUtility.ToJson(localization);
            File.WriteAllText(Application.streamingAssetsPath + "/Localization.json", json);
        }
    }
}


