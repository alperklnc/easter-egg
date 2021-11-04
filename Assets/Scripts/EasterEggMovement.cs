using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggMovement : MonoBehaviour
{
    float speedMult = 1f;
    float distance = 1f;
    
    private Vector3 targetPosition = Vector3.zero;
    private float smoothTime = 40f;
    private Vector3 velocity = Vector3.zero;
    
    public void Movement(Vector3 position)
    {
        targetPosition.Set(position.x, 0.5f, position.z + distance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.deltaTime);
    }
 
    public void SetSpeedMult(float speedMult)
    {
        this.speedMult = speedMult;
    }
    public void SetFollowDistance(float distance)
    {
        this.distance = distance;
    }
    public void SetSmoothness(float smoothness)
    {
        smoothTime = smoothness;
    }
}
