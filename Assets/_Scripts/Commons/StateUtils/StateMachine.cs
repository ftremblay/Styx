namespace RageCure.StateUtils
{
    public class StateMachine<T>
    {
        public State<T> CurrentState { get; set; }
        public State<T> PreviousState { get; set; }
        public State<T> GlobalState { get; set; }
        public StateMachine(State<T> currentState)
        {
            CurrentState = currentState;
        }

        public void Update(T entity)
        {
            if (GlobalState != null)
                GlobalState.Execute(entity);
            if (CurrentState != null)
                CurrentState.Execute(entity);
        }
        public void FixedUpdate(T entity)
        {
            if (GlobalState != null)
                GlobalState.FixedExecute(entity);
            if (CurrentState != null)
                CurrentState.FixedExecute(entity);
        }

        public void ChangeState(T entity, State<T> newState)
        {
            if (newState == null) return;
            PreviousState = CurrentState;
            CurrentState.Exit(entity);
            CurrentState = newState;
            CurrentState.Enter(entity);
        }
        public void RevertToPreviousState(T entity)
        {
            ChangeState(entity, PreviousState);
        }
        public bool IsInState(State<T> state)
        {
            return state == CurrentState;
        }
    }

}
