using RageCure.StateUtils;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

using Assets._Scripts.Views;

namespace Assets._Scripts.States.Players
{
    public class PlayerSkatingState : State<PlayerView>
    {
        public Rigidbody Rigidbody;
        public Animator Animator;
        
        public override void Enter(PlayerView entity)
        {
        }

        public override void Execute(PlayerView entity)
        {
        }

        public override void Exit(PlayerView entity)
        {
        }

        public override void FixedExecute(PlayerView entity)
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            var move = (vertical * Vector3.forward + horizontal * Vector3.right);
            var movement = entity.Model.Movement;

            if (move.magnitude > 1f)
                move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            var forwardAmount = move.z;
            var turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * movement.TurnSpeed * Time.deltaTime, 0);
            Rigidbody.velocity += (transform.forward * movement.MovementSpeed * Time.deltaTime) * forwardAmount;

            Rigidbody.drag = movement.Drag;
            Rigidbody.angularDrag = movement.AngularDrag;

            Animator.SetFloat("Forward Amount", forwardAmount);
        }
    }
}
