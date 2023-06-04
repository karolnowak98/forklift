using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Forklift Config", menuName = "Configs/Forklift Config")]
    public class ForkliftConfig : ScriptableObject
    {
        [SerializeField] private float _acceleratingSpeed;
        [SerializeField] private float _maxWheelTurnAngle;
        [SerializeField] private float _centerMassOffset;
        [SerializeField] private float _steeringSpeed;
        [SerializeField] private float _frictionCoefficient;
        [SerializeField] private float _forkLiftingSpeed;
        [SerializeField] private Vector2 _liftMinMaxPositions;

        public float AcceleratingSpeed => _acceleratingSpeed;
        public float MaxWheelTurnAngle => _maxWheelTurnAngle;
        public float CenterMassOffset => _centerMassOffset;
        public float SteeringSpeed => _steeringSpeed;
        public float FrictionCoefficient => _frictionCoefficient;
        public float ForkLiftingSpeed => _forkLiftingSpeed;
        public Vector2 LiftMinMaxPositions => _liftMinMaxPositions;
    }
}