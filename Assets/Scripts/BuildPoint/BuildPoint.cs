using System;
using UnityEngine;
using HouseSystem;
using PoolSystem;
using ConstValues;
using Random = UnityEngine.Random;

namespace BuildPointSystem
{
    public class BuildPoint : MonoBehaviour
    {
        [SerializeField] private BuildPointData _data;
        [SerializeField] private FXResourses _fXData;
        [SerializeField] private VolumeFXResources _volumeFXResources;

        public ConstructionSite Construction { get; private set; }

        public event Action<ConstructionSite> Building;

        void Start()
        {
#if !UNITY_EDITOR
        if (PlayerPrefs.HasKey(StringConstValues.StartHouse))
        {
            Agava.YandexGames.VideoAd.Show();
        }
#endif
            int houseNumber = PlayerPrefs.GetInt(StringConstValues.HouseNumber, Random.Range(0, _data.HousePrefabs.Count));
            House house = _data.HousePrefabs[houseNumber];

            if (!PlayerPrefs.HasKey(StringConstValues.StartHouse))
            {
                house = _data.StartHouse;
            }

            var building = Instantiate(house, transform.position, transform.rotation);
            Construction = building.ConstructionSite;
            Building?.Invoke(Construction);

            PoolService.Instance.FxPool = new FXPool(_fXData);
            PoolService.Instance.VolumeFXPool = new VolumeFXPool(_volumeFXResources);

            PlayerPrefs.SetInt(StringConstValues.HouseNumber, houseNumber);
        }
    }
}


