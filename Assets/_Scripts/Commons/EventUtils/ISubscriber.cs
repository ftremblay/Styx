namespace RageCure.EventUtils
{
    public interface ISubscriber<TEventType>
    {
        void OnEvent(TEventType e);
    }

}
