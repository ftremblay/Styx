using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public enum RagdollState
    {
        Ragdolled,
        Animated
    }

    public class RagdollManager : MonoBehaviour
    {
        [SerializeField]
        private RagdollState _state = RagdollState.Animated;
        [SerializeField]
        private List<Rigidbody> _rigidbodies;
        [SerializeField]
        private List<Collider> _colliders;

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private Collider _mainCollider;

        public void Start()
        {
            _animator = (_animator ?? GetComponent<Animator>());
            _rigidbody = (_rigidbody ?? GetComponent<Rigidbody>());
            _mainCollider = (_mainCollider ?? GetComponent<CapsuleCollider>());

            _rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
            _colliders = GetComponentsInChildren<Collider>().ToList();
            Ragdolled = false;
        }

        public bool Ragdolled {
            get { return _state == RagdollState.Ragdolled; }
            set
            {
                if (value == true)
                {
                    _rigidbodies.ForEach(r => r.isKinematic = false);
                    _colliders.ForEach(c => c.enabled = true);
                    _rigidbody.isKinematic = true;
                    _animator.enabled = false;
                    _mainCollider.enabled = false;
                    _state = RagdollState.Ragdolled;
                    var randomRigidbody = _rigidbodies[Random.Range(0, _rigidbodies.Count())];
                    randomRigidbody.AddExplosionForce(5000f, transform.position, 50f);
                }
                else
                {
                    _rigidbodies.ForEach(r => r.isKinematic = true);
                    _colliders.ForEach(c => c.enabled = false);
                    _rigidbody.isKinematic = false;
                    _animator.enabled = true;
                    _mainCollider.enabled = true;
                    _state = RagdollState.Animated;
                }
            }
        }

        public void FixedUpdate()
        {
            if (Ragdolled == _animator.isActiveAndEnabled)
                Ragdolled = !_animator.isActiveAndEnabled;
        }
    }
}
