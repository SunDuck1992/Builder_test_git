using UnityEngine;
using TMPro;
using WareHouseSystem;

namespace UI
{
    public class BuildProgress : MonoBehaviour
    {
        private const string Template = "{0} / {1}";

        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Materials _materials;

        public Materials Materials => _materials;

        public void ShowProgress(int currentCount, int maxCount)
        {
            _progressText.text = string.Format(Template, currentCount, maxCount);
        }
    }
}