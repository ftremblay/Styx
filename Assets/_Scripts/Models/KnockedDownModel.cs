using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Knocked Down Model", menuName = "Models/Knocked Down model")]
    public class KnockedDownModel : ScriptableObject
    {
        [SerializeField]
        private float _cooldown = 2f;

        public float Cooldown { get { return _cooldown; } }
    }
}
