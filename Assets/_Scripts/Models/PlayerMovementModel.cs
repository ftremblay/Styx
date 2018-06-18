using UnityEngine;

namespace Assets._Scripts.Players.Models
{
    [CreateAssetMenu(fileName = "Player Movement Model", menuName = "Model/New Player Movement Model")]
    public class PlayerMovementModel : ScriptableObject
    {
        public Vector3 MovementDirection;

        public float MovementSpeed = 15;
        public float TurnSpeed = 500;

        public float Drag = 2;
        public float AngularDrag = 0.8f;
    }

}
