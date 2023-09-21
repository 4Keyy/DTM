using DTM.Domain.Common;

namespace DTM.Domain.Events
{
    public class CompletedEvent<T> : BaseEvent where T : class
    {
        public CompletedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; } 
    }
}
