using UnityEngine;

namespace Styx.Models
{
    [CreateAssetMenu(fileName = "Animator Model", menuName = "Models/Animator model")]
    public class AnimatorModel : ScriptableObject
    {
        [SerializeField]
        private Animator _animator;

        public Animator Animator
        {
            get { return _animator; }
            set { _animator = value; }
        }

        public void SetTrigger (string trigger)
        {
            _animator.SetTrigger(trigger);
        }

        public void SetFloat (string key, float value)
        {
            _animator.SetFloat(key, value);
        }

        public void Disable()
        {
            if (_animator.enabled)
                _animator.enabled = false;
        }

        public void Enable()
        {
            if (!_animator.enabled)
                _animator.enabled = true;
        }
    }
}
