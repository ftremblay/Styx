using UnityEngine;

namespace Assets._Scripts.Utils.Casters
{
    public class SphereCaster : MonoBehaviour
    {
        public GameObject CurrentHitObject;

        public float SphereRadius;
        public float MaxDistance;
        public LayerMask LayerMask;

        private Vector3 _origin;
        private Vector3 _direction;

        private float _currentHitDistance;

        public GameObject Cast()
        {
            _origin = transform.position;
            _direction = transform.forward;
            RaycastHit hit;
            if (Physics.SphereCast(_origin, SphereRadius, _direction, out hit, MaxDistance, LayerMask, QueryTriggerInteraction.UseGlobal))
            {
                CurrentHitObject = hit.transform.gameObject;
                _currentHitDistance = hit.distance;
            }
            else
            {
                _currentHitDistance = MaxDistance;
                CurrentHitObject = null;
            }

            return CurrentHitObject;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
            Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, SphereRadius);
        }
    }
}
