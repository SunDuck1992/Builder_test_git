using System.Collections.Generic;
using UI;
using UnityEngine;
using ConstValues;

namespace PlayerSystem
{
    public class PlayerSkins : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _skins;
        [SerializeField] private Shop _shop;
        [SerializeField] private PlayerMovement _movement;

        private void OnChangeSkin(GameObject skin)
        {
            _skins.ForEach(x => x.SetActive(false));
            var activeSkin = _skins.Find(x => x.name.Equals(skin.name));
            activeSkin.SetActive(true);
            _movement.Animator = activeSkin.GetComponent<Animator>();
            PlayerPrefs.SetInt(StringConstValues.Skin, _skins.IndexOf(activeSkin));
        }

        private void Start()
        {
            _shop = FindObjectOfType<Shop>();
            _shop.ChangedSkin += OnChangeSkin;
            _skins[PlayerPrefs.GetInt(StringConstValues.Skin, 0)].SetActive(true);
            _movement.Animator = _skins[PlayerPrefs.GetInt(StringConstValues.Skin, 0)].GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            _shop.ChangedSkin -= OnChangeSkin;
        }
    }
}


