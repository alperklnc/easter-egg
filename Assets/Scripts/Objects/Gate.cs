using System.Collections;
using System.Collections.Generic;
using EasterEgg;
using UnityEngine;

public class Gate : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EasterEgg"))
        {
            other.GetComponent<EasterEggBehaviour>().ActivateRibbon();
        }
    }
}
