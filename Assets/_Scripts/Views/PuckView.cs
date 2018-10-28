using RageCure.StateUtils;
using Styx.Entities.PuckModule;
using Styx.Managers;
using Styx.Models;
using Styx.States;
using UnityEngine;

namespace Styx.Views
{
    public class PuckView : MonoBehaviour
    {
        [SerializeField]
        private RigidbodyModel _rigidbodyModel;
        [SerializeField]
        private TransformModel _transformModel;

        [SerializeField]
        private PuckLooseState _puckLooseState;
        [SerializeField]
        private PuckCarriedState _puckCarriedState;

        public PuckState PuckState { get; private set; }

        public void Awake()
        {
            _rigidbodyModel = _rigidbodyModel ?? GetComponent<RigidbodyModel>();
            _transformModel = _transformModel ?? GetComponent<TransformModel>();

            _puckLooseState = _puckLooseState ?? GetComponent<PuckLooseState>();
            _puckCarriedState = _puckCarriedState ?? GetComponent<PuckCarriedState>();
        }

        public void Start()
        {
            _rigidbodyModel.Rigidbody = GetComponent<Rigidbody>();
            _transformModel.Transform = transform;

            PuckState = new PuckState
            {
                Puck = new Puck
                {
                    RigidbodyModel = _rigidbodyModel,
                    TransformModel = _transformModel
                },
                States = new Entities.PuckModule.States
                {
                    PuckCarried = _puckCarriedState,
                    PuckLoose = _puckLooseState
                },
                StateMachine = new StateMachine<PuckState>(_puckLooseState)
            };
            PuckManager.Instance.Register(PuckState);
        }

        public void Update()
        {
            PuckState.StateMachine.Update(PuckState);
        }

        public void FixedUpdate()
        {
            _rigidbodyModel.Update();
            PuckState.StateMachine.FixedUpdate(PuckState);
            Debug.Log("Puck state: " + PuckState.StateMachine.CurrentState);
        }
    }
}
