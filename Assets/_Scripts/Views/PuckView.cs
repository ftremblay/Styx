using Assets._Scripts.Models;
using UnityEngine;
using RageCure.StateUtils;

using Assets._Scripts.States.Puck;
using System;

namespace Assets._Scripts.Views
{
    public class PuckView : MonoBehaviour
    {
        public PuckModel PuckModel;

        public StateMachine<PuckView> StateMachine;

        public PuckLooseState LooseState;
        public PuckCarriedState CarriedState;

        public void Start()
        {
            PuckModel = ScriptableObject.CreateInstance<PuckModel>();

            StateMachine = new StateMachine<PuckView>(this, LooseState);

            LooseState.PuckPicked += PuckPicked;
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        public void PuckPicked(object sender, EventArgs e)
        {
            StateMachine.ChangeState(CarriedState);
        }
    }
}
