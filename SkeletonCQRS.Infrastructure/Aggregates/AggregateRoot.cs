using System;
using System.Collections.Generic;
using System.Linq;
using SkeletonCQRS.Infrastructure.Events;
using SkeletonCQRS.Infrastructure.Extensions;

namespace SkeletonCQRS.Infrastructure.Aggregates
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, false);
            }
        }

        public IEnumerable<IDomainEvent> FlushUncommitedChanges()
        {
            var changes = _changes.ToList();

            var i = 0;
            foreach (var change in changes)
            {
                i++;
            }
            Version = Version + i;
            _changes.Clear();

            return changes;
        }

        protected void ApplyChange(IDomainEvent @event, bool isNew = true)
        {
            this.AsDynamic().Apply(@event);
            if (isNew)
            {
                _changes.Add(@event);
            }
            else
            {
                Version++;
            }
        }
    }
}