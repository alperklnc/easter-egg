using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    [SerializeField] VariableJoystick variableJoystick;

    [SerializeField] PlayerController playerController;

    void Update() {
        //float horizontal = variableJoystick.Horizontal;

        float horizontal = Input.GetAxis("Horizontal");
        
        playerController.Move(horizontal);
    }
}
