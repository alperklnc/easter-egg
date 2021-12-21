using System;
using UnityEngine;

namespace Objects
{
    public class FloorModifier : ChocolateModifier
    {
        [SerializeField] private ParticleSystem waveParticle;

        private void Awake()
        {
            var wavePS = waveParticle.main;
            wavePS.startColor = material.color;
        }

        protected override void ChangeMaterial(Collider other)
        {
            base.ChangeMaterial(other);
            waveParticle.Play();
        }
    }
}