using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EasterEggBehaviour : MonoBehaviour
{
    private bool isInGroup = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("EasterEgg") || other.CompareTag("Player")) && !isInGroup)
        {
            isInGroup = true;
            GeneralController.Instantiate().AddEasterEgg(gameObject);
        }
        
        else if (other.CompareTag("Painter"))
        {
            GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(other.GetComponent<Painter>().materialName);
        }
    }

    public bool IsInGroup()
    {
        return isInGroup;
    }
    
    public void SetIsInGroup(bool isInGroup)
    {
        this.isInGroup = isInGroup;
    }
}
