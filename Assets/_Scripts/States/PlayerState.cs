using UnityEngine;

namespace Assets._Scripts.States
{
    [CreateAssetMenu(fileName = "Player State", menuName = "States/New Player State")]
    public class PlayerState : ScriptableObject
    {
        public bool HasPuck;
    }
}
