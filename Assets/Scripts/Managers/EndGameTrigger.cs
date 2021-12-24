using Managers;
using EasterEgg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> myDict=null;
    private List<GameObject> eggList=null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.state = GameState.EndGame;
            EggStackManager eggStackManager = EggStackManager.Instance;
            eggList=eggStackManager.GetEggList();
            eggStackManager.ClearEggList();
            float mult = 1;
            GameObject firstEgg = eggList[0];
            Vector3 firstEggPos = firstEgg.transform.position;
            myDict = new Dictionary<GameObject, Vector3>();
            for (int i = 0; i < eggList.Count; i++)
            {
                GameObject currentEgg = eggList[i];
                EasterEggBehaviour easterEggBehaviour = currentEgg.GetComponent<EasterEggBehaviour>();
                Vector3 newPos = new Vector3(firstEggPos.x, firstEggPos.y, firstEggPos.z + i*mult);
                easterEggBehaviour.IsInGroup = false;
                myDict.Add(currentEgg, newPos);
            }
        }
    }

    private void Update()
    {
        if (eggList != null)
        {
            if (eggList.Count > 0)
            {
                //Debug.Log(eggList.Count);
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
