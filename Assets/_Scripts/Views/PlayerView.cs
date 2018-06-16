using UnityEngine;
using RageCure.StateUtils;

using Assets._Scripts.Models;
using Assets._Scripts.States.Players;

namespace Assets._Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        public StateMachine<PlayerView> StateMachine { get; set; }

        [SerializeField] private PlayerNormalState _normalState;
        [SerializeField] private PlayerPuckState _puckState;
        [SerializeField] private PlayerModel _playerModel;

        public PlayerNormalState NormalState { get { return _normalState; } }
        public PlayerPuckState PuckState { get { return _puckState; } }
        public PlayerModel Model { get { return _playerModel; } }

        public void Start()
        {
            _playerModel = ScriptableObject.CreateInstance<PlayerModel>();

            StateMachine = new StateMachine<PlayerView>(this, _normalState);

            if (_normalState == null)
                _normalState = GetComponent<PlayerNormalState>();

            if (_puckState == null)
                _puckState = GetComponent<PlayerPuckState>();
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        public void PickPuck(Rigidbody puckRigidbody)
        {
            _puckState.PuckRigidbody = puckRigidbody;
            StateMachine.ChangeState(_puckState);
        }
    }
}
