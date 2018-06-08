using Assets._Scripts.Events;
using Assets._Scripts.Utils;
using Assets._Scripts.Views;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets._Scripts.States.Players
{
    public class PlayerPuckState : PlayerSkatingState
    {
        [SerializeField] private GameObject _puckJointAnchor;
        [SerializeField] private Transform _opponentsGoal;

        private HingeJoint _puckJoint;

        public Rigidbody PuckRigidbody { get; set; }

        public void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();

            if (Animator == null)
                Animator = GetComponent<Animator>();
        }

        public override void Enter(PlayerView entity)
        {
            base.Enter(entity);
            _puckJoint = _puckJointAnchor.AddComponent<HingeJoint>();
            PuckRigidbody.SetPosition(_puckJoint.transform.position);
            _puckJoint.connectedBody = PuckRigidbody;
        }

        public override void Execute(PlayerView entity)
        {
            base.Execute(entity);
            HandleWristShot(entity);
        }

        public override void Exit(PlayerView entity)
        {
            base.Exit(entity);
            ReleasePuck();
        }

        public override void FixedExecute(PlayerView entity)
        {
            base.FixedExecute(entity);

            if (_puckJoint == null)
                entity.StateMachine.ChangeState(entity.NormalState);
        }

        private void Shoot()
        {
            var direction = (_opponentsGoal.position - transform.position).normalized;
            LookAtWherePlayerShoot(_opponentsGoal.position);
            ReleasePuck();
            PuckRigidbody.AddForce(direction * 30  + new Vector3(0, 0.4f, 0) , ForceMode.Impulse);
        }

        private void ReleasePuck()
        {
            if (_puckJoint != null)
                Destroy(_puckJoint);
        }

        private void LookAtWherePlayerShoot(Vector3 destination)
        {
            transform.LookAt(destination);
        }
        
        private void HandleWristShot(PlayerView entity)
        {
            var shot = CrossPlatformInputManager.GetAxis("Shot");
            if (IsWristShooting(shot))
            {
                Shoot();
                entity.StateMachine.ChangeState(entity.NormalState);
            }
        }

        private bool IsWristShooting(float shot)
        {
            return (_opponentsGoal.position.z > 0 && shot > 0.7f) || (_opponentsGoal.position.z < 0 && shot < -0.7f);
        } 
    }
}
