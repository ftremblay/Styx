using Assets._Scripts.States;
using Assets._Scripts.Utils.Pooler;
using UnityEngine;

namespace Assets._Scripts.Views
{
    public class PuckView : MonoBehaviour, IPooledObject
    {
        public PuckState State;
        public Rigidbody Rigidbody;

        public void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnSpawn()
        {
            Rigidbody.AddForce(new Vector3(0, 0.5f, 1) * State.Speed, ForceMode.Impulse);
        }

        public void FixedUpdate()
        {
            Rigidbody.mass = State.Mass;
            Rigidbody.angularDrag = State.AngularDrag;
            Rigidbody.drag = State.Drag;
        }
    }
}
