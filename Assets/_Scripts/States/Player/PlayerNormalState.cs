using Styx.Entities.PlayerModule;

namespace Styx.States
{
    public class PlayerNormalState : PlayerSkatingState
    {
        public override void Enter(PlayerState playerState)
        {
            base.Enter(playerState);
        }

        public override void Execute(PlayerState playerState)
        {
            base.Execute(playerState);
            playerState.Player.Inputs.DashKeyDown.Execute();

            if (playerState.Player.Inputs.DashKeyDown.IsPressed)
                playerState.Reduce(Message.UpdateToDash);
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
