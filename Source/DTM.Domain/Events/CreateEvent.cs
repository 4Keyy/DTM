using DTM.Domain.Common;

namespace DTM.Domain.Events
{
    public class CreateEvent<T> : BaseEvent where T : class
    {
        public CreateEvent(T item)
        { 
            Item = item;
        }

        public T Item { get; }
    }
}
