using RageCure.StateUtils;
using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Managers;
using UnityEngine;
using RageCure.Commons.Extensions;
using RootMotion.FinalIK;

namespace Styx.States
{
    public class PlayerDashState : State<PlayerState>
    {
        [SerializeField]
        private AimIK _aimIK;

        private float _timestamp;

        public override void Enter(PlayerState playerState)
        {
            _timestamp = Time.time + playerState.Player.DashModel.Duration;
            playerState.Player.Rigidbody.velocity = transform.forward.normalized * playerState.Player.DashModel.Velocity * Time.deltaTime;
            playerState.Player.AnimatorModel.SetTrigger("Dash");
            _aimIK.enabled = false;
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
            _aimIK.enabled = true;
        }

        public override void FixedExecute(PlayerState playerState)
        {
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.relativeVelocity.magnitude > 10f)
            {
                var dashingPlayerState = PlayerManager.Instance.GetPlayerState(PlayerId.Value);
                if (dashingPlayerState.StateMachine.CurrentState == this)
                {
                    var playerId = collision.gameObject.GetComponent<PlayerId>();
                    var playerState = PlayerManager.Instance.GetPlayerState(playerId.Value);
                    var currentVelocity = playerState.Player.Rigidbody.velocity;
                    playerState.Reduce(Message.UpdateToKnockDown);
                    playerState.Player.RagdollModel.Rigidbodies.ForEach(r => r.velocity = (currentVelocity + new Vector3(0, 10, 0)) * collision.relativeVelocity.magnitude * Time.deltaTime);

                }
            }
        }
    }
}
