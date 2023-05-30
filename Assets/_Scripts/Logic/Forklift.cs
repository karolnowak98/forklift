using UnityEngine;
using UnityEngine.InputSystem;
using Data;

namespace Logic
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField] 
        private ForkliftConfig _config;

        public void Drive(InputAction.CallbackContext context)
        {
            Debug.Log("Drive");
        }
        
        public void LiftUp(InputAction.CallbackContext context)
        {
            Debug.Log("Lifting up...");
        }
        
        public void Lower(InputAction.CallbackContext context)
        {
            Debug.Log("Lowering...");
        }
    }
}