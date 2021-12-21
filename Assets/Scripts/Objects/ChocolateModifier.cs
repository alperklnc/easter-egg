using System;
using DefaultNamespace;
using UnityEngine;

namespace Objects
{
    public class ChocolateModifier : Modifier
    {
        [SerializeField] protected ParticleSystem splashParticle;

        protected Material material;

        private void Awake()
        {
            InitSplashParticle();
        }

        protected void InitSplashParticle()
        {
            material = GetComponent<MeshRenderer>().material;
            var splashPS = splashParticle.main;
            splashPS.startColor = material.color;
        }

        protected override void ChangeMaterial(Collider other)
        {
            base.ChangeMaterial(other);
            splashParticle.Play();
        }
    }
}