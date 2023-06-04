using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private Transform[] _frontWheels;
        [SerializeField] private Transform[] _backWheels;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ForkliftConfig _config;
        [SerializeField] private Transform _forks;
        [SerializeField] private Transform _steeringWheel;
        
        private Vector2 _movementInput;
        private Controls _controls;
        private bool _isRaisingForks;
        private bool _isLoweringForks;

        private void Awake()
        {
            _rb.centerOfMass += Vector3.down * _config.CenterMassOffset;
            
            _controls = new Controls();
            _controls.Player.Drive.performed += OnMove;
            _controls.Player.Drive.canceled += OnMove;
            _controls.Player.LiftRaise.performed += OnForkliftRaise;
            _controls.Player.LiftRaise.canceled += OnForkliftRaise;
            _controls.Player.LiftLower.performed += OnForkliftLower;
            _controls.Player.LiftLower.canceled += OnForkliftLower;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void FixedUpdate()
        {
            ApplyMoveForce();
            ApplySteeringTorque();
            ApplyFriction();
            ApplyWheelRotating();
            
            if (_isRaisingForks)
            {
                RaiseForks();
            }

            if (_isLoweringForks)
            {
                LowerForks();
            }
        }

        
        private void ApplyMoveForce()
        {
            var movementForce = transform.forward * (_movementInput.y * _config.AcceleratingSpeed);
            _rb.AddForce(movementForce);
        }

        private void ApplySteeringTorque()
        {
            var steeringAngle = _movementInput.x * _config.MaxWheelTurnAngle;
            var steeringTorque = steeringAngle * _config.SteeringSpeed;
            _rb.AddTorque(Vector3.up * steeringTorque);
        }
        
        private void ApplyFriction()
        {
            var right = transform.right;
            var sidewaysVelocity = Vector3.Dot(_rb.velocity, right) * right;
            var frictionForce = -sidewaysVelocity * _config.FrictionCoefficient;
            _rb.AddForce(frictionForce);
        }

        private void ApplyWheelRotating()
        {
            var rotationAmount = _movementInput.x * -_config.MaxWheelTurnAngle;
            _steeringWheel.localRotation = Quaternion.Euler(0f, 0f, rotationAmount);
        }
        
        private void RaiseForks()
        {
            var newPosition = _forks.localPosition + Vector3.up * (_config.ForkLiftingSpeed * Time.deltaTime);
            newPosition.y = Mathf.Clamp(newPosition.y, _config.LiftMinMaxPositions.x, _config.LiftMinMaxPositions.y);
            _forks.localPosition = newPosition;
        }

        private void LowerForks()
        {
            var newPosition = _forks.localPosition - Vector3.up * (_config.ForkLiftingSpeed * Time.deltaTime);
            newPosition.y = Mathf.Clamp(newPosition.y, _config.LiftMinMaxPositions.x, _config.LiftMinMaxPositions.y);
            _forks.localPosition = newPosition;
        }
        
        private void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }
        
        private void OnForkliftRaise(InputAction.CallbackContext context)
        {
            _isRaisingForks = context.ReadValueAsButton();
        }

        private void OnForkliftLower(InputAction.CallbackContext context)
        {
            _isLoweringForks = context.ReadValueAsButton();
        }
    }
}