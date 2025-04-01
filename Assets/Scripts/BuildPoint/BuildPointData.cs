using System.Collections.Generic;
using HouseSystem;
using UnityEngine;

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