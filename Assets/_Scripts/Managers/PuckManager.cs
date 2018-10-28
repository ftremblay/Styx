using Styx.Entities.PuckModule;
using UnityEngine;

namespace Styx.Managers
{
    public class PuckManager : MonoBehaviour
    {
        private PuckState _puckState;
        public static PuckManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

        }

        public void Register (PuckState puckState)
        {
            _puckState = puckState;
        }

        public PuckState GetPuckState()
        {
            return _puckState;
        }

        public void UpdatePuckState (PuckState updatedPuckState)
        {
            _puckState = updatedPuckState;
        }

        public void UpdateVelocity (Vector3 velocity)
        {
            _puckState.Puck.RigidbodyModel.Rigidbody.velocity = velocity;
        }

    }
}
