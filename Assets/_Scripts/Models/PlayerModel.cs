using Assets._Scripts.Players.Models;
using System;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [CreateAssetMenu(fileName = "Player Model", menuName = "Models/New Player Model")]
    public class PlayerModel : ScriptableObject
    {
        public PlayerMovementModel Movement;
        public Guid Id = Guid.NewGuid();
        public bool HasPuck;

        public float WristShotPower = 100f;

        public void OnEnable()
        {
            Movement = CreateInstance<PlayerMovementModel>();
        }
    }
}
