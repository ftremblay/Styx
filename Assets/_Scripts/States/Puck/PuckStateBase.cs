using Assets._Scripts.Models;
using Assets._Scripts.Views;
using RageCure.StateUtils;
using UnityEngine;

namespace Assets._Scripts.States.Puck
{
    public class PuckStateBase : State<PuckView>
    {
        public Rigidbody Rigidbody;

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
            Rigidbody.mass = entity.PuckModel.Mass;
            Rigidbody.angularDrag = entity.PuckModel.AngularDrag;
            Rigidbody.drag = entity.PuckModel.Drag;
        }
    }
}
