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
        
        private void Start()
        {
            //animator = GetComponentInChildren<Animator>();

            //LevelManager.OnLevelLoad += OnLevelLoad;
        }

        /*
        private void OnLevelLoad(Level level) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            tailTarget.position = new Vector3(-0.14f, 0.73f, 0);
        
            foxAnimator.SetTrigger("Reset");
            StopAllCoroutines();
        }
        */
        private void Update() {

        }

        public void Move(float horizontalInput) {
            if(horizontalInput != 0 && GameManager.Instance.IsPlaying()) {
                if(transform.position.x > range) {
                    transform.position = new Vector3(range, transform.position.y, transform.position.z);
                }
                if(transform.position.x < -range) {
                    transform.position = new Vector3(-range, transform.position.y, transform.position.z);
                }
                
                transform.Translate(horizontalInput * verticalSpeed * Time.deltaTime, 0, horizontalSpeed * Time.deltaTime, Space.World);
                transform.Rotate(new Vector3(rotationSpeed,0, 0) * Time.deltaTime);
                //transform.RotateAround(transform.position, transform.right, Time.deltaTime * rotationSpeed);
                //animator.SetFloat("Speed", 1);
            } else {
                //animator.SetFloat("Speed", 0);
            }
        }
    }
}