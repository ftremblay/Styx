using System;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [CreateAssetMenu(fileName = "Puck Model", menuName = "Models/New Puck Model")]
    public class PuckModel : ScriptableObject
    {
        public float Speed = 2f;

        public float Drag = 0f;
        public float AngularDrag = 0.05f;
        public float Mass = 1f;
    }
}
