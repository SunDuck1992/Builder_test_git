using System;
using System.Collections.Generic;
using UnityEngine;

namespace HouseSystem
{
    [Serializable]
    public class StageEventObjects
    {
        public List<ParticleSystem> effects;
        public GameObject eventObject;
        public int stageNumber;
    }
}