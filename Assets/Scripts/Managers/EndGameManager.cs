using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EndGameManager :MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float timeBetweenEggs=0.75f;
    [SerializeField]
    private float speed = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Automate()
    {
        //GameManager.Instance.state = GameState.EndGame;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<InputService>().enabled = false;
        Vector3 current = player.transform.position;
        Vector3 newPos = new Vector3(0, current.y, current.z);

        List<GameObject> eggList = EggStackManager.Instance.GetEggList();
        ConfigureEggs(eggList);
        StartCoroutine(MoveObject(player,current, newPos, duration));
        StartCoroutine(Perform());
    }

    private void ConfigureEggs(List<GameObject> eggList)
    {
        foreach(GameObject egg in eggList)
        {
            egg.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private IEnumerator Perform()
    {
        while (true)
        {
            List<GameObject> eggList = EggStackManager.Instance.GetEggList();
            GameObject removedEgg = EggStackManager.Instance.RemoveFirstEasterEgg();
            GameObject currentEgg = removedEgg;
            if (removedEgg == null)
            {
                GameManager.Instance.state = GameState.EndGame;
                break;
            }
                
            for (int i = 0; i < eggList.Count; i++)
            {
                eggList[i].transform.position = currentEgg.transform.position;
                currentEgg = eggList[i];
            }
            bool isLeft = eggList.Count % 2 == 0;
            Vector3 removedEggPosition = removedEgg.transform.position;
            Vector3 nextPosition = new Vector3(isLeft ? -1.5f : 1.5f, removedEggPosition.y, removedEggPosition.z + 3);
            StartCoroutine(MoveObject(removedEgg,removedEggPosition,nextPosition,duration));
            yield return new WaitForSeconds(timeBetweenEggs);
        }
        yield return new WaitForEndOfFrame();
    }


    //float speed = 3f;
    float duration = 2f;

    
    private IEnumerator MoveObject(GameObject obj, Vector3 from, Vector3 to, float time)
    {
        float timeElapsed = 0f;
        float rate = (1f / time) * speed;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rate;

            Vector3 nextPos = Vector3.Lerp(from, to, timeElapsed);
            obj.transform.position = GetNextPosition(obj, nextPos);
            yield return null;
        }

        yield return new WaitForSeconds(1);
    }

    private Vector3 GetNextPosition(GameObject obj,Vector3 nextPos)
    {
        if(obj.CompareTag("Player"))
            return new Vector3(nextPos.x, obj.transform.position.y, obj.transform.position.z);
        return new Vector3(nextPos.x, obj.transform.position.y, nextPos.z);
    }
}
