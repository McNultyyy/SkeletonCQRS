using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Events
{
    [Serializable]
    public class InventoryItemCreated : DomainEvent
    {
        public string InventoryItemName { get; set; }

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private InventoryItemCreated() { }

        public InventoryItemCreated(Guid aggregateId, int aggregateVersion, string inventoryItemName) : base(aggregateId, aggregateVersion)
        {
            InventoryItemName = inventoryItemName;
        }
    }
}