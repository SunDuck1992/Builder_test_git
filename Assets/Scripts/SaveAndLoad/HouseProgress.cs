using System.Collections.Generic;
using System;
using WareHouseSystem;

namespace AutoSaveSystem
{
    [Serializable]
    public class HouseProgress
    {
        private const string Tamplate = "{0}_{1}";

        private int _currentStage;
        private List<string> _name = new();

        public int CurrentStage => _currentStage;

        public bool ContainTo(BuildMaterial material, int currentStage)
        {
            return _name.Contains(string.Format(Tamplate, material.transform.parent.name, currentStage));
        }

        public void Save(BuildMaterial buildMaterial, int currentStage)
        {
            if (buildMaterial != null)
            {
                _name.Add(string.Format(Tamplate, buildMaterial.transform.parent.name, currentStage));
            }
        }

        public void SetCurrentStage(int stage)
        {
            _currentStage = stage;
        }
    }
}