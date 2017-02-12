using System;
using System.ComponentModel;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Events
{
    [Serializable]
    public class ItemsRemovedFromInventory : DomainEvent
    {
        public int Count { get; set; }

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private ItemsRemovedFromInventory() { }

        public ItemsRemovedFromInventory(Guid aggregateId, int aggregateVersion, int count) : base(aggregateId, aggregateVersion)
        {
            Count = count;
        }
    }
}