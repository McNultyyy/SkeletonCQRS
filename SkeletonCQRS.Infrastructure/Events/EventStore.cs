using SkeletonCQRS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using SkeletonCQRS.Data.Entities;

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
        private readonly IEventPublisher _eventPublisher;

        public EventStore(IRepository<EventEntity, Guid> repository, IEventSerializer serializer, IEventTypeFactory typeFactory, IEventPublisher eventPublisher)
        {
            _repository = repository;
            _serializer = serializer;
            _typeFactory = typeFactory;
            _eventPublisher = eventPublisher;
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
            foreach (var domainEvent in events)
            {
                var eventEntity = ToEventEntity(domainEvent);
                _repository.Add(eventEntity);
                _eventPublisher.Publish(domainEvent);
            }
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