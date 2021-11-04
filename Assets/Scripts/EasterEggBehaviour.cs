using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EasterEggBehaviour : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;
    
    private bool isInGroup = false;

    private void Update()
    {
        if (isInGroup)
        {
            transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
        }
    }

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
