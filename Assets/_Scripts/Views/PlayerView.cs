using UnityEngine;
using RageCure.StateUtils;

using Assets._Scripts.Models;
using Assets._Scripts.States.Players;

namespace Assets._Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        public StateMachine<PlayerView> StateMachine;

        public PlayerNormalState NormalState;
        public PlayerPuckState PuckState;
        
        public PlayerModel PlayerModel;

        public void Start()
        {
            PlayerModel = ScriptableObject.CreateInstance<PlayerModel>();

            StateMachine = new StateMachine<PlayerView>(this, NormalState);

            if (NormalState == null)
                NormalState = GetComponent<PlayerNormalState>();
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        public void PickPuck()
        {
            StateMachine.ChangeState(PuckState);
        }
    }
}
