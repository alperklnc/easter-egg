using System;
using UnityEngine;

namespace Objects
{
    public class FloorModifier : ChocolateModifier
    {
        [SerializeField] private ParticleSystem waveParticle;

        private void Awake()
        {
            base.InitSplashParticle();
            var wavePS = waveParticle.main;
            wavePS.startColor = material.color;
        }

        protected override void ChangeMaterial(Collider other)
        {
            base.ChangeMaterial(other);
            
            var wavePosition = waveParticle.transform.position;
            wavePosition.x = other.transform.position.x;
            waveParticle.transform.position = wavePosition;

            var splashPosition = splashParticle.transform.position;
            splashPosition.x = other.transform.position.x;
            splashParticle.transform.position = splashPosition;
            
            waveParticle.Play();
        }
    }
}