using DTM.Domain.Common;

namespace DTM.Domain.Events
{
    public class DeletedEvent<T> : BaseEvent where T : class 
    {
        public DeletedEvent(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
