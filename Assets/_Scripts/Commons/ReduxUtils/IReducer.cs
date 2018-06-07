using Assets._Scripts;

namespace RageCure.Commons.ReduxUtils
{
    public interface IReducer<T, A>
    {
        T Reduce(T state, A action);
    }
}
