using DTM.Domain.Common;
using Microsoft.Extensions.Logging;

namespace DTM.Application.Common.BaseEventHandlers
{
    public class DeleteEventHandler<T> : INotificationHandler<T> where T : BaseEvent
    {
        private readonly ILogger<DeleteEventHandler<T>> _logger;

        public DeleteEventHandler(ILogger<DeleteEventHandler<T>> logger)
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
