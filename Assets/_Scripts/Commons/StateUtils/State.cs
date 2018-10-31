using Styx.Entities;
using Styx.Entities.PlayerModule;
using Styx.Managers;
using UnityEngine;

namespace RageCure.StateUtils
{
    public abstract class State<T> : MonoBehaviour
    {
        protected PlayerId PlayerId;

        public void Start()
        {
            PlayerId = (PlayerId ?? GetComponent<PlayerId>());
        }

        public abstract void Enter(T entity);
        public abstract void Execute(T entity);
        public abstract void FixedExecute(T entity);
        public abstract void Exit(T entity);
    }
}
