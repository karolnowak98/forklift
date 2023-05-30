using UnityEngine;

namespace Logic
{
    public class Palette : MonoBehaviour
    {
        [SerializeField, Tooltip("In kilograms."), Range(0, 100)] 
        private float weight;
        
        
    }
}