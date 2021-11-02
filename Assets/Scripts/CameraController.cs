using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float smoothSpeed = 0.125f;

    [SerializeField] Vector3 offset;

    private void Start() {

    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying())
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
