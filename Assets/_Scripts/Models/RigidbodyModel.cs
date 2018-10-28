using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Rigidbody Model", menuName = "Models/Rigidbody model")]
    public class RigidbodyModel : ScriptableObject
    {
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _drag = 2f;
        [SerializeField]
        private float _angularDrag = 0.8f;
        [SerializeField]
        private float _mass = 1f;

        public Rigidbody Rigidbody
        {
            get { return _rigidbody; }
            set { _rigidbody = value; }
        }

        public float Drag
        {
            get { return _drag; }
            set { _drag = value; }
        }

        public float AngularDrag
        {
            get { return _angularDrag; }
            set { _angularDrag = value; }
        }

        public float Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        public void Update()
        {
            _rigidbody.drag = _drag;
            _rigidbody.angularDrag = _angularDrag;
            _rigidbody.mass = _mass;
        }
    }
}
