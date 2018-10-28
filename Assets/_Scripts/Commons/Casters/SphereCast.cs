using UnityEngine;

namespace RageCure.Commons.Casters
{
    [CreateAssetMenu(fileName = "Pass Cast", menuName = "Casts/Pass Cast")]
    public class SphereCast : ScriptableObject
    {
        [SerializeField]
        private float _radius = 0f;
        [SerializeField]
        private float _maxDistance = 0f;
        [SerializeField]
        private LayerMask _mask;
        [SerializeField]
        private Vector3 _originOffset = Vector3.zero;

        public float Radius { get { return _radius; } }
        public float MaxDistance { get { return _maxDistance; } }
        public LayerMask Mask { get { return _mask; } }
        public Vector3 OriginOffset { get { return _originOffset; } }
    }
}
