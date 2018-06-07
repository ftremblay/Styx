using Assets._Scripts.Models;
using Assets._Scripts.Views;
using RageCure.StateUtils;
using UnityEngine;

namespace Assets._Scripts.States.Players
{
    public class PlayerNormalState : PlayerSkatingState
    {
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
        }

        public override void Execute(PlayerView entity)
        {
            base.Execute(entity);
        }

        public override void Exit(PlayerView entity)
        {
            base.Execute(entity);
        }

        public override void FixedExecute(PlayerView entity)
        {
            base.FixedExecute(entity);
        }
    }
}
