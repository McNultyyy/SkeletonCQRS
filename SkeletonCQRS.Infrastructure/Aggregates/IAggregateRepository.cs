using System;
using System.Linq;
using System.Runtime.InteropServices;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Infrastructure.Aggregates
{
    public interface IAggregateRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        TAggregate GetById(Guid id);
        void Save(TAggregate aggregate);
    }

    public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly IEventStore _eventStore;

        public AggregateRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public TAggregate GetById(Guid id)
        {
            var events = _eventStore.GetEventsFor(id).ToList();
            var aggregate = Activator.CreateInstance<TAggregate>();
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public void Save(TAggregate aggregate)
        {
            var events = aggregate.FlushUncommitedChanges();
            _eventStore.SaveEvents(events);
        }
    }
}