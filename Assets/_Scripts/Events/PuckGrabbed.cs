using System;
using UnityEngine;

namespace Assets._Scripts.Events
{
    public class PuckGrabbed
    {
        public Guid PlayerId;
        public Rigidbody PuckRigibody;
        public Transform PuckTransform;

        public static PuckGrabbed Create(Guid playerId, Rigidbody puckRigidbody, Transform puckTransform)
        {
            return new PuckGrabbed(playerId, puckRigidbody, puckTransform);
        }

        private PuckGrabbed(Guid playerId, Rigidbody puckRigidbody, Transform puckTransform)
        {
            PlayerId = playerId;
            PuckRigibody = puckRigidbody;
            PuckTransform = puckTransform;
        }
    }
}
