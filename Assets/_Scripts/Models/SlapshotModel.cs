using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Slapshot Model", menuName = "Models/Slapshot model")]
    public class SlapshotModel : ScriptableObject
    {
        public float Power = 4000f;
        public float UpwardVelocity = 500f;
        public float RotationSpeed = 3f;
    }
}
