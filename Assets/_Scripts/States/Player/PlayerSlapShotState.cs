using RageCure.StateUtils;
using RootMotion.FinalIK;
using Styx.Commons.Utils;
using Styx.Entities.PlayerModule;
using Styx.Entities.PuckModule;
using Styx.Managers;
using Styx.Models;
using System;
using UnityEngine;

namespace Styx.States.playerState
{
    public class PlayerSlapShotState : State<PlayerState>
    {
        [SerializeField]
        private GameObject _puckAnchor;
        private PuckState _puckState;

        [SerializeField]
        private AimIK _aimIk;

        private Vector3 _currentShotDirection;

        private bool _slapshooting = false;
        private Action _slapshotAnimEvent;

        public void Start()
        {
            _aimIk = _aimIk ?? GetComponent<AimIK>();
        }

        public override void Enter(PlayerState playerState)
        {
            _aimIk.enabled = false;
            playerState.Player.AnimatorModel.Animator.SetBool("Attempting slapshot", true);
            _puckState = PuckManager.Instance.GetPuckState();
            _currentShotDirection = GetShotDirection(playerState);
            _slapshotAnimEvent = () => Slapshot(playerState);
        }

        public override void Execute(PlayerState playerState)
        {
            if (_slapshooting)
                return;

            _puckState.Puck.SetPosition(_puckAnchor.transform.position);
            _puckState.Puck.SetRotation(Vector3.zero);

            playerState.Player.Inputs.HorizontalShootAxis.Execute();
            playerState.Player.Inputs.VerticalShootAxis.Execute();
        }

        public override void Exit(PlayerState playerState)
        {
            ResetState(playerState);
        }

        public override void FixedExecute(PlayerState playerState)
        {
            if (_slapshooting)
                return;

            HandleSlapshot(playerState);
        }

        public void Slapshot_AnimEvent()
        {
            _slapshotAnimEvent();
        }

        private void RotatePlayer(SlapshotModel model, Vector3 shotDirection)
        {
            if (shotDirection.magnitude == 0)
                return;

            var angle = Vector3.SignedAngle(transform.forward, shotDirection, Vector3.up);
            transform.RotateAround(_puckAnchor.transform.position, Vector3.up, angle * model.RotationSpeed * Time.deltaTime);
        }

        private void HandleSlapshot(PlayerState playerState)
        {
            var shotDirection = GetShotDirection(playerState);
            RotatePlayer(playerState.Player.SlapshotModel, shotDirection);

            Debug.Log("Shot direction: " + shotDirection + ", Shot magnitude: " + shotDirection.magnitude);
            if (shotDirection.magnitude == 0)
            {
                if ((_currentShotDirection.magnitude - shotDirection.magnitude) >= 0.5f)
                {
                    //Slapshot
                    playerState.Player.AnimatorModel.Animator.SetTrigger("Slapshot");
                    _slapshooting = true;
                }
            }
            else
            {
                if (shotDirection.magnitude > _currentShotDirection.magnitude)
                {
                    _currentShotDirection = shotDirection;
                }
            }
        }

        private void Slapshot(PlayerState playerState)
        {
            
            _puckState.Reduce(Entities.PuckModule.Message.UpdateToLoose);
            var shotVelocity = (_currentShotDirection * playerState.Player.SlapshotModel.Power * _currentShotDirection.magnitude + new Vector3(0, playerState.Player.SlapshotModel.UpwardVelocity, 0)) * Time.deltaTime;
            Debug.Log("SLAPSHOT - Shot direction: " + _currentShotDirection + ", Shot magnitude: " + shotVelocity.magnitude);
            _puckState.Puck.Rigidbody.velocity = shotVelocity;
            playerState.Reduce(Entities.PlayerModule.Message.UpdateToNormal);
        }

        private void ResetState(PlayerState playerState)
        {
            playerState.Player.AnimatorModel.Animator.SetBool("Attempting slapshot", false);
            _currentShotDirection = Vector3.zero;
            _slapshooting = false;
        }

        private Vector3 GetShotDirection(PlayerState playerState)
        {
            return Vector3Utils.NormalizeIfGreater(Vector3.forward * playerState.Player.Inputs.VerticalShootAxis.Value + Vector3.left * playerState.Player.Inputs.HorizontalShootAxis.Value);
        }
    }
}
