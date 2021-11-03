using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(WasherObjectData))]
    public class WasherEditor : Editor
    {
        private WasherObjectData _washerObject;
        
        public int[] eggTextureTypeIDs = {0,1,2};
        
        public string[] eggTextureTypeNames = new string[]{
            "ChocolateBrown",
            "ChocolateLightPink",
            "ChocolateHeavyPink",
        };
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();
            int eggTexture = EditorGUILayout.IntPopup("Egg Texture", 0, eggTextureTypeNames, eggTextureTypeIDs);

            if (EditorGUI.EndChangeCheck())
            {
                Debug.Log(_washerObject);
                _washerObject.eggTexture = (EggTexture) eggTexture;
            }
        }
    }
}