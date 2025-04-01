using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BuildPointSystem;
using HouseSystem;
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
            _construction.HouseBuilding -= OnShowHouseBuildProgress;
            _construction.Building -= OnShowProgress;
            _construction.CompletedStage -= OnCompleteStage;
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

        private void OnShowHouseBuildProgress(int current, int maxCount)
        {
            _houseProgressText.text = $"{current} / {maxCount}";
            _progressSlider.value = current / (float)maxCount;
        }

        private void OnSetup(ConstructionSite constructionSite)
        {
            constructionSite.Building += OnShowProgress;
            constructionSite.CompletedStage += OnCompleteStage;
            constructionSite.HouseBuilding += OnShowHouseBuildProgress;

            _construction = constructionSite;
            ShowMaterial();
            OnShowHouseBuildProgress(constructionSite.House.CurrnetElementsCount, constructionSite.House.MaxElementsCount);
        }

        private void OnShowProgress(Materials material, int currentCount, int maxCount)
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
    }
}