using UnityEngine;

namespace DefaultNamespace
{
    public static class ResourceService
    {
        public static Material GetEggMaterial(string materialName)
        {
            return Resources.Load<Material>($"Materials/Egg/{materialName}");
        }
    }
}