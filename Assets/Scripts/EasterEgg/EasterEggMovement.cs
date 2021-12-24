using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggMovement : MonoBehaviour
{
    public float Distance { get; set; } = 0.5f;
    public float SmoothTime { get; set; } = 3f;
    
    public int EggIndex { get; set; }
    
    private Vector3 targetPosition = Vector3.zero;
    
    private float xVelocity = 0f;
    
    public void Movement(Vector3 position, float rotationSpeed)
    {
        float newZ = position.z + Distance * (EggIndex + 1);
        targetPosition.Set(position.x, transform.position.y, newZ);

        float smoothTime = SmoothTime * EggIndex * EggIndex * Time.deltaTime;
        float newX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref xVelocity, smoothTime);
        transform.position = new Vector3(newX, targetPosition.y, targetPosition.z);
        
        transform.Rotate(new Vector3(rotationSpeed,0, 0), Space.World);
    }


    public void MoveToward(Vector3 newPos)
    {
        float step = 5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        transform.Rotate(new Vector3(3f, 0, 0), Space.World);
    }
}