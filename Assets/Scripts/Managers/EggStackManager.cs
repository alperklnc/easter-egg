using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Managers
{
    public class EggStackManager : MonoBehaviour
    {
        #region Variables
    
        [SerializeField] GameObject player;
        
        [SerializeField] float easterEggDistance = 0.6f;
        [SerializeField] float baseSmoothness = 20f;
        
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

        #endregion

        #region Stack Movement

        private void Update()
        {
            MoveEggs();
            
            player.GetComponent<PlayerController>().PushAnimation(eggList.Count > 0);
        }

        private void MoveEggs()
        {
            foreach(GameObject egg in eggList)
            {
                EasterEggMovement easterEggMovement = egg.GetComponent<EasterEggMovement>();
                easterEggMovement.Movement(player.transform.position, eggVerticalRotationSpeed);
                easterEggMovement.EggIndex = eggList.IndexOf(egg);
            } 
        }

        #endregion
    }
}
