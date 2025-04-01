using System.Collections.Generic;
using WareHouseSystem;

namespace HouseSystem
{
    public class MaterialInfo
    {
        private int _currentCount;
        private Queue<BuildMaterial> _materialCells = new();
        private int _maxCount;

        public int MaxCount => _maxCount;
        public int CurrentCount => _currentCount;
        public Queue<BuildMaterial> MaterialCells => _materialCells;

        public void IncreaseMaxCount()
        {
            _maxCount++;
        }

        public void IncreaseCurrentCount()
        {
            _currentCount++;
        }

        public void AddMaterial(BuildMaterial buildMaterial)
        {
            _materialCells.Enqueue(buildMaterial);
        }

        public BuildMaterial RemoveMaterial()
        {
            if (_materialCells.Count > 0)
            {
                var material = _materialCells.Dequeue();
                return material;
            }

            return null;
        }
    }
}