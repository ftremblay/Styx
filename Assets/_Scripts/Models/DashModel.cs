using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Dash Model", menuName = "Models/Dash model")]
    public class DashModel : ScriptableObject
    {
        [SerializeField]
        private float _velocity = 500f;
        

        public float Velocity { get { return _velocity; } }
    }
}
