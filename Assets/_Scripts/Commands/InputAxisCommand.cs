using UnityEngine;

namespace Styx.Commands
{
    public class InputAxisCommand : Command
    {
        [SerializeField]
        private string _inputAxis = "";
        [SerializeField]
        private float _value = 0f;

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override void Execute()
        {
            _value = Input.GetAxis(_inputAxis);
        }
    }
}
