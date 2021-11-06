using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggMovement : MonoBehaviour
{
    public float Distance { get; set; } = 0.5f;
    public float SmoothTime { get; set; } = 3f;
    
    private Vector3 targetPosition = Vector3.zero;
    
    private float xVelocity = 0f;
    
    public void Movement(Vector3 position, float rotationSpeed, int eggIndex)
    {
        float newZ = position.z + Distance;
        // First egg biraz daha önde olabilir
        // if (isFirstEgg) newZ += Distance;
        targetPosition.Set(position.x, transform.position.y, newZ);

        float newX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref xVelocity, SmoothTime * eggIndex * Time.deltaTime);
        transform.position = new Vector3(newX, targetPosition.y, targetPosition.z);
        
        transform.Rotate(new Vector3(rotationSpeed,0, 0), Space.World);
    }
}