using RageCure.StateUtils;
using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Managers;
using UnityEngine;
using RageCure.Commons.Extensions;

namespace Styx.States
{
    public class PlayerDashState : State<PlayerState>
    {

        private float _timestamp;

        public override void Enter(PlayerState playerState)
        {
            _timestamp = Time.time + playerState.Player.DashModel.Duration;
            playerState.Player.RigidbodyModel.Rigidbody.velocity = transform.forward * Time.deltaTime * playerState.Player.DashModel.Velocity;
        }

        public override void Execute(PlayerState playerState)
        {
            if (_timestamp <= Time.time)
                playerState.Reduce(Message.UpdateToNormal);
        }

        public override void Exit(PlayerState playerState)
        {
            playerState.Player.DashModel.IsOnCooldown = true;
            this.Invoke(() => { playerState.Player.DashModel.IsOnCooldown = false; }, playerState.Player.DashModel.Cooldown);
        }

        public override void FixedExecute(PlayerState playerState)
        {
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Layer: " + LayerMask.LayerToName(collision.gameObject.layer) + " velocity: " + collision.relativeVelocity.magnitude);
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.relativeVelocity.magnitude > 10f)
            {
                var dashingPlayerState = PlayerManager.Instance.GetPlayerState(PlayerId.Value);
                if (dashingPlayerState.StateMachine.CurrentState == this)
                {
                    var playerId = collision.gameObject.GetComponent<PlayerId>().Value;
                    var playerState = PlayerManager.Instance.GetPlayerState(playerId);
                    var currentVelocity = playerState.Player.RigidbodyModel.Rigidbody.velocity;
                    playerState.Reduce(Message.UpdateToKnockDown);
                    playerState.Player.RagdollModel.Rigidbodies.ForEach(r => r.velocity = currentVelocity);
                    //playerState.Player.RagdollModel.Rigidbodies.ForEach(r => r.AddExplosionForce(collision.relativeVelocity.magnitude, collision.contacts[0].point, 50f));
                }
            }
        }
    }
}
