using UnityEngine;

namespace RageCure.StateUtils
{
    public abstract class State<T> : MonoBehaviour
    {
        public abstract void Enter(T entity);
        public abstract void Execute(T entity);
        public abstract void FixedExecute(T entity);
        public abstract void Exit(T entity);
    }
}
