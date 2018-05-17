using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sphere Cast", menuName = "Cast/New Sphere Cast")]
    public class SphereCast : ScriptableObject
    {
        public float Radius;
        public float MaxDistance;
        public LayerMask Mask;

        public Vector3 OriginOffset;
    }
}
