using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Dash Model", menuName = "Models/Dash model")]
    public class DashModel : ScriptableObject
    {
        [SerializeField]
        [Range(1000, 5000)]
        private float _velocity = 1500f;
        [SerializeField]
        [Range(0, 2)]
        private float _cooldown = 0.1f;

        public float Velocity { get { return _velocity; } }
        public float Cooldown { get { return _cooldown; } }
    }
}
