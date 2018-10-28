using Styx.Entities.PlayerModule;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Styx.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Dictionary<Guid, PlayerState> _players = new Dictionary<Guid, PlayerState>();
        public static PlayerManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public PlayerState GetPlayerState (Guid key)
        {
            if (_players.ContainsKey(key))
                return _players[key];
            throw new Exception("PlayerManager.cs - Players dictionary doesn't contains key: " + key.ToString());
        }

        public Player GetPlayer (Guid key)
        {
            if (_players.ContainsKey(key))
                return _players[key].Player;
            throw new Exception("PlayerManager.cs - Players dictionary doesn't contains key: " + key.ToString());
        }

        public void UpdatePlayer (PlayerState playerState)
        {
            _players[playerState.Player.Id.Value] = playerState;
        }

        public void Register (PlayerState playerState)
        {
            if (!_players.ContainsKey(playerState.Player.Id.Value))
                _players.Add(playerState.Player.Id.Value, playerState);
        }
    }
}
