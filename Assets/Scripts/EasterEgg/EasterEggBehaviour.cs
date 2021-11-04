using System;
using DefaultNamespace;
using Managers;
using Services;
using UnityEngine;

namespace EasterEgg
{
    public class EasterEggBehaviour : MonoBehaviour
    {
        public bool IsInGroup { get; set; } = false;
    
        private void OnTriggerEnter(Collider other)
        {
            if((other.CompareTag("EasterEgg") || other.CompareTag("Player")) && !IsInGroup)
            {
                IsInGroup = true;
                EggStackManager.Instance.AddEasterEgg(gameObject);
            }
            else if (other.CompareTag("Breaker"))
            {
                EggStackManager.Instance.RemoveEasterEgg(gameObject);
            }
            else if (other.CompareTag("Painter"))
            {
                GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(other.GetComponent<Painter>().materialName);
            }
        }

        private void OnDestroy()
        {
            Debug.Log("Destroyed");
        }
    }
}
