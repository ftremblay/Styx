namespace RageCure.StateUtils
{
    public class StateMachine<T>
    {
        private T _entity;
        public State<T> CurrentState { get; set; }
        public State<T> PreviousState { get; set; }
        public State<T> GlobalState { get; set; }
        public StateMachine(T entity)
        {
            _entity = entity;
        }

        public void Update()
        {
            if (GlobalState != null)
                GlobalState.Execute(_entity);
            if (CurrentState != null)
                CurrentState.Execute(_entity);
        }
        public void FixedUpdate()
        {
            if (GlobalState != null)
                GlobalState.FixedExecute(_entity);
            if (CurrentState != null)
                CurrentState.FixedExecute(_entity);
        }

        public void ChangeState(State<T> newState)
        {
            if (newState == null) return;
            PreviousState = CurrentState;
            CurrentState.Exit(_entity);
            CurrentState = newState;
            CurrentState.Enter(_entity);
        }
        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }
        public bool IsInState(State<T> state)
        {
            return state == CurrentState;
        }
    }

}
