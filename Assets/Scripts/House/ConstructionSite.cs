using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using PlayerSystem;
using PoolSystem;
using WareHouseSystem;

namespace HouseSystem
{
    public class ConstructionSite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _triggerZone;
        [SerializeField] private Color _targetColor;
        [SerializeField] private House _house;
        [SerializeField] private float _delay;
        [SerializeField] private float _speed;

        private Coroutine _coroutine;

        public event Action<Materials, int, int> Building;
        public event Action CompletedStage;
        public event Action<int, int> HouseBuilding;
        public event Action CompletedBuild;
        public House House => _house;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                _triggerZone.color = _targetColor;
                _coroutine = StartCoroutine(BuildHouse(player.Inventory));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                _triggerZone.color = Color.white;

                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
            }
        }

        private IEnumerator BuildHouse(Inventory inventory)
        {
            var material = inventory.Material;

            while (inventory.CurrentCount > 0 & _house.IsCanBuild && _house.StageMaterials.Contains(material))
            {
                _house.BuildElement(inventory.GetItems(), _speed, material);
                var info = _house.GetCountInfo(material);
                Building?.Invoke(material, info.current, info.max);
                HouseBuilding?.Invoke(_house.CurrnetElementsCount, _house.MaxElementsCount);

                if (_house.CheckIsComplete(() => CompletedBuild?.Invoke()))
                {
                    var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.BuildComplete);
                    StartCoroutine(VolumeFxPlay(volumeFX));
                }

                if (_house.StageMaterials.Count <= 0)
                {
                    _house.NextStage();
                    CompletedStage?.Invoke();
                }
                yield return new WaitForSeconds(_delay);
            }
        }

        private IEnumerator VolumeFxPlay(AudioSource audioSource)
        {
            while (audioSource.isPlaying)
            {
                yield return null;

                if (!audioSource.isPlaying)
                {
                    PoolService.Instance.VolumeFXPool.Despawn(audioSource);
                    break;
                }
            }
        }
    }
}


