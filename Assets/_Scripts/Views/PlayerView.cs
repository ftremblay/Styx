using RageCure.StateUtils;
using Styx.Commands;
using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Managers;
using Styx.Models;
using Styx.States;
using UnityEngine;

namespace Styx.Views
{
    public class P
    {
        public float Boom { get; set; }
    }

    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private PlayerId _playerId;

        [SerializeField]
        private RigidbodyModel _rigidbodyModel;
        [SerializeField]
        private MovementModel _movementModel;
        [SerializeField]
        private AnimatorModel _animatorModel;
        [SerializeField]
        private TransformModel _transformModel;

        // *********************** INPUTS ***********************************
        [SerializeField]
        private InputAxisCommand _horizontalAxis;
        [SerializeField]
        private InputAxisCommand _verticalAxis;
        [SerializeField]
        private InputAxisCommand _shootAxis;
        [SerializeField]
        private InputKeyDownCommand _passKeyDown;
        // *******************************************************************

        // *************************** STATES ******************************************
        [SerializeField]
        private PlayerCarryPuckState _playerCarryPuckState;
        [SerializeField]
        private PlayerNormalState _playerNormalState;
        [SerializeField]
        private PlayerPassState _playerPassState;

        public PlayerState PlayerState { get; private set; }

        public void Awake()
        {
            _playerId = (_playerId ?? GetComponent<PlayerId>());

            _playerCarryPuckState = (_playerCarryPuckState ?? GetComponent<PlayerCarryPuckState>());
            _playerNormalState = (_playerNormalState ?? GetComponent<PlayerNormalState>());
            _playerPassState = (_playerPassState ?? GetComponent<PlayerPassState>());

            _rigidbodyModel = (_rigidbodyModel ?? GetComponent<RigidbodyModel>());
            _transformModel = (_transformModel ?? GetComponent<TransformModel>());
            _movementModel = (_movementModel ?? GetComponent<MovementModel>());
            _animatorModel = (_animatorModel ?? GetComponent<AnimatorModel>());
        }
        
        public void Start()
        {
            _rigidbodyModel.Rigidbody = GetComponent<Rigidbody>();
            _transformModel.Transform = transform;
            _animatorModel.Animator = GetComponent<Animator>();

            //TODO: Will be moved when team management system and game loop system is implemented
            PlayerState = new PlayerState
            {
                Player = new Player
                {
                    Id = _playerId,
                    RigidbodyModel = _rigidbodyModel,
                    MovementModel = _movementModel,
                    TransformModel = _transformModel,
                    AnimatorModel = _animatorModel,
                    Inputs = new Inputs
                    {
                        HorizontalAxis = _horizontalAxis,
                        VerticalAxis = _verticalAxis,
                        ShootAxis = _shootAxis,
                        PassKeyDown = _passKeyDown
                    }
                },
                States = new Entities.PlayerModule.States
                {
                    PlayerCarryPuck = _playerCarryPuckState,
                    PlayerNormal = _playerNormalState,
                    PlayerPass = _playerPassState
                },
                StateMachine = new StateMachine<PlayerState>(_playerNormalState)
            };
            PlayerManager.Instance.Register(PlayerState);
        }

        public void Update()
        {
            PlayerState.StateMachine.Update(PlayerState);
        }

        public void FixedUpdate()
        {
            _rigidbodyModel.Update();
            PlayerState.StateMachine.FixedUpdate(PlayerState);
            Debug.Log("Player state: " + PlayerState.StateMachine.CurrentState);
        }
    }
}
