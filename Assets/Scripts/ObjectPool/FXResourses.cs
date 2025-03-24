using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    [CreateAssetMenu(fileName = nameof(FXResourses), menuName = "Data/" + nameof(FXResourses))]

    public partial class FXResourses : ScriptableObject
    {
        [SerializeField] private List<Data> _datas;

        public void ForEach(Action<FXType, ParticleSystem> action)
        {
            _datas.ForEach(x => action?.Invoke(x.type, x.particleSystem));
        }
    }
}

