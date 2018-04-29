using Assets._Scripts.States;
using Assets._Scripts.Utils.Pooler;
using UnityEngine;

namespace Assets._Scripts.Views
{
    public class PuckView : MonoBehaviour, IPooledObject
    {
        public PuckShot Shot;
        public Rigidbody Rigidbody;

        public void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnSpawn()
        {
            Rigidbody.AddForce(transform.forward * Shot.Speed, ForceMode.Impulse);
        }

        public void FixedUpdate()
        {
            Rigidbody.mass = Shot.Mass;
            Rigidbody.angularDrag = Shot.AngularDrag;
            Rigidbody.drag = Shot.Drag;
        }
    }
}
