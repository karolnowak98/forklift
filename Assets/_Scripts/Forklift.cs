using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

namespace Forklift
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField] private ForkliftConfig _config;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform[] _frontWheels;
        [SerializeField] private Transform[] _backWheels;
        [SerializeField] private Transform _forks;
        [SerializeField] private Transform _steeringWheel;
        
        private Controls _controls;
        private Vector2 _movementInput;
        private float _rotationTimer;
        private float _currentSpeed;
        private float _targetSpeed;
        private float _fixedDeltaTime;
        private bool _isRaisingForks;
        private bool _isLoweringForks;

        private void Awake()
        {
            _rb.centerOfMass += _config.CenterMassOffset;
            _currentSpeed = 0f;
            _targetSpeed = 0f;
            
            _controls = new Controls();
            _controls.Player.Drive.performed += OnMove;
            _controls.Player.Drive.canceled += OnMove;
            _controls.Player.LiftRaise.performed += OnForkliftRaise;
            _controls.Player.LiftRaise.canceled += OnForkliftRaise;
            _controls.Player.LiftLower.performed += OnForkliftLower;
            _controls.Player.LiftLower.canceled += OnForkliftLower;
        }

        private void Start()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
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
            
            if (_isRaisingForks)
            {
                RaiseForks();
            }

            if (_isLoweringForks)
            {
                LowerForks();
            }
        }

        private void Update()
        {
            ApplyWheelRotating();
        }

        private void ApplyMoveForce()
        {
            _targetSpeed = _movementInput.y * _config.MaxSpeed;
            _currentSpeed =  Mathf.Lerp(_currentSpeed, _targetSpeed, _config.AcceleratingSpeed * Time.fixedDeltaTime);
            
            var movementForce = transform.forward * (_movementInput.y * Mathf.Abs(_currentSpeed));
            _rb.AddForce(movementForce);
        }

        private void ApplySteeringTorque()
        {
            if (_movementInput.y == 0)
            {
                return;
            }
            
            var rotationAmount = _movementInput.x * -_config.MaxWheelTurnAngle;
            
            if (_movementInput.y > 0)
            {
                rotationAmount *= -1;
            }
            
            var steeringTorque = rotationAmount * _config.SteeringSpeed;

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
            var speedRatio = Mathf.Abs(_rb.velocity.magnitude) / _config.MaxSpeed;
            var xRotationAmount = _movementInput.x * -_config.MaxWheelTurnAngle;
            var yRotationAmount = speedRatio * _config.MaxWheelRotationSpeed;
            yRotationAmount *= Vector3.Dot(_rb.velocity, transform.forward) > 0 ? 1 : -1;

            foreach (var wheel in _frontWheels)
            {
                wheel.Rotate(yRotationAmount, 0f, 0f);
            }
            
            foreach (var wheel in _backWheels)
            {
                wheel.Rotate(yRotationAmount, 0f, 0f);
                wheel.localRotation = quaternion.Euler(0f, xRotationAmount, 0f);
            }
            
            _steeringWheel.localRotation = Quaternion.Euler(0f, 0f, xRotationAmount);
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