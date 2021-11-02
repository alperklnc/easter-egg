using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject ground;
    [SerializeField] GameObject finish;

    [Header("Prefabs")]
    [SerializeField] GameObject groundPrefab;
    [SerializeField] GameObject finishPrefab;

    List<GameObject> list = new List<GameObject>();

    public void RecreatePlatform(int length) {
        foreach(GameObject item in list) {
            Destroy(item);
        }
        CreatePlatform(length);
    }

    public void CreatePlatform(int length) {
        // Create Ground
        for(int z = -2; z < length; z+=2) {
            Vector3 pos = new Vector3(0, 0, z);
            GameObject ground_ = Instantiate(groundPrefab, pos, Quaternion.identity);
            ground_.transform.SetParent(ground.transform);

            list.Add(ground_);
        }

        /*
        // Create Finish Line
        Vector3 position = new Vector3(length + 0.75f, -1.5f, 0);
        GameObject finish_ = Instantiate(finishPrefab, position, Quaternion.Euler(-90, 180, 0));
        finish_.transform.SetParent(finish.transform);

        list.Add(finish_);
        */
    }
}
