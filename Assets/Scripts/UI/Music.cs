using UnityEngine;

namespace UI
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        void Start()
        {          
            DontDestroyOnLoad(gameObject);
        }
    }
}

