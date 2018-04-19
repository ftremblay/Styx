using Assets._Scripts.Players.States;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets._Scripts.Players.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementState MovementState;

        public void FixedUpdate()
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");
            Vector3 move = vertical * Vector3.forward + horizontal * Vector3.right;

            Debug.Log(move);
        }
    }
}
