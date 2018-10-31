using Styx.Models;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class RagdollManager : MonoBehaviour
    {
        [SerializeField]
        private RagdollModel _ragdollModel;

        public void Start()
        {
            _ragdollModel.MainAnimator = (_ragdollModel.MainAnimator ?? GetComponent<Animator>());
            _ragdollModel.MainRigidbody = (_ragdollModel.MainRigidbody ?? GetComponent<Rigidbody>());
            _ragdollModel.MainCollider = (_ragdollModel.MainCollider ?? GetComponent<CapsuleCollider>());

            _ragdollModel.Rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
            _ragdollModel.Colliders = GetComponentsInChildren<Collider>().ToList();
            Ragdolled = false;
        }

        public bool Ragdolled {
            get { return _ragdollModel.State == RagdollState.Ragdolled; }
            set
            {
                if (value == true)
                {
                    _ragdollModel.Rigidbodies.ForEach(r => r.isKinematic = false);
                    _ragdollModel.Colliders.ForEach(c => c.enabled = true);
                    _ragdollModel.MainRigidbody.isKinematic = true;
                    _ragdollModel.MainAnimator.enabled = false;
                    _ragdollModel.MainCollider.enabled = false;
                    _ragdollModel.State = RagdollState.Ragdolled;
                }
                else
                {
                    _ragdollModel.Rigidbodies.ForEach(r => r.isKinematic = true);
                    _ragdollModel.Colliders.ForEach(c => c.enabled = false);
                    _ragdollModel.MainRigidbody.isKinematic = false;
                    _ragdollModel.MainAnimator.enabled = true;
                    _ragdollModel.MainCollider.enabled = true;
                    _ragdollModel.State = RagdollState.Animated;
                }
            }
        }

        public void FixedUpdate()
        {
            if (Ragdolled == _ragdollModel.MainAnimator.isActiveAndEnabled)
                Ragdolled = !_ragdollModel.MainAnimator.isActiveAndEnabled;
        }
    }
}
