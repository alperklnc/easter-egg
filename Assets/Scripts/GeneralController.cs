using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController
{
    // Singleton Haline Getirilecek
    //private static Stack<GameObject> easterEggList;
    private GameObject tail=null;
    private GameObject player;
    private float baseDistance = 0.6f;
    private float baseSmoothness = 20f;
    private static List<GameObject> eggList;
   
    private static GeneralController controller;

    private GeneralController() { }

    public static GeneralController Instantiate()
    {
        if (controller == null)
        {
            eggList = new List<GameObject>();
            controller = new GeneralController();
        }
            
        return controller;
    }  

    public GameObject GetPlayer()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }

    public void AddEasterEgg(GameObject easterEgg)
    {
        EasterEggMovement easterEggMovement = easterEgg.AddComponent<EasterEggMovement>();
        easterEggMovement.SetSmoothness(baseSmoothness * (eggList.Count + 1));
        
        if (tail == null)
        {
            tail = GetPlayer();
        }

        eggList.Add(easterEgg);
        easterEggMovement.SetFollowDistance(baseDistance * eggList.Count);
        easterEgg.transform.position = new Vector3(tail.transform.position.x, easterEgg.transform.position.y, player.transform.position.z + baseDistance * eggList.Count);
        tail = easterEgg;
    }

    public void MoveEggs()
    {
        foreach(GameObject egg in eggList)
        {
            EasterEggMovement easterEggMovement = egg.GetComponent<EasterEggMovement>();
            easterEggMovement.Movement(player.transform.position);
        } 
    }
    
}
