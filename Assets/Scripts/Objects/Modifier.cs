using System;
using System.Collections;
using System.Collections.Generic;
using EasterEgg;
using Services;
using UnityEngine;

namespace DefaultNamespace
{
    public class Modifier : MonoBehaviour
    {
        [SerializeField] Chocolate chocolate;
        [SerializeField] Pattern pattern;

        public Chocolate ChocolateType
        {
            get
            {
                return chocolate;
            }
            set
            {
                chocolate = value;
            }
        }
        
        public Pattern PatternType
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("EasterEgg"))
            {
                ChangeMaterial(other);
            }
        }

        virtual protected void ChangeMaterial(Collider other)
        {
            other.GetComponent<EasterEggBehaviour>().ChangeMaterial(PatternType, ChocolateType);
        }
    }
}
