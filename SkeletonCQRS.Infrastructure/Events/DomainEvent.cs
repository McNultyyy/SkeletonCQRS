using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IDomainEvent : IMessage
    {
        Guid EventId { get; set; }
        Guid AggregateId { get; set; }
        int AggregateVersion { get; set; }
        DateTime TimeStamp { get; set; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected DomainEvent() { }
        protected DomainEvent(Guid aggregateId, int aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }
    }
}