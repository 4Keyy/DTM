using DTM.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DTM.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public DispatchDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null)
            {
                return;
            }

            var entities = context.ChangeTracker.Entries<BaseEntity>()
                .Where(ents => ents.Entity.DomainEvents.Count != 0)
                .Select(ents => ents.Entity);

            var domainEvents = entities.SelectMany(domainEnts => domainEnts.DomainEvents).ToList();

            entities.ToList().ForEach(ents => ents.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
