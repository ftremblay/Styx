using Styx.Entities.PlayerModule;
using Styx.Entities.PuckModule;
using Styx.Managers;
using System;
using UnityEngine;

namespace Styx.States
{
    public class PlayerCarryPuckState : PlayerSkatingState
    {
        [SerializeField]
        private GameObject _puckAnchor;
        private Guid _playerId;
        private PuckState _puckState;

        private void HandlePass (PlayerState playerState)
        {
            if (playerState.Player.Inputs.PassKeyDown.IsPressed)
            {
                playerState.Reduce(Entities.PlayerModule.Message.UpdateToPass);
            }
        }

        public override void Enter(PlayerState playerState)
        {
            base.Enter(playerState);
            _puckState = PuckManager.Instance.GetPuckState();
        }

        public override void Execute(PlayerState playerState)
        {
            base.Execute(playerState);
            _puckState.Puck.SetPosition(_puckAnchor.transform.position);
            _puckState.Puck.SetRotation(Vector3.zero);

            playerState.Player.Inputs.ShootAxis.Execute();
            playerState.Player.Inputs.PassKeyDown.Execute();

            HandlePass(playerState);
        }

        public override void FixedExecute(PlayerState playerState)
        {
            base.FixedExecute(playerState);
        }

        public override void Exit(PlayerState playerState)
        {
            base.Exit(playerState);
        }
    }
}
