using System;
using System.Collections.Generic;
using UnityEngine;

namespace HouseSystem
{
    [Serializable]
    public class StageEventObjects
    {
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private GameObject _eventObject;
        [SerializeField] private int _stageNumber;

        public int StageNumber => _stageNumber;
        public GameObject EventObject => _eventObject;
        public List<ParticleSystem> Effects => _effects;
    }
}