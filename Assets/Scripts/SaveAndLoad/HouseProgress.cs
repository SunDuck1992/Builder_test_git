using System.Collections.Generic;
using System;
using WareHouseSystem;

namespace AutoSaveSystem
{
    [Serializable]
    public class HouseProgress
    {
        private const string Tamplate = "{0}_{1}";

        public List<string> name = new();
        public int currentStage;

        public bool ContainTo(BuildMaterial material, int currentStage)
        {
            return name.Contains(string.Format(Tamplate, material.transform.parent.name, currentStage));
        }

        public void Save(BuildMaterial buildMaterial, int currentStage)
        {
            if (buildMaterial != null)
            {
                name.Add(string.Format(Tamplate, buildMaterial.transform.parent.name, currentStage));
            }
        }
    }
}


