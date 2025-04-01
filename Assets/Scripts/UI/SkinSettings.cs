using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class SkinSettings
    {
        [SerializeField] private int _cost;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _isAds;

        public int Cost => _cost;
        public GameObject GameObject => _gameObject;
        public bool IsAds => _isAds;
    }
}