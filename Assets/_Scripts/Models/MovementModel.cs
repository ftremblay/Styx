using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Movement Model", menuName = "Models/Movement model")]
    public class MovementModel : ScriptableObject
    {
        [SerializeField]
        private float _linearSpeed = 15f;
        [SerializeField]
        private float _rotationSpeed = 500f;

        public float LinearSpeed
        {
            get { return _linearSpeed; }
            set { _linearSpeed = value; }
        }

        public float RotationSpeed
        {
            get { return _rotationSpeed; }
            set { _rotationSpeed = value; }
        }
    }
}
