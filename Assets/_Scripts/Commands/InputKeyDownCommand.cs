using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Styx.Commands
{
    public class InputKeyDownCommand : Command
    {
        [SerializeField]
        private string _inputKey = "";
        [SerializeField]
        private bool _isPressed = false;

        public bool IsPressed
        {
            get { return _isPressed; }
        }

        public override void Execute()
        {
            _isPressed = Input.GetButtonDown(_inputKey);
        }
    }
}
