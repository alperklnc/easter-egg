using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EasterEggBehaviour : MonoBehaviour
{
    public bool IsInGroup { get; set; } = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("EasterEgg") || other.CompareTag("Player")) && !IsInGroup)
        {
            IsInGroup = true;
            GeneralController.Instantiate().AddEasterEgg(gameObject);
        }
        
        else if (other.CompareTag("Painter"))
        {
            GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(other.GetComponent<Painter>().materialName);
        }
    }
}
