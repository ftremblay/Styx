using Styx.Commands;
using Styx.Models;
using RageCure.StateUtils;
using UnityEngine;

namespace Styx.Entities.PlayerModule
{
    public enum Message
    {
        UpdateToNormal,
        UpdateToCarryPuck,
        UpdateToPass
    }

    public class Inputs
    {
        public InputAxisCommand HorizontalAxis { get; set; }
        public InputAxisCommand VerticalAxis { get; set; }
        public InputAxisCommand ShootAxis { get; set; }
        public InputKeyDownCommand PassKeyDown { get; set; }
    }
    
    public class Player
    {
        public PlayerId Id { get; set; }
        public RigidbodyModel RigidbodyModel { get; set; }
        public MovementModel MovementModel { get; set; }
        public AnimatorModel AnimatorModel { get; set; }
        public TransformModel TransformModel { get; set; }
        public Inputs Inputs { get; set; }
        //TODO: Add scriptable object model to gather pass speed information

        public void AddVelocity(Vector3 value)
        {
            RigidbodyModel.Rigidbody.velocity += value;
        }

        public float LinearSpeed
        {
            get { return MovementModel.LinearSpeed; }
        }

        public float RotationSpeed
        {
            get { return MovementModel.RotationSpeed; }
        }
    }

    public class States
    {
        public State<PlayerState> PlayerCarryPuck { get; set; }
        public State<PlayerState> PlayerNormal { get; set; }
        public State<PlayerState> PlayerPass { get; set; }
    }

    public class PlayerState
    {
        public Player Player { get; set; }
        public States States { get; set; }
        public StateMachine<PlayerState> StateMachine { get; set; }

        public void Reduce (Message msg)
        {
            switch (msg)
            {
                case Message.UpdateToNormal:
                    StateMachine.ChangeState(this, States.PlayerNormal);
                    break;
                case Message.UpdateToCarryPuck:
                    StateMachine.ChangeState(this, States.PlayerCarryPuck);
                    break;
                case Message.UpdateToPass:
                    StateMachine.ChangeState(this, States.PlayerPass);
                    break;
            }
        }
    }
}
