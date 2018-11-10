using RageCure.StateUtils;
using Styx.Commands;
using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Managers;
using Styx.Models;
using Styx.States;
using Styx.States.playerState;
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
        [SerializeField]
        private SlapshotModel _slapshotModel;

        [SerializeField]
        private Rigidbody _rigidbody;

        // *********************** INPUTS ***********************************
        [SerializeField]
        private InputAxisCommand _horizontalAxis;
        [SerializeField]
        private InputAxisCommand _verticalAxis;
        [SerializeField]
        private InputAxisCommand _horizontalShootAxis;
        [SerializeField]
        private InputAxisCommand _verticalShootAxis;
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
        [SerializeField]
        private PlayerSlapShotState _playerSlapShotState;

        public PlayerState PlayerState { get; private set; }

        public void Awake()
        {
            _playerId = (_playerId ?? GetComponent<PlayerId>());

            _playerCarryPuckState = (_playerCarryPuckState ?? GetComponent<PlayerCarryPuckState>());
            _playerNormalState = (_playerNormalState ?? GetComponent<PlayerNormalState>());
            _playerPassState = (_playerPassState ?? GetComponent<PlayerPassState>());
            _playerDashState = (_playerDashState ?? GetComponent<PlayerDashState>());
            _playerKnockedDownState = (_playerKnockedDownState ?? GetComponent<PlayerKnockedDownState>());
            _playerSlapShotState = (_playerSlapShotState ?? GetComponent<PlayerSlapShotState>());
        }
        
        public void Start()
        {
            var animator = GetComponent<Animator>();
            _rigidbody = _rigidbody ?? GetComponent<Rigidbody>();
            _transformModel.Transform = transform;
            _animatorModel.Animator = animator;
            _ragdollModel.MainAnimator = animator;
            _ragdollModel.MainCollider = GetComponent<CapsuleCollider>();
            _ragdollModel.MainRigidbody = _rigidbody;

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
                    SlapshotModel = _slapshotModel,
                    Rigidbody = _rigidbody,
                    Inputs = new Inputs
                    {
                        HorizontalAxis = _horizontalAxis,
                        VerticalAxis = _verticalAxis,
                        HorizontalShootAxis = _horizontalShootAxis,
                        VerticalShootAxis = _verticalShootAxis,
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
                    PlayerKnockedDown = _playerKnockedDownState,
                    PlayerSlapShot = _playerSlapShotState
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
            PlayerState.Player.UpdateRigidbody();
            PlayerState.StateMachine.FixedUpdate(PlayerState);
        }
    }
}
