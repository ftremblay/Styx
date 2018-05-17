﻿using Assets._Scripts.Players.States;
using RageCure.EventUtils;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets._Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementState MovementState;
        public EventAggregator EventAggregrator;

        public void FixedUpdate()
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            MovementState.MovementDirection = (vertical * Vector3.forward + horizontal * Vector3.right);
        }

        public void Update()
        {
            if (Input.GetButtonDown("Pass"))
            {
                Debug.Log("Pass");
            }
        }
    }
}
