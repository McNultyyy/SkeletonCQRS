using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        protected DomainEvent()
        {
        }

        protected DomainEvent(Guid aggregateId, int aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }

        protected bool Equals(DomainEvent other)
        {
            foreach (var equalityProperty in EqualityProperties())
            {
                var myValue = equalityProperty.GetValue(this);
                var theirValue = equalityProperty.GetValue(other);
                if (!Equals(myValue, theirValue))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DomainEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 0;
                var counter = 0;
                foreach (var equalityProperty in EqualityProperties())
                {
                    if (counter == 0)
                        hashcode = equalityProperty.GetValue(this)?.GetHashCode() ?? 0;
                    else
                        hashcode = (hashcode * 397) ^ equalityProperty.GetValue(this)?.GetHashCode() ?? 0;
                    counter++;
                }
                return hashcode;
            }
        }

        private IEnumerable<PropertyInfo> EqualityProperties()
        {
            return GetType().GetProperties();
        }
    }
}