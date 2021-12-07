using System.Collections.Generic;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using EasterEgg;

namespace Managers
{
    public class EggStackManager : MonoBehaviour
    {
        #region Variables
    
        [SerializeField] GameObject player;
        
        [SerializeField] float easterEggDistance = 0.6f;
        [SerializeField] float baseSmoothness = 20f;
        private float additionAnimationDelay=100;
        [SerializeField] private float eggVerticalRotationSpeed;
    
        private GameObject tail;
        private static List<GameObject> eggList;
    
        #endregion
    
        #region Singleton

        public static EggStackManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                eggList = new List<GameObject>();
                Instance = this;
            }
        }

        #endregion

        #region Stack Adjustment
        float lastAddedTime = 0;

        public void AddEasterEgg(GameObject easterEgg)
        {
            EasterEggMovement easterEggMovement = easterEgg.AddComponent<EasterEggMovement>();
            easterEggMovement.SmoothTime = baseSmoothness;
            easterEggMovement.Distance = easterEggDistance;
            
            if (tail == null) tail = player;

            eggList.Add(easterEgg);
            easterEggMovement.EggIndex = eggList.IndexOf(easterEgg);
            easterEgg.transform.position = new Vector3(tail.transform.position.x, easterEgg.transform.position.y, player.transform.position.z + easterEggDistance * eggList.Count);
            easterEgg.transform.rotation = Quaternion.Euler(0, -90, 90);
            tail = easterEgg;
            //StopAllCoroutines();
            float currentTime = Time.time * 1000;
            float diff = currentTime - lastAddedTime;
            lastAddedTime = currentTime;
            if(diff >= additionAnimationDelay)
            {
                List<GameObject> copyList = new List<GameObject>(eggList);
                StartCoroutine(AnimateEasterEggs(copyList));
            }            
        }

        public void RemoveEasterEgg(GameObject easterEgg)
        {
            if (eggList.Remove(easterEgg))
            {
                Destroy(easterEgg);
            }
            else
            {
                Debug.Log("Couldn't remove " + easterEgg);
            }
        }

        public GameObject RemoveFirstEasterEgg()
        {
            if (eggList.Count > 0)
            {
                GameObject egg = eggList[0];
                eggList.RemoveAt(0);
                return egg;
            }
            return null;
        }

        public List<GameObject> GetEggList()
        {
            List<GameObject> copyList = new List<GameObject>(eggList);
            return copyList;
        }

        #endregion

        #region Stack Movement

        private void Update()
        {
            MoveEggs();
            
            player.GetComponent<PlayerController>().PushAnimation(eggList.Count > 0);
        }

        // Potential Listener-Oberver
        private void MoveEggs()
        {
            foreach(GameObject egg in eggList)
            {
                EasterEggMovement easterEggMovement = egg.GetComponent<EasterEggMovement>();
                easterEggMovement.Movement(player.transform.position, eggVerticalRotationSpeed);
                easterEggMovement.EggIndex = eggList.IndexOf(egg);
            } 
        }

        private IEnumerator AnimateEasterEggs(List<GameObject> temp)
        {

            for(int i = temp.Count - 1; i >= 0; i--)
            {
                GameObject egg = temp[i];
                EasterEggBehaviour easterEggBehaviour = egg.GetComponent<EasterEggBehaviour>();
                easterEggBehaviour.AddingAnimation();
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForEndOfFrame();
        }
        #endregion
    }
}
