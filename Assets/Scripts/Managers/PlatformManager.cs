using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject groundParent;
    [SerializeField] GameObject finishParent;

    [Header("Prefabs")]
    [SerializeField] GameObject groundPrefab;
    [SerializeField] GameObject finishPrefab;

    [Space]
    [SerializeField] private int offset;
    [SerializeField] private int prefabLength;
    
    List<GameObject> list = new List<GameObject>();

    public void RecreatePlatform(int length) {
        foreach(GameObject item in list) {
            Destroy(item);
        }
        CreatePlatform(length);
    }

    public void CreatePlatform(int length) {
        // Create Ground
        for(int z = -offset; z < length; z+=prefabLength) {
            Vector3 pos = new Vector3(0, 0, z);
            GameObject ground = Instantiate(groundPrefab, pos, Quaternion.identity);
            ground.transform.SetParent(groundParent.transform);

            list.Add(ground);
        }
        
        // Create Finish Line
        Vector3 position = new Vector3(0, 0, length + prefabLength / 2 + 1);
        GameObject finish = Instantiate(finishPrefab, position, Quaternion.identity);
        finish.transform.SetParent(finishParent.transform);

        list.Add(finish);
    }
}
