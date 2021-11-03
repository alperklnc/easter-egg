using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Washer", menuName = "Washer")]
    public class WasherObjectData : ScriptableObject
    {
        public EggTexture eggTexture;
    }
}