using System;
using UnityEngine;

namespace PoolSystem
{
    [Serializable]
    public struct VolumeData
    {
        public VolumeFXType type;
        public AudioClip clip;
    }
}