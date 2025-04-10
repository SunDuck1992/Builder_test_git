using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoSaveSystem;
using ConstValues;
using PoolSystem;
using WareHouseSystem;

namespace HouseSystem
{
    public class House : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private ConstructionSite _constructionSite;
        [SerializeField] private List<StageEventObjects> _stageEventObjects;

        private List<StageInfo> _stages = new List<StageInfo>();
        private HouseProgress _saveData;
        private float _delay = 3f;
        private int _startHouseIndex = 0;

        public event Action CompletedHouse;

        public bool IsCanBuild => CurrentStage < _stages.Count;
        public int CurrentStage { get; private set; }
        public int CurrnetElementsCount { get; private set; }
        public int MaxElementsCount { get; private set; }
        public ConstructionSite ConstructionSite => _constructionSite;
        public IReadOnlyList<Materials> StageMaterials => _stages[CurrentStage].StageMaterials;

        private void Awake()
        {
            string json = PlayerPrefs.GetString(StringConstValues.House, string.Empty);

            if (string.IsNullOrEmpty(json))
            {
                _saveData = new HouseProgress();
            }
            else
            {
                _saveData = JsonUtility.FromJson<HouseProgress>(json);
                CurrentStage = _saveData.CurrentStage;
            }

            for (int i = CurrentStage; i > 0; i--)
            {
                ActivateEventObjects(i, true);
            }

            for (int i = 0; i < _root.childCount; i++)
            {
                var child = _root.GetChild(i);

                var materials = child.GetComponentsInChildren<BuildMaterial>();

                if (materials.Length > 0)
                {
                    var stageInfo = new StageInfo();

                    for (int j = 0; j < materials.Length; j++)
                    {
                        stageInfo.AddMaterial(materials[j], _saveData.ContainTo(materials[j], _stages.Count));

                        if (_saveData.ContainTo(materials[j], _stages.Count))
                        {
                            CurrnetElementsCount++;
                        }
                    }

                    _stages.Add(stageInfo);
                }

                MaxElementsCount += materials.Length;
            }
        }

        public (int max, int current) GetCountInfo(Materials material)
        {
            return _stages[CurrentStage].GetCountInfo(material);
        }

        public void NextStage()
        {
            CurrentStage++;
            _saveData.SetCurrentStage(CurrentStage);
            string json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString(StringConstValues.House, json);

            ActivateEventObjects(CurrentStage, false);
        }

        public void BuildElement(Transform target, float speed, Materials materials)
        {
            if (target == null)
            {
                return;
            }

            var stageInfo = _stages[CurrentStage];
            var element = stageInfo.GetMaterial(materials);
            element?.PutBrick(target, speed);

            var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.InstallBlock);
            StartCoroutine(VolumeFxPlay(volumeFX));

            _saveData.Save(element, CurrentStage);
            string json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString(StringConstValues.House, json);
            CurrnetElementsCount++;
        }

        public bool CheckIsComplete(Action callback)
        {
            var result = CurrnetElementsCount >= MaxElementsCount;

            if (result)
            {
                CompletedHouse?.Invoke();
                StartCoroutine(CompleteHouse(callback));
            }

            return result;
        }

        [ContextMenu("Hide")]
        public void HideBricks()
        {
            var buildMaterials = transform.GetComponentsInChildren<BuildMaterial>();

            for (int i = 0; i < buildMaterials.Length; i++)
            {
                buildMaterials[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        [ContextMenu("Show")]
        public void ShowBricks()
        {
            var buildMaterials = transform.GetComponentsInChildren<BuildMaterial>();

            for (int i = 0; i < buildMaterials.Length; i++)
            {
                buildMaterials[i].GetComponent<MeshRenderer>().enabled = true;
            }
        }

        private void ActivateEventObjects(int stage, bool isLoad)
        {
            var data = _stageEventObjects.Find(x => x.StageNumber == stage);

            if (data != null)
            {
                if (!isLoad)
                {
                    data.EventObject.SetActive(true);

                    foreach (ParticleSystem particle in data.Effects)
                    {
                        particle.Play();
                    }
                }

                data.EventObject.SetActive(true);
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

        private IEnumerator CompleteHouse(Action callback)
        {
            yield return new WaitForSeconds(_delay);
            PlayerPrefs.SetInt(StringConstValues.StartHouse, _startHouseIndex);
            callback?.Invoke();
        }       
    }
}