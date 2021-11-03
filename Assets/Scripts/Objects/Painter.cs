using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Painter : MonoBehaviour
    {
        public EggTexture eggTexture;

        public string materialName;

        private void Awake()
        {
            materialName = eggTexture.ToString();
        }
    }
}