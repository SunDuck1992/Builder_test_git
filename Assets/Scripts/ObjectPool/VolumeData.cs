using System;
using UnityEngine;

namespace PoolSystem
{
    [Serializable]
    public struct VolumeData
    {
        [SerializeField] private VolumeFXType _type;
        [SerializeField] private AudioClip _clip;

        public VolumeFXType Type => _type;
        public AudioClip Clip => _clip;
    }
}