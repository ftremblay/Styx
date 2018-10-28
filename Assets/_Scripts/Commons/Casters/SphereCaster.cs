using Styx.Entities.PlayerModule;
using UnityEngine;

namespace RageCure.Commons.Casters
{
    public class SphereCaster : MonoBehaviour
    {
        [SerializeField]
        private GameObject _currentHitObject;
        [SerializeField]
        private SphereCast _sphereCast;

        private Vector3 _origin = Vector3.zero;
        private Vector3 _direction = Vector3.zero;

        private float _currentHitDistance = 0f;

        public GameObject Cast (Player player)
        {
            _origin = transform.position + _sphereCast.OriginOffset;
            _direction = player.Inputs.VerticalAxis.Value * Vector3.forward + player.Inputs.HorizontalAxis.Value * Vector3.right;
            RaycastHit hit;
            if (Physics.SphereCast(_origin, _sphereCast.Radius, _direction, out hit, _sphereCast.MaxDistance, (int) _sphereCast.Mask, QueryTriggerInteraction.UseGlobal))
            {
                _currentHitObject = hit.transform.gameObject;
                _currentHitDistance = hit.distance;
            }
            else
            {
                _currentHitObject = null;
            }
            return _currentHitObject;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
            Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, _sphereCast.Radius);
        }
    }
}
