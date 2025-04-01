using System.Collections.Generic;
using UnityEngine;

namespace HouseSystem
{
    public class EffectsActivator : MonoBehaviour
    {
        [SerializeField] private House _house;
        [SerializeField] private List<ParticleSystem> _particles;

        private void OnEnable()
        {
            _house.CompletedHouse += OnActivate;
        }

        private void OnDisable()
        {
            _house.CompletedHouse -= OnActivate;
        }

        private void OnActivate()
        {
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
    }
}