using RageCure.Commons.Casters;
using RootMotion.FinalIK;
using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Entities.PuckModule;
using Styx.Managers;
using System;
using UnityEngine;

namespace Styx.States
{
    public class PlayerPassState : PlayerSkatingState
    {
        [SerializeField]
        private SphereCaster _sphereCaster;
        [SerializeField]
        private GameObject _puckAnchor;
        [SerializeField]
        private AimIK _aimIk;

        private Action _handlePassAction = () => { };
        private GameObject _currentHitObject;
        private PuckState _puckState;
        private const float _passSpeed = 60f;

        private void ResetState()
        {
            _currentHitObject = null;
            _aimIk.enabled = true;
        }

        private Vector3 CalculatePassVelocity (Player player, Player otherPlayer)
        {
            var Sc = _passSpeed;
            var Sr = otherPlayer.Rigidbody.velocity.magnitude;
            var Pc = player.TransformModel.Transform.position;
            var Pr = otherPlayer.TransformModel.Transform.position;

            var D = Pc - Pr;
            var d = D.magnitude;
            var Vr = otherPlayer.Rigidbody.velocity;

            var a = (Sc * Sc) - (Sr * Sr);
            var b = 2f * Vector3.Dot(D, Vr);
            var c = -(d * d);

            var t = (-b + Mathf.Sqrt(Mathf.Abs((b * b) - (4f * a * c)))) / (2f * a);

            var Pi = Pr + (Vr * t);

            return ((Pi - Pc) / t);
        }

        public void HandlePass (Player player)
        {
            if (_currentHitObject != null)
            {
                var index = _currentHitObject.GetComponent<PlayerId>().Value;
                var otherPlayer = PlayerManager.Instance.GetPlayerState(index).Player;

                if (otherPlayer != null)
                {
                    var passVelocity = CalculatePassVelocity(player, otherPlayer);
                    PuckManager.Instance.UpdateVelocity(passVelocity);
                    return;
                }
                
            }
            var defaultPassVelocity = player.TransformModel.Transform.forward * _passSpeed;
            PuckManager.Instance.UpdateVelocity(defaultPassVelocity);
        }

        public void AnimEvent_Pass()
        {
            _handlePassAction();
        }

        public override void Enter(PlayerState playerState)
        {
            base.Enter(playerState);
            var player = playerState.Player;
            _puckState = PuckManager.Instance.GetPuckState();
            _currentHitObject = _sphereCaster.Cast(player);
            _handlePassAction = () =>
                {
                    playerState.Reduce(Entities.PlayerModule.Message.UpdateToNormal);
                };
            _aimIk.enabled = false;
            //TODO: Find a way to reference animation without passing string
            player.AnimatorModel.SetTrigger("Front pass");
        }

        public override void Execute(PlayerState playerState)
        {
            base.Execute(playerState);
        }

        public override void FixedExecute(PlayerState playerState)
        {
            base.FixedExecute(playerState);
            _puckState.Puck.SetPosition(_puckAnchor.transform.position);
            _puckState.Puck.SetRotation(Vector3.zero);
        }

        public override void Exit(PlayerState playerState)
        {
            base.Exit(playerState);
            HandlePass(playerState.Player);
            _puckState.Reduce(Entities.PuckModule.Message.UpdateToLoose);
            ResetState();
        }
    }
}
