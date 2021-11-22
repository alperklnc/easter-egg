using System.Collections;
using System.Collections.Generic;
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
    }
}
