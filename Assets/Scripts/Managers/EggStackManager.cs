using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggStackManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField] GameObject player;
    
    [SerializeField] float baseDistance = 0.6f;
    [SerializeField] float baseSmoothness = 20f;
    
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
        easterEggMovement.SmoothTime = baseSmoothness * (eggList.Count + 1);
        
        if (tail == null) tail = player;

        eggList.Add(easterEgg);
        easterEggMovement.Distance = baseDistance * eggList.Count;
        easterEgg.transform.position = new Vector3(tail.transform.position.x, easterEgg.transform.position.y, player.transform.position.z + baseDistance * eggList.Count);
        tail = easterEgg;
    }

    public void RemoveEasterEgg(GameObject easterEgg)
    {
        if(!eggList.Remove(easterEgg))
            Debug.Log("Couldn't remove " + easterEgg);
    }

    #endregion

    #region Stack Movement

    private void Update()
    {
        MoveEggs();
    }

    private void MoveEggs()
    {
        foreach(GameObject egg in eggList)
        {
            EasterEggMovement easterEggMovement = egg.GetComponent<EasterEggMovement>();
            easterEggMovement.Movement(player.transform.position);
        } 
    }

    #endregion
}
