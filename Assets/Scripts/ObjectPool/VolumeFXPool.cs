using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public class VolumeFXPool : MonoBehaviour
    {
        private LinkedList<AudioSource> _pool = new();
        private Transform _container;
        private VolumeFXResources _resources;

        public VolumeFXPool(VolumeFXResources audio)
        {
            _container = new GameObject().transform;
            _resources = audio;
            MonoBehaviour.DontDestroyOnLoad(_container);
        }

        public AudioSource Spawn(VolumeFXType volumeFXType)
        {
            if (_pool.Count > 0)
            {
                AudioSource audioSource = _pool.Last.Value;
                _pool.RemoveLast();
                audioSource.clip = _resources.GetFX(volumeFXType);
                audioSource.Play();
                return audioSource;
            }

            var result = MonoBehaviour.Instantiate(_resources.AudioSource);
            result.transform.position = Camera.main.transform.position;
            result.clip = _resources.GetFX(volumeFXType);
            result.Play();
            return result;
        }

        public void Despawn(AudioSource prefab)
        {
            _pool.AddLast(prefab);
            prefab.Stop();
            prefab.transform.SetParent(_container);
        }
    }
}