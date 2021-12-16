using System.Collections;
using System.Collections.Generic;
using EasterEgg;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EasterEgg"))
        {
            other.GetComponent<EasterEggBehaviour>().ActivateRibbon();
        }
    }
}
