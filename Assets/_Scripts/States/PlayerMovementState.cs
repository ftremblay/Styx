using UnityEngine;

namespace Assets._Scripts.Players.States
{
    [CreateAssetMenu(fileName = "Player Movement State", menuName = "States/New Player Movement State")]
    public class PlayerMovementState : ScriptableObject
    {
        public Vector3 MovementDirection;

        public float MovementSpeed = 10;
        public float TurnSpeed = 10;

        public float Drag = 10;
        public float AngularDrag = 0.8f;
    }

}
