using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Forklift Config", menuName = "Configs/Forklift Config")]
    public class ForkliftConfig : ScriptableObject
    {
        [SerializeField, Range(0, 100)]
        private float _movementSpeed;
        
        [SerializeField, Range(0, 100)]
        private float _liftingSpeed;
        
        [SerializeField, Range(0, 80)]
        private float _maxAngle;
        
        [SerializeField, Range(0, 100)]
        private float _maxKg;

        [SerializeField] 
        private Vector2Int _liftMinMaxPositions;

        public Vector2Int LiftMinMaxPositions => _liftMinMaxPositions;
        public float MovementSpeed => _movementSpeed;
        public float LiftingSpeed => _liftingSpeed;
        public float MaxAngle => _maxAngle;
        public float MaxKg => _maxKg;
    }
}