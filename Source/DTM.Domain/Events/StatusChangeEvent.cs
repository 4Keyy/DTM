using DTM.Domain.Common;

namespace DTM.Domain.Events
{
    public class StatusChangeEvent<T> : BaseEvent where T : class
    {
        public StatusChangeEvent(T item)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}
