using System;
using System.ComponentModel;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Events
{
    [Serializable]
    public class InventoryItemDeactivated : DomainEvent
    {
        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private InventoryItemDeactivated() { }

        public InventoryItemDeactivated(Guid aggregateId, int aggregateVersion) : base(aggregateId, aggregateVersion) { }
    }
}