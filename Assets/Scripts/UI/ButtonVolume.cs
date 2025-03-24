using UnityEngine;
using ConstValues;

namespace UI
{
    public class ButtonVolume : MonoBehaviour
    {
        [SerializeField] private GameObject _offVolume;

        private bool _enabled;

        private void Start()
        {
            _enabled = PlayerPrefs.GetInt(StringConstValues.VolumeMusic, 1) == 1;
            Sound();
        }

        public void ChangeSound()
        {
            _enabled = !_enabled;
            Sound();
            PlayerPrefs.SetInt(StringConstValues.VolumeMusic, (_enabled == true) ? 1 : 0);
        }

        private void Sound()
        {
            AudioListener.volume = (_enabled == true) ? 1.0f : 0.0f;
            _offVolume.SetActive(!_enabled);
        }
    }
}

