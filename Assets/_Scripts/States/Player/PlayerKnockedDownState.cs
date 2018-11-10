using RageCure.StateUtils;
using Styx.Entities.PlayerModule;
using Styx.Managers;
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
            PuckManager.Instance.GetPuckState().Reduce(Entities.PuckModule.Message.UpdateToLoose);
            PuckManager.Instance.GetPuckState().Puck.Rigidbody.velocity = playerState.Player.Rigidbody.velocity;
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
