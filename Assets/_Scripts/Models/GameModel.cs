using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [CreateAssetMenu(fileName = "Game Model", menuName = "Models/New Game Model")]
    public class GameModel : ScriptableObject
    {
        public Dictionary<Guid, PlayerModel> Players;

        public List<PlayerModel> PlayerStates;
        public PuckModel PuckState;
    }
}
