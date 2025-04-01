using System.Collections.Generic;
using UnityEngine;
using WareHouseSystem;

namespace HouseSystem
{
    public class StageInfo
    {
        private Dictionary<Materials, MaterialInfo> _infoes = new();
        private List<Materials> _stageMaterials = new();

        public IReadOnlyList<Materials> StageMaterials => _stageMaterials;

        public (int max, int current) GetCountInfo(Materials material)
        {
            var materialInfo = _infoes[material];
            return (materialInfo.MaxCount, materialInfo.CurrentCount);
        }

        public void AddMaterial(BuildMaterial buildMaterial, bool isLoad)
        {
            if (!_infoes.TryGetValue(buildMaterial.Materials, out var info))
            {
                info = new();
                _infoes.Add(buildMaterial.Materials, info);
            }

            if (isLoad)
            {
                buildMaterial.GetComponent<MeshRenderer>().enabled = true;
                info.IncreaseCurrentCount();
            }
            else
            {
                if (!_stageMaterials.Contains(buildMaterial.Materials))
                {
                    _stageMaterials.Add(buildMaterial.Materials);
                }

                info.AddMaterial(buildMaterial);
            }

            info.IncreaseMaxCount();
        }

        public BuildMaterial GetMaterial(Materials material)
        {
            if (_infoes.TryGetValue(material, out var info))
            {
                var element = info.RemoveMaterial();
                info.IncreaseCurrentCount();

                if (info.MaterialCells.Count <= 0)
                {
                    _stageMaterials.Remove(material);
                }

                return element;
            }

            return null;
        }
    }
}