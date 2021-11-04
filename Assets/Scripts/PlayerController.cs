using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;
        
        private CharacterController characterController;
        private float offset = 0.01f;
        
        Animator animator;

        [SerializeField] float horizontalSpeed = 2f;
        [SerializeField] float verticalSpeed = 3f;
        

        [SerializeField] float range = 2f;

        Vector3 moveDirection = Vector3.zero;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            characterController.Move(moveDirection * Time.deltaTime);
            moveDirection = Vector3.zero;
            
            EggStackManager.Instance.MoveEggs();
        }

        public void Move(float horizontalInput) {
            if(horizontalInput != 0 && GameManager.Instance.IsPlaying())
            {
                var position = transform.position;

                if(position.x > range)
                {
                    position.x = range;
                }
                if(position.x < -range) {
                    position.x = -range;
                }

                transform.position = position;

                moveDirection = new Vector3(horizontalInput * verticalSpeed, 0, horizontalSpeed);
                
                //transform.Translate(horizontalInput * verticalSpeed * Time.deltaTime, 0, horizontalSpeed * Time.deltaTime, Space.World);
                //transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
            }
        }
    }
}