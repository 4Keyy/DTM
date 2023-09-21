using DTM.Domain.Common;
using Microsoft.Extensions.Logging;

namespace DTM.Application.Common.BaseEventHandlers
{
    public class CompletedEventHandler<T> : INotificationHandler<T> where T : BaseEvent
    {
        private readonly ILogger<CompletedEventHandler<T>> _logger;

        public CompletedEventHandler(ILogger<CompletedEventHandler<T>> logger)
        {
            _logger = logger;
        }

        public Task Handle(T notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DTM Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
