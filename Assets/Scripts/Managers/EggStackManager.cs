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
        [SerializeField] private GameObject additionText;
        [SerializeField] private float textZOffset=3f;


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
            easterEgg.GetComponent<Rigidbody>().isKinematic = false;
            easterEggMovement.SmoothTime = baseSmoothness;
            easterEggMovement.Distance = easterEggDistance;
            if (tail == null) tail = player;
            
            eggList.Add(easterEgg);
            easterEggMovement.EggIndex = eggList.Count - 1;
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
            UIAdd();
        }

        private void UIAdd()
        {
            Vector3 currentPos = player.transform.position;
            Vector3 nextPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + textZOffset);
            GameObject instantiatedText = Instantiate(additionText, nextPos, Quaternion.identity);
            Destroy(instantiatedText, 1f);
        }

        public int GetIndexOfEgg(GameObject egg)
        {
            return eggList.IndexOf(egg);
        }

        public List<GameObject> GetListFromCurrentEgg(GameObject egg)
        {
            int ind = GetIndexOfEgg(egg);
            if(ind!=-1)
                return eggList.GetRange(ind,eggList.Count-ind);
            return null;
        }

        public void RemovePartOfList(int start,int count)
        {
            if (start > 0)
                tail = eggList[start - 1];
            else
                tail = player;
            eggList.RemoveRange(start, count);
        }

        public void RandomInitializeEggs(List<GameObject> list)
        {
            foreach (GameObject egg in list)
            {
                Vector3 nextPos = FindAppropriatePoosition(egg);
                egg.GetComponent<EasterEggBehaviour>().StartThrowingAnimation(nextPos);
            }
        }

        public Vector3 FindAppropriatePoosition(GameObject obj)
        {
            Vector3 nextPos=Vector3.zero;
            Vector3 currentPlayerPos = player.transform.position;
            bool isCollided =true;
            int count= 0;
            for (; count <= 100 && isCollided; count++)
            {
                Vector3 currentPos = obj.transform.position;
                float zPos = Random.Range(10f, 15f);
                zPos += currentPlayerPos.z;
                float xPos = Random.Range(-2f, 2f);
                nextPos = new Vector3(xPos, currentPos.y + 0.1f, zPos);
                isCollided = Physics.CheckBox(nextPos, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, LayerMask.GetMask("Objects"));
            }
            return nextPos;
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

        public void ClearEggList()
        {
            eggList.Clear();
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
            if (GameManager.Instance.IsPlaying())
            {
                MoveEggs();

                player.GetComponent<PlayerController>().PushAnimation(eggList.Count > 0);
            }
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

        private IEnumerator AnimateEasterEggs(List<GameObject> temp)
        {

            for(int i = temp.Count - 1; i >= 0; i--)
            {
                GameObject egg = temp[i];
                EasterEggBehaviour easterEggBehaviour = egg.GetComponent<EasterEggBehaviour>();
                easterEggBehaviour.StartResizingAnimation(1);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForEndOfFrame();
        }
        #endregion
    }
}
