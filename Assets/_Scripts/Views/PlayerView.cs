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
        [SerializeField]
        private DashModel _dashModel;
        [SerializeField]
        private RagdollModel _ragdollModel;

        // *********************** INPUTS ***********************************
        [SerializeField]
        private InputAxisCommand _horizontalAxis;
        [SerializeField]
        private InputAxisCommand _verticalAxis;
        [SerializeField]
        private InputAxisCommand _shootAxis;
        [SerializeField]
        private InputKeyDownCommand _passKeyDown;
        [SerializeField]
        private InputKeyDownCommand _dashKeyDown;
        // *******************************************************************

        // *************************** STATES ******************************************
        [SerializeField]
        private PlayerCarryPuckState _playerCarryPuckState;
        [SerializeField]
        private PlayerNormalState _playerNormalState;
        [SerializeField]
        private PlayerPassState _playerPassState;
        [SerializeField]
        private PlayerDashState _playerDashState;
        [SerializeField]
        private PlayerKnockedDownState _playerKnockedDownState;

        public PlayerState PlayerState { get; private set; }

        public void Awake()
        {
            _playerId = (_playerId ?? GetComponent<PlayerId>());

            _playerCarryPuckState = (_playerCarryPuckState ?? GetComponent<PlayerCarryPuckState>());
            _playerNormalState = (_playerNormalState ?? GetComponent<PlayerNormalState>());
            _playerPassState = (_playerPassState ?? GetComponent<PlayerPassState>());
            _playerDashState = (_playerDashState ?? GetComponent<PlayerDashState>());
            _playerKnockedDownState = (_playerKnockedDownState ?? GetComponent<PlayerKnockedDownState>());
        }
        
        public void Start()
        {
            _rigidbodyModel.Rigidbody = GetComponent<Rigidbody>();
            _transformModel.Transform = transform;
            _animatorModel.Animator = GetComponent<Animator>();

            //TODO: Will be moved when team management system and game loop system is implemented
            //TODO: Use a builder pattern maybe??
            PlayerState = new PlayerState
            {
                Player = new Player
                {
                    Id = _playerId,
                    RigidbodyModel = _rigidbodyModel,
                    MovementModel = _movementModel,
                    TransformModel = _transformModel,
                    AnimatorModel = _animatorModel,
                    DashModel = _dashModel,
                    RagdollModel = _ragdollModel,
                    Inputs = new Inputs
                    {
                        HorizontalAxis = _horizontalAxis,
                        VerticalAxis = _verticalAxis,
                        ShootAxis = _shootAxis,
                        PassKeyDown = _passKeyDown,
                        DashKeyDown = _dashKeyDown
                    }
                },
                States = new Entities.PlayerModule.States
                {
                    PlayerCarryPuck = _playerCarryPuckState,
                    PlayerNormal = _playerNormalState,
                    PlayerPass = _playerPassState,
                    PlayerDash = _playerDashState,
                    PlayerKnockedDown = _playerKnockedDownState
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
        }
    }
}
