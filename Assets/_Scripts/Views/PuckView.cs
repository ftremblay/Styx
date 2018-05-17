using Assets._Scripts.States;
using Assets._Scripts.Utils.Pooler;
using UnityEngine;

namespace Assets._Scripts.Views
{
    public class PuckView : MonoBehaviour, IPooledObject
    {
        public PuckThrow Throw;
        public Rigidbody Rigidbody;

        public void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnSpawn()
        {
            Rigidbody.AddForce(transform.forward * Throw.Speed, ForceMode.Impulse);
        }

        public void FixedUpdate()
        {
            Rigidbody.mass = Throw.Mass;
            Rigidbody.angularDrag = Throw.AngularDrag;
            Rigidbody.drag = Throw.Drag;
        }
    }
}
