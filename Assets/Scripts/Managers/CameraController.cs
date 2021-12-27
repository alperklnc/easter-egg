using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float smoothSpeed = 0.125f;

    [SerializeField] Vector3 offset;
    
    private void Update()
    {
        if (GameManager.Instance.IsPlaying())
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    float rotationSpeed = 0.5f;
    float rotationDuration = 4f;

    public void StartRelocating(Vector3 newPos,Vector3 newRot)
    {
        StartCoroutine(Relocate(newPos, newRot, rotationDuration));
    }

    private IEnumerator Relocate(Vector3 toPos, Vector3 toRot, float time)
    {
        float timeElapsed = 0f;
        float rate = (1f / time) * rotationSpeed;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rate;

            Vector3 nextPos = Vector3.Lerp(transform.position, toPos, timeElapsed);
            Vector3 nextRot = Vector3.Lerp(transform.eulerAngles, toRot, timeElapsed);
            gameObject.transform.position = nextPos;
            gameObject.transform.eulerAngles = nextRot;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }
}
