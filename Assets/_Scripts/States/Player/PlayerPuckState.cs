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
        public GameObject PuckJointPlaceholder;
        public Rigidbody PuckRigidbody;
        public PuckShot PuckShot;

        private HingeJoint _puckJoint;

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
            _puckJoint = PuckJointPlaceholder.AddComponent<HingeJoint>();
            PuckRigidbody.SetPosition(_puckJoint.transform.position);
            _puckJoint.connectedBody = PuckRigidbody;
        }

        public override void Execute(PlayerView entity)
        {
            base.Execute(entity);
            var shotX = CrossPlatformInputManager.GetAxis("ShotX");
            var shotY = CrossPlatformInputManager.GetAxis("ShotY");
            if (shotX != 0 && shotY != 0)
            {
                var shotAngle = Mathf.Abs((Mathf.Atan2(shotX, shotY) * Mathf.Rad2Deg) - 180);
                var playerOrientation = transform.eulerAngles.y;
                
                if (IsOrientationTop(shotAngle) == IsOrientationTop(playerOrientation))
                {
                    Shoot();
                    entity.StateMachine.ChangeState(entity.NormalState);
                }
            }
        }

        private bool IsOrientationTop(float angle)
        {
            return (angle >= 270f && angle <= 90f);
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
            Debug.Log("Shoot");
            transform.LookAt(transform.position + new Vector3(Input.GetAxis("ShotX"), 0, -Input.GetAxis("ShotY")));
            ReleasePuck();
            PuckRigidbody.AddForce(transform.forward * 30  + new Vector3(0, 0.4f, 0) , ForceMode.Impulse);

        }

        private void ReleasePuck()
        {
            if (_puckJoint != null)
                Destroy(_puckJoint);
        }
    }
}
