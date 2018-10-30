using RageCure.StateUtils;
using Styx.Entities.PlayerModule;
using Styx.Models;
using UnityEngine;

namespace Styx.States
{
    [RequireComponent(typeof(DashModel))]
    public class PlayerDashState : State<PlayerState>
    {
        [SerializeField]
        private DashModel _dashModel;
        private float _timestamp;

        public override void Enter(PlayerState playerState)
        {
            _timestamp = Time.time + _dashModel.Cooldown;
        }

        public override void Execute(PlayerState playerState)
        {
            if (_timestamp > Time.time)
                playerState.Player.RigidbodyModel.Rigidbody.velocity = transform.forward * Time.deltaTime * _dashModel.Velocity;
            else
                playerState.Reduce(Message.UpdateToNormal);
                
        }

        public override void Exit(PlayerState playerState)
        {
        }

        public override void FixedExecute(PlayerState playerState)
        {
        }
    }
}
