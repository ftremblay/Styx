using Assets._Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets._Scripts.Utils.Casters
{
    public class SphereCaster : MonoBehaviour
    {
        public GameObject CurrentHitObject;
        public bool CastEveryFrame = false;
        public SphereCast Configs;

        private Vector3 _origin;
        private Vector3 _direction;

        private float _currentHitDistance;

        public GameObject Cast()
        {
            _origin = transform.position + Configs.OriginOffset;
            _direction = transform.forward;
            RaycastHit hit;
            if (Physics.SphereCast(_origin, Configs.Radius, _direction, out hit, Configs.MaxDistance, Configs.Mask, QueryTriggerInteraction.UseGlobal))
            {
                CurrentHitObject = hit.transform.gameObject;
                _currentHitDistance = hit.distance;
            }
            else
            {
                _currentHitDistance = Configs.MaxDistance;
                CurrentHitObject = null;
            }

            return CurrentHitObject;
        }

        public void FixedUpdate()
        {
            if (!CastEveryFrame) return;

            Cast();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
            Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, Configs.Radius);
        }
    }
}
