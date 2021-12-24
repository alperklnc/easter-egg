using System.Collections;
using System.Collections.Generic;
using EasterEgg;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private ParticleSystem starParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EasterEgg"))
        {
            other.GetComponent<EasterEggBehaviour>().ActivateRibbon();
            
            var starParticlePosition = starParticle.transform.position;
            starParticlePosition.x = other.transform.position.x;
            starParticle.transform.position = starParticlePosition;

            starParticle.Play();
        }
    }
    
    private void ChangeMaterial(Collider other)
    {
        var starParticlePosition = starParticle.transform.position;
        starParticlePosition.x = other.transform.position.x;
        starParticle.transform.position = starParticlePosition;

        starParticle.Play();
    }
}
