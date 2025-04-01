using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ConstValues;
using PlayerSystem;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<SkinSettings> _skinSettings;
        [SerializeField] private List<Image> _buttons;
        [SerializeField] private Sprite _sprite;

        public event Action<GameObject> ChangedSkin;

        private void Start()
        {
            Load();
        }

        public void ChangeSkinButtonClick(int index)
        {

            var skinSettings = _skinSettings[index];

            if (skinSettings.IsAds)
            {
                if (PlayerPrefs.HasKey($"skin_{index}"))
                {
                    ChangeSkin(skinSettings.GameObject);
                    ChangeButtonSprite(index);
                }
                else
                {
#if !UNITY_EDITOR
                Agava.YandexGames.VideoAd.Show(() =>
                {
                    ChangeSkin(skinSettings.gameObject);
                    ChangeButtonSprite(index);
                });
#endif
                }
            }
            else
            {
                if (PlayerPrefs.HasKey($"skin_{index}"))
                {
                    ChangeSkin(skinSettings.GameObject);
                    ChangeButtonSprite(index);
                }
                else
                {
                    if (UpgradePlayer.Instance.CheckMoney(skinSettings.Cost))
                    {
                        UpgradePlayer.Instance.ChangeMoney(-skinSettings.Cost);
                        ChangeSkin(skinSettings.GameObject);
                        ChangeButtonSprite(index);
                    }
                }
            }
        }
        public void ClickShop(bool isClick)
        {
            _animator.SetBool(StringConstValues.IsClick, isClick);
        }

        private void ChangeSkin(GameObject gameObject)
        {
            ChangedSkin?.Invoke(gameObject);
        }

        private void ChangeButtonSprite(int index)
        {
            _buttons[index].sprite = _sprite;
            var text = _buttons[index].gameObject.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
            {
                text.gameObject.SetActive(false);
            }

            PlayerPrefs.SetString($"skin_{index}", StringConstValues.Skin);
        }

        private void Load()
        {
            for (int i = 1; i < _skinSettings.Count; i++)
            {
                if (PlayerPrefs.HasKey($"skin_{i}"))
                {
                    ChangeButtonSprite(i);
                }
            }
        }
    }
}