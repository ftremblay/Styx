using Styx.Entities.PlayerModule;
using RageCure.StateUtils;
using UnityEngine;
using Styx.Commons.Utils;

namespace Styx.States
{
    public class PlayerSkatingState : State<PlayerState>
    {
        private void Rotate(float turnAmount, Player player)
        {
            transform.Rotate(0f, turnAmount * player.MovementModel.RotationSpeed * Time.deltaTime, 0f);
        }

        private void Move(float forwardAmount, Player player)
        {
            player.AddVelocity(transform.forward * player.LinearSpeed * Time.deltaTime * forwardAmount);
        } 

        public override void Enter(PlayerState player)
        {
        }

        public override void Execute(PlayerState playerState)
        {
            var player = playerState.Player;
            player.Inputs.HorizontalAxis.Execute();
            player.Inputs.VerticalAxis.Execute();
        }

        public override void FixedExecute(PlayerState playerState)
        {
            var player = playerState.Player;
            var movementVector = (player.Inputs.VerticalAxis.Value * Vector3.forward + player.Inputs.HorizontalAxis.Value * Vector3.right);
            var normalizedMovementVector = Vector3Utils.NormalizeIfNot(movementVector);
            var move = transform.InverseTransformDirection(normalizedMovementVector);

            var forwardAmount = move.z;
            var turnAmount = Mathf.Atan2(move.x, move.z);

            Rotate(turnAmount, player);
            Move(forwardAmount, player);
            player.AnimatorModel.SetFloat("Forward Amount", forwardAmount);
        }

        public override void Exit(PlayerState playerState)
        {
        }

        //public void OnCollisionEnter(Collision collision)
        //{
        //    Debug.Log(collision.relativeVelocity.magnitude);
        //    if (!_isColliderActive && _playerState == null)
        //        return;

        //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.relativeVelocity.magnitude > 10f)
        //    {
        //        var playerId = collision.gameObject.GetComponent<PlayerId>().Value;
        //        var playerState = PlayerManager.Instance.GetPlayerState(playerId);
        //        if (playerState.StateMachine.CurrentState.GetType() != typeof(PlayerDashState))
        //            playerState.Reduce(Message.UpdateToKnockDown);
        //    }
        //}
    }
}
