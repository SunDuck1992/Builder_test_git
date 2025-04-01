using UnityEngine;

namespace UI
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}