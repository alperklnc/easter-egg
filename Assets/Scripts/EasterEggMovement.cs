using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggMovement : MonoBehaviour
{
    float distance = 1f;
    float rotationSpeed = 3f;
    
    private Vector3 targetPosition = Vector3.zero;
    private float smoothTime = 0.3f;

    private float xVelocity = 0f;

    public Vector3 TargetPosition { get; set; }

    public void Movement(Vector3 position)
    {
        targetPosition.Set(position.x, transform.position.y, position.z + distance);
        float newX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref xVelocity, smoothTime * Time.deltaTime);
        transform.position = new Vector3(newX, targetPosition.y, targetPosition.z);
        
        transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
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
