using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    [CreateAssetMenu(fileName = nameof(VolumeFXResources), menuName = "Data/" + nameof(VolumeFXResources))]

    public partial class VolumeFXResources : ScriptableObject
    {
        [SerializeField] private List<VolumeData> _datas;
        [SerializeField] private AudioSource _audioSourcePrefab;

        public AudioSource AudioSource => _audioSourcePrefab;

        public AudioClip GetFX(VolumeFXType fX)
        {
            var result = _datas.Find(x => x.Type == fX);
            return result.Clip;
        }
    }
}