using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace EasterEgg
{
    public class Breaker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EasterEgg"))
            {
                
            }
        }
    }
}
