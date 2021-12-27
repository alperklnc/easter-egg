using Managers;
using EasterEgg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> myDict=null;
    private List<GameObject> eggList=null;
    [SerializeField] float cameraYPos;
    [SerializeField] float cameraXRot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            camera.GetComponent<CameraController>().StartRelocating(GetNewPos(camera,cameraYPos),GetNewRot(camera,cameraXRot));
            
            GameManager.Instance.state = GameState.EndGame;
            EggStackManager eggStackManager = EggStackManager.Instance;
            eggList=eggStackManager.GetEggList();
            if (eggList.Count > 0)
            {
                eggStackManager.ClearEggList();
                float mult = 1;
                GameObject firstEgg = eggList[0];
                Vector3 firstEggPos = firstEgg.transform.position;
                myDict = new Dictionary<GameObject, Vector3>();
                for (int i = 0; i < eggList.Count; i++)
                {
                    GameObject currentEgg = eggList[i];
                    EasterEggBehaviour easterEggBehaviour = currentEgg.GetComponent<EasterEggBehaviour>();
                    Vector3 newPos = new Vector3(firstEggPos.x, firstEggPos.y, firstEggPos.z + i * mult);
                    easterEggBehaviour.IsInGroup = false;
                    myDict.Add(currentEgg, newPos);
                }
            }
        }
    }

    private Vector3 GetNewPos(GameObject obj,float offset)
    {
        Vector3 current = obj.transform.position;
        return new Vector3(0,offset,current.z);
    }

    private Vector3 GetNewRot(GameObject obj, float offset)
    {
        Vector3 current = obj.transform.eulerAngles;
        obj.transform.eulerAngles = new Vector3(current.x, 0, current.z);
        return new Vector3(offset, 0, current.z);
    }

    private void Update()
    {
        if (eggList != null)
        {
            if (eggList.Count > 0)
            {
                foreach (GameObject egg in eggList)
                {
                    EasterEggMovement easterEggMovement = egg.GetComponent<EasterEggMovement>();
                    easterEggMovement.MoveToward(myDict[egg]);
                }

                eggList.RemoveAll(egg => Mathf.Abs(Vector3.Distance(egg.transform.position, myDict[egg]))<=0.01f);
            }
        }
    }
}
