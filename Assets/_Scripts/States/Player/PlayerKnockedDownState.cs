using RageCure.StateUtils;
using Styx.Entities.PlayerModule;
using Styx.Models;
using UnityEngine;

namespace Styx.States
{
    public class PlayerKnockedDownState : State<PlayerState>
    {
        [SerializeField]
        private KnockedDownModel _knockedDownModel;
        [SerializeField]
        private Transform _rootToSpawn;
        private float _timestamp;

        public override void Enter(PlayerState playerState)
        {
            playerState.Player.AnimatorModel.Disable();
            _timestamp = Time.time + _knockedDownModel.Cooldown;
        }

        public override void Execute(PlayerState playerState)
        {
            if (Time.time >= _timestamp)
                playerState.Reduce(Message.UpdateToNormal);
        }

        public override void Exit(PlayerState playerState)
        {
            transform.position = _rootToSpawn.position;
            playerState.Player.AnimatorModel.Enable();
        }

        public override void FixedExecute(PlayerState playerState)
        {
        }
    }
}
