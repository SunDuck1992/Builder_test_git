using System.Collections.Generic;
using UnityEngine;
using HouseSystem;

namespace BuildPointSystem
{
    [CreateAssetMenu(fileName = nameof(BuildPointData), menuName = "Data/" + nameof(BuildPointData))]

    public class BuildPointData : ScriptableObject
    {
        [SerializeField] private List<House> _housePrefabs;
        [SerializeField] private House _startHouse;

        public IReadOnlyList<House> HousePrefabs => _housePrefabs;
        public House StartHouse => _startHouse;
    }
}


