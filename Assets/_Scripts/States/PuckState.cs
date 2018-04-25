﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.States
{
    [CreateAssetMenu(fileName = "Puck State", menuName = "States/New Puck State")]
    public class PuckState : ScriptableObject
    {
        public Vector3 ShotAngle = new Vector3(0, 0.5f, 1);

        public float Speed = 2;

        public float Drag = 0;
        public float AngularDrag = 0.05f;
        public float Mass = 1;
    }
}
