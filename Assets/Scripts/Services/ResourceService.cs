using UnityEngine;

namespace Services
{
    public static class ResourceService
    {
        public static Material GetEggMaterial(string materialName)
        {
            return Resources.Load<Material>($"Materials/{materialName}");
        }
    }
}