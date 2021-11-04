using DefaultNamespace;
using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] VariableJoystick variableJoystick;

        [SerializeField] PlayerController playerController;

        private bool firstTime = true;
        
        void Update() {
            //float horizontal = variableJoystick.Horizontal;
            float horizontal = Input.GetAxis("Horizontal");
            
            if (firstTime && horizontal != 0)
            {
                GameManager.Instance.StartPlaying();
            }
            
            playerController.Move(horizontal);
        }
    }
}
