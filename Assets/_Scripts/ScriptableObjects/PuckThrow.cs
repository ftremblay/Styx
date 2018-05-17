using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.States
{
    [CreateAssetMenu(fileName = "Puck Throw", menuName = "Shots/New Puck Throw")]
    public class PuckThrow : ScriptableObject
    {
        public Vector3 ThrowAngle = new Vector3(0, 0.5f, 1);

        public float Speed = 2f;

        public float Drag = 0f;
        public float AngularDrag = 0.05f;
        public float Mass = 1f;
    }
}
