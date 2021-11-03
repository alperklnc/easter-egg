using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        Animator animator;

        [SerializeField] float horizontalSpeed = 2f;
        [SerializeField] float verticalSpeed = 3f;
        [SerializeField] float rotationSpeed = 3f;

        [SerializeField] float range = 2f;
        
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
                
                transform.Translate(horizontalInput * verticalSpeed * Time.deltaTime, 0, horizontalSpeed * Time.deltaTime, Space.World);
                transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cleaner"))
            {
                GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial("Default");
            }
            
            else if (other.CompareTag("Painter"))
            {
                GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(other.GetComponent<Painter>().materialName);
            }
        }
    }
}