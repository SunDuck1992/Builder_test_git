using UnityEngine;
using TMPro;

namespace PlayerSystem
{
    public class FollowText : MonoBehaviour
    {
        private const string Template = "{0} / {1}";

        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _target;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Inventory _inventory;

        private void FixedUpdate()
        {
            if (_target == null)
            {
                return;
            }

            _target.position = Camera.main.WorldToScreenPoint(_pivot.position);
        }

        private void OnEnable()
        {
            _inventory.Added += OnShowInfo;
        }

        private void OnDisable()
        {
            _inventory.Added -= OnShowInfo;
        }

        private void OnShowInfo(int currentCount, int maxCount)
        {
            _infoText.text = string.Format(Template, currentCount, maxCount);
        }
    }
}

