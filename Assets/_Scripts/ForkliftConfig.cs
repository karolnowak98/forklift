using UnityEngine;

namespace Forklift
{
    [CreateAssetMenu(fileName = "Forklift Config", menuName = "Configs/Forklift Config")]
    public class ForkliftConfig : ScriptableObject
    {
        [SerializeField] private float _acceleratingSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _steeringSpeed;
        [SerializeField] private float _maxWheelTurnAngle;
        [SerializeField] private float _maxWheelRotationSpeed;
        [SerializeField] private Vector3 _centerMassOffset;
        [SerializeField] private float _frictionCoefficient;
        [SerializeField] private float _forkLiftingSpeed;
        [SerializeField] private Vector2 _liftMinMaxPositions;

        public float AcceleratingSpeed => _acceleratingSpeed;
        public float MaxSpeed => _maxSpeed;
        public float SteeringSpeed => _steeringSpeed;
        public float MaxWheelTurnAngle => _maxWheelTurnAngle;
        public float MaxWheelRotationSpeed => _maxWheelRotationSpeed;
        public Vector3 CenterMassOffset => _centerMassOffset;
        public float FrictionCoefficient => _frictionCoefficient;
        public float ForkLiftingSpeed => _forkLiftingSpeed;
        public Vector2 LiftMinMaxPositions => _liftMinMaxPositions;
    }
}