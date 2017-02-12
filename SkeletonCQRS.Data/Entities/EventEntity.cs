using SkeletonCQRS.Data.Entities;
using System;
using System.Xml.Linq;

namespace SkeletonCQRS.Data.Entities
{
    public class EventEntity : Entity
    {
        public EventEntity() { }

        public EventEntity(Guid id, DateTime timeStamp, Guid aggregateId, string eventName, string eventData)
        {
            Id = id;
            TimeStamp = timeStamp;
            AggregateId = aggregateId;
            EventName = eventName;
            EventData = eventData;
        }

        public DateTime TimeStamp { get; set; }
        public Guid AggregateId { get; set; }
        public string EventName { get; set; }
        public string EventData { get; set; }
    }
}