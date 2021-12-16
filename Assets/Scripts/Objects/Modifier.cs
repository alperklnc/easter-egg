using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

namespace DefaultNamespace
{
    public class Modifier : MonoBehaviour
    {
        public EggTexture eggTexture;

        public string materialName;

        private void Awake()
        {
            materialName = eggTexture.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EasterEgg"))
            {
                //TODO refoctor
                other.GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(materialName);
            }
        }
    }
}
