using RageCure.StateUtils;
using Styx.Entities.PuckModule;
using UnityEngine;

namespace Styx.States
{
    public class PuckCarriedState : State<PuckState>
    {
        [SerializeField]
        private SphereCollider _collider;

        private void ReactivateCollider()
        {
            _collider.enabled = true;
        }

        public void Start()
        {
            _collider = _collider ?? GetComponent<SphereCollider>();
        }

        public override void Enter(PuckState entity)
        {
            _collider.enabled = false;
        }

        public override void Execute(PuckState entity)
        {
        }

        public override void Exit(PuckState entity)
        {
            Invoke("ReactivateCollider", 0.2f);
        }

        public override void FixedExecute(PuckState entity)
        {
        }
    }
}
