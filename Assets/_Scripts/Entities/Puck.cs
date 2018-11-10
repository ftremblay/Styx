using RageCure.StateUtils;
using Styx.Models;
using UnityEngine;

namespace Styx.Entities.PuckModule
{
    public enum Message
    {
        UpdateToLoose,
        UpdateToCarried
    }

    public class Puck
    {
        public RigidbodyModel RigidbodyModel { get; set; }
        public TransformModel TransformModel { get; set; }

        public Rigidbody Rigidbody { get; set; }

        public void UpdateRigidbody()
        {
            Rigidbody.mass = RigidbodyModel.Mass;
            Rigidbody.drag = RigidbodyModel.Drag;
            Rigidbody.angularDrag = RigidbodyModel.AngularDrag;
        }

        public void SetPosition (Vector3 pos)
        {
            TransformModel.Transform.position = pos;
        }

        public void SetRotation (Vector3 rot)
        {
            TransformModel.Transform.rotation = Quaternion.Euler(rot);
        }
    }

    public class States
    {
        public State<PuckState> PuckLoose { get; set; }
        public State<PuckState> PuckCarried { get; set; }
    }

    public class PuckState
    {
        public Puck Puck { get; set; }
        public States States { get; set; }
        public StateMachine<PuckState> StateMachine { get; set; }

        public void Reduce (Message msg)
        {
            switch(msg)
            {
                case Message.UpdateToLoose:
                    StateMachine.ChangeState(this, States.PuckLoose);
                    break;
                case Message.UpdateToCarried:
                    StateMachine.ChangeState(this, States.PuckCarried);
                    break;
            }
        }
    }


}
