using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSplitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("EndGameController").GetComponent<EndGameManager>().Automate();
        }
    }
}
