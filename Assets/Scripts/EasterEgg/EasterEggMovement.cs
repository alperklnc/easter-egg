using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggMovement : MonoBehaviour
{
    public float Distance { get; set; } = 0.5f;
    public float SmoothTime { get; set; } = 0.3f;
    
    float rotationSpeed = 3f;
    
    private Vector3 targetPosition = Vector3.zero;
    
    private float xVelocity = 0f;
    
    public void Movement(Vector3 position)
    {
        targetPosition.Set(position.x, transform.position.y, position.z + Distance);
        float newX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref xVelocity, SmoothTime * Time.deltaTime);
        transform.position = new Vector3(newX, targetPosition.y, targetPosition.z);
        
        // TODO rotate when only moves
        transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
    }
}
