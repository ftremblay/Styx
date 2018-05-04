using RageCure.EventUtils;
using System;

namespace RageCure.ReduxUtils
{
    public class Store<TState>
    {
        private TState _state;
        private Func<TState, object, TState>[] _reducers;
        private EventAggregator _eventAggregator;

        private Store(Func<TState, object, TState>[] reducers, TState initState)
        {
            _state = initState;
            _reducers = reducers;
            _eventAggregator = new EventAggregator();
        }

        public static Store<TState> CreateStore(Func<TState, object, TState>[] reducers, TState initState)
        {
            return new Store<TState>(reducers, initState);
        }

        public TState State
        {
            get { return _state; }
        }

        public void Dispatch(object action)
        {
            foreach (var r in _reducers)
            {
                _state = r(_state, action);
            }
            _eventAggregator.Publish(action);
        }

        public void Subscribe(object listener)
        {
            _eventAggregator.Subscribe(listener);
        }
    }

    public interface IStoreReducer<TState>
    {
        TState Reduce<TAction>(TState state, TAction action);
    }

}
