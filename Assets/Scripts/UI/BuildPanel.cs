using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using HouseSystem;
using BuildPointSystem;
using WareHouseSystem;

namespace UI
{
    public class BuildPanel : MonoBehaviour
    {
        [SerializeField] private List<BuildProgress> _buildProgresses;
        [SerializeField] private TextMeshProUGUI _houseProgressText;
        [SerializeField] private BuildPoint _buildPoint;
        [SerializeField] private Slider _progressSlider;

        private ConstructionSite _construction;

        private void Awake()
        {
            _buildPoint = FindObjectOfType<BuildPoint>();
            _buildPoint.Building += OnSetup;
        }

        private void OnDestroy()
        {
            _buildPoint.Building -= OnSetup;

            _construction.HouseBuilding -= ShowHouseBuildProgress;
            _construction.Building -= ShowProgress;
            _construction.CompletedStage -= OnCompleteStage;
        }

        public void OnSetup(ConstructionSite constructionSite)
        {
            constructionSite.Building += ShowProgress;
            constructionSite.CompletedStage += OnCompleteStage;
            constructionSite.HouseBuilding += ShowHouseBuildProgress;

            _construction = constructionSite;
            ShowMaterial();
            ShowHouseBuildProgress(constructionSite.House.CurrnetElementsCount, constructionSite.House.MaxElementsCount);
        }

        private void ShowProgress(Materials material, int currentCount, int maxCount)
        {
            BuildProgress progress = _buildProgresses.Find(x => x.Materials == material);

            if (progress != null)
            {
                progress.ShowProgress(currentCount, maxCount);
            }
        }

        private void OnCompleteStage()
        {
            ShowMaterial();
        }

        private void ShowMaterial()
        {
            if (!_buildPoint.Construction.House.IsCanBuild) return;

            _buildProgresses.ForEach(x =>
            {
                if (_buildPoint.Construction.House.StageMaterials.Contains(x.Materials))
                {
                    var countInfo = _buildPoint.Construction.House.GetCountInfo(x.Materials);
                    x.gameObject.SetActive(true);
                    x.ShowProgress(countInfo.current, countInfo.max);
                }
                else
                {
                    x.gameObject.SetActive(false);
                }
            });
        }

        private void ShowHouseBuildProgress(int current, int maxCount)
        {
            _houseProgressText.text = $"{current} / {maxCount}";
            _progressSlider.value = current / (float)maxCount;
        }
    }
}


