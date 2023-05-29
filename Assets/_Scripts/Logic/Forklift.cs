using Data;
using UnityEngine;

namespace Logic
{
    public class Forklift : MonoBehaviour
    {
        [SerializeField] private ForkliftConfig _config;

        public void Drive()
        {
            Debug.Log("Drive");
        }
    }
}