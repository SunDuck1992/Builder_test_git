using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolSystem;
using WareHouseSystem;

namespace PlayerSystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _handPosition;
        [SerializeField] private FXResourses _fxData;

        private readonly float _distanceBetween = 0.25f;
        private LinkedList<GameObject> _itemsList = new();

        public event Action<int, int> Added;

        public int CurrentCount => _itemsList.Count;
        public Materials Material { get; private set; }

        private void Start()
        {
            OnUpdateCounter();
        }

        private void OnEnable()
        {
            UpgradePlayer.Instance.Upgraded += OnUpdateCounter;
        }

        private void OnDisable()
        {
            UpgradePlayer.Instance.Upgraded -= OnUpdateCounter;
        }

        public void AddItem(GameObject item, Materials material)
        {
            if (Material == Materials.None)
            {
                Material = material;
            }

            if (_itemsList.Count < UpgradePlayer.Instance.MaxCount)
            {
                SortList(item);
                _itemsList.AddLast(item);

                var fx = PoolService.Instance.FxPool.Spawn(FXType.TakeBlock);
                var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.TakeBlock);
                fx.transform.position = item.transform.position;

                StartCoroutine(VolumeFxPlay(volumeFX));
                StartCoroutine(FxPlay(fx.GetComponent<ParticleSystem>()));

                OnUpdateCounter();
            }

            if (Material != material)
                return;
        }

        public Transform GetItems()
        {
            GameObject item = _itemsList.Last.Value;
            _itemsList.RemoveLast();
            item.transform.SetParent(null);

            if (_itemsList.Count <= 0)
            {
                Material = Materials.None;
            }

            OnUpdateCounter();

            return item.transform;
        }

        public void ThrowOutItem()
        {
            GameObject item = _itemsList.Last.Value;
            _itemsList.RemoveLast();

            PoolService.Instance.GetPool(item).DeSpawn(item);

            var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.ThrowOutTrash);
            volumeFX.volume = 0.1f;
            StartCoroutine(VolumeFxPlay(volumeFX));

            if (_itemsList.Count <= 0)
            {
                Material = Materials.None;
            }

            OnUpdateCounter();
        }
      
        private void SortList(GameObject item)
        {
            if (_itemsList.Count > 0)
            {
                Vector3 offset = _itemsList.Last.Value.transform.position + Vector3.up * _distanceBetween;
                item.transform.position = offset;
            }
            else
            {
                item.transform.position = _handPosition.position;
            }

            item.transform.SetParent(_handPosition);
            item.transform.localRotation = Quaternion.identity;
        }

        private IEnumerator FxPlay(ParticleSystem particle)
        {
            while (particle.isPlaying)
            {
                yield return null;

                if (!particle.isPlaying)
                {
                    PoolService.Instance.FxPool.Despawn(particle);
                    break;
                }
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

        private void OnUpdateCounter()
        {
            Added?.Invoke(_itemsList.Count, UpgradePlayer.Instance.MaxCount);
        }
    }
}