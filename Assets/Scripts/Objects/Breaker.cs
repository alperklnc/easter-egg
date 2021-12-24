using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace EasterEgg
{
    public class Breaker : MonoBehaviour
    {
        private EggStackManager eggStackManager;
        private bool isFirstCollision = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EasterEgg"))
            {
                if (other.GetComponent<EasterEggBehaviour>().IsInGroup && isFirstCollision)
                {
                    isFirstCollision = false;
                    eggStackManager = EggStackManager.Instance;
                    List<GameObject> list = GetListOfSpreadedEggs(other.gameObject);
                    SpreadEggs(list);
                }
            }
        }

        private List<GameObject> GetListOfSpreadedEggs(GameObject egg)
        {
            return eggStackManager.GetListFromCurrentEgg(egg);
        }

        private void SpreadEggs(List<GameObject> list)
        {
            if (list!=null)
            {
                foreach(GameObject egg in list)
                {
                    egg.GetComponent<Rigidbody>().isKinematic = true;
                    egg.GetComponent<EasterEggBehaviour>().IsInGroup = false;
                }
                int ind = eggStackManager.GetIndexOfEgg(list[0]);
                eggStackManager.RemovePartOfList(ind, list.Count);

                eggStackManager.RandomInitializeEggs(list);
            }
        }
    }
}
