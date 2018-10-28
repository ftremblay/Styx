using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Transform Model", menuName = "Models/Transform model")]
    public class TransformModel : ScriptableObject
    {
        [SerializeField]
        private Transform _transform;

        public Transform Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
    }
}
