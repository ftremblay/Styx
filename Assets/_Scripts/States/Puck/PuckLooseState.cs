using RageCure.StateUtils;
using Styx.Entities;
using Styx.Entities.PuckModule;
using Styx.Managers;
using UnityEngine;

using PlayerMessage = Styx.Entities.PlayerModule.Message;
using PuckMessage = Styx.Entities.PuckModule.Message;

namespace Styx.States
{
    public class PuckLooseState : State<PuckState>
    {
        private PuckState _puckState;
        public PuckState PuckState { get
            {
                if (_puckState == null)
                {
                    _puckState = PuckManager.Instance.GetPuckState();
                }
                return _puckState;
            }
            private set {
                _puckState = value;
            }
        }

        public override void Enter(PuckState puckState)
        {}

        public override void Execute(PuckState puckState)
        {}

        public override void Exit(PuckState puckState)
        {}

        public override void FixedExecute(PuckState puckState)
        {}

        public void OnTriggerEnter(Collider other)
        {
            if (!PuckState.StateMachine.IsInState(this))
                return;

            //TODO: Find a way to not use hardcoded string to reference the layer name
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                var playerId = other.gameObject.GetComponent<PlayerId>().Value;
                var playerState = PlayerManager.Instance.GetPlayerState(playerId);
                playerState.Reduce(PlayerMessage.UpdateToCarryPuck);

                PuckState.Reduce(PuckMessage.UpdateToCarried);
            }
        }
    }
}
