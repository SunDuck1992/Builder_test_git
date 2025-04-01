using System;
using UnityEngine;

namespace PoolSystem
{
    [Serializable]
    public struct Data
    {
        [SerializeField] private FXType _type;
        [SerializeField] private ParticleSystem _particleSystem;

        public FXType Type => _type;
        public ParticleSystem ParticleSystem => _particleSystem;
    }
}