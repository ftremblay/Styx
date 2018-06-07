using RageCure.Commons.ReduxUtils;
using RageCure.EventUtils;
using System;

namespace RageCure.ReduxUtils
{
    public class Store<TState, TAction>
    {
        private TState _state;
        private IReducer<TState, TAction>[] _reducers;
        private EventAggregator _eventAggregator;

        private Store(IReducer<TState, TAction>[] reducers, TState initState)
        {
            _state = initState;
            _reducers = reducers;
            _eventAggregator = new EventAggregator();
        }

        public static Store<TState, TAction> CreateStore(IReducer<TState, TAction>[] reducers, TState initState)
        {
            return new Store<TState, TAction>(reducers, initState);
        }

        public TState State
        {
            get { return _state; }
        }

        public void Dispatch(TAction action)
        {
            foreach (var r in _reducers)
            {
                _state = r.Reduce(_state, action);
            }

            _eventAggregator.Publish(StateChanged<TState>.Create(_state));
        }

        public void Subscribe(object o) {
            _eventAggregator.Subscribe(o);
        }

        
    }

    public class StateChanged<TState>
    {
        public TState State;

        public static StateChanged<TState> Create(TState state)
        {
            if (state == null)
                throw new Exception("State is null");

            return new StateChanged<TState>(state);
        }

        private StateChanged(TState state)
        {
            State = state;
        }
    }
}
