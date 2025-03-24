using System.Collections.Generic;
using WareHouseSystem;

namespace HouseSystem
{
    public class MaterialInfo
    {
        public int currentCount;
        public Queue<BuildMaterial> materialCells = new();
        public int maxCount;
    }
}