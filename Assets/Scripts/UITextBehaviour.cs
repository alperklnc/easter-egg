using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextBehaviour : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*Time.deltaTime);
        if (transform.position.y >= 3f)
        {
            Destroy(gameObject);
        }
    }
}
