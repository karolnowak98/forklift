using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "Forklift Config", menuName = "Configs/Forklift Config")]
    public class ForkliftConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _liftingSpeed;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _maxKg;

        public float MovementSpeed => _movementSpeed;
        public float LiftingSpeed => _liftingSpeed;
        public float MaxAngle => _maxAngle;
        public float MaxKg => _maxKg;
    }
}