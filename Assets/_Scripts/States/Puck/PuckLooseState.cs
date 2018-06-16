using Assets._Scripts.Views;
using RageCure.StateUtils;
using System;
using UnityEngine;

namespace Assets._Scripts.States.Puck
{
    public class PuckLooseState : State<PuckView>
    {
        public event EventHandler PuckPicked;

        [SerializeField]
        private Rigidbody _rigidbody;

        public void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Enter(PuckView entity)
        {
        }

        public override void Execute(PuckView entity)
        {
        }

        public override void Exit(PuckView entity)
        {
        }

        public override void FixedExecute(PuckView entity)
        {
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerView>().PickPuck(_rigidbody);
                PuckPicked(this, null);
            }
        }
    }
}
