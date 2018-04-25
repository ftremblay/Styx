using Assets._Scripts.Players.States;
using UnityEngine;

namespace Assets._Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerMovementState MovementState;
        public Rigidbody Rigidbody;

        public void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            UpdateMovement(MovementState);
        }

        private void UpdateMovement(PlayerMovementState state)
        {
            var move = state.MovementDirection;
            if (move.magnitude > 1f)
                move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            var forwardAmount = move.z;
            var turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * state.TurnSpeed * Time.deltaTime, 0);
            Rigidbody.velocity += (transform.forward * state.MovementSpeed * Time.deltaTime) * forwardAmount;

            Rigidbody.drag = state.Drag;
            Rigidbody.angularDrag = state.AngularDrag;
        }
    }
}
