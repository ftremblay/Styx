using System.Collections.Generic;
using UnityEngine;

namespace Styx.Models
{
    public enum RagdollState
    {
        Ragdolled,
        Animated
    }

    [CreateAssetMenu(fileName = "Ragdoll Model", menuName = "Models/Ragdoll model")]
    public class RagdollModel : ScriptableObject
    {
        public RagdollState State = RagdollState.Animated;
        public List<Rigidbody> Rigidbodies;
        public List<Collider> Colliders;

        public Animator MainAnimator;
        public Rigidbody MainRigidbody;
        public Collider MainCollider;
    }
}
