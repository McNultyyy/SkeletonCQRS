using SkeletonCQRS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using SkeletonCQRS.Data.Entities;
using SkeletonCQRS.Domain.Helpers;

namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventStore
    {
        IEnumerable<IDomainEvent> GetEventsFor(Guid aggregateId);
        void SaveEvents(IEnumerable<IDomainEvent> events);
    }
    public class EventStore : IEventStore
    {
        private readonly IRepository<EventEntity, Guid> _repository;
        private readonly IEventSerializer _serializer;
        private readonly IEventTypeFactory _typeFactory;

        public EventStore(IRepository<EventEntity, Guid> repository, IEventSerializer serializer, IEventTypeFactory typeFactory)
        {
            _repository = repository;
            _serializer = serializer;
            _typeFactory = typeFactory;
        }

        public IEnumerable<IDomainEvent> GetEventsFor(Guid aggregateId)
        {
            var results = _repository.Get(x => x.AggregateId == aggregateId).ToList();

            foreach (var eventEntity in results)
            {
                var type = _typeFactory.GetFor(eventEntity.EventName);
                var domainEvent = _serializer.Deserialize(eventEntity.EventData, type);
                yield return domainEvent;
            }
        }

        public void SaveEvents(IEnumerable<IDomainEvent> events)
        {
            var entityEvents = events.Select(ToEventEntity).ToList();
            _repository.AddRange(entityEvents);
        }

        private EventEntity ToEventEntity(IDomainEvent e)
        {
            var eType = e.GetType().Name;
            var eData = _serializer.Serialize(e);

            var entity = new EventEntity(
                e.EventId, e.TimeStamp, e.AggregateId, eType, eData
            );

            return entity;
        }
    }
}