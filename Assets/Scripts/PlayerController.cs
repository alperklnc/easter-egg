using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;
        private Animator animator;

        [SerializeField] float horizontalSpeed = 2f;
        [SerializeField] float verticalSpeed = 3f;

        [SerializeField] float range = 2f;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetBool("IsPlaying", GameManager.Instance.IsPlaying());
            if (GameManager.Instance.IsPlaying())
            {
                transform.Translate(0, 0, horizontalSpeed * Time.deltaTime);
            }
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

                transform.Translate(horizontalInput * verticalSpeed * Time.deltaTime, 0, 0);
            }
        }

        public void Push(bool hasEgg)
        {
            animator.SetBool("HasEgg", hasEgg);
        }
    }
}