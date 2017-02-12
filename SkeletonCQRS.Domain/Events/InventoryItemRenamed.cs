using System;
using System.ComponentModel;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Events
{
    [Serializable]
    public class InventoryItemRenamed : DomainEvent
    {
        public string OldName { get; set; }
        public string NewName { get; set; }

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private InventoryItemRenamed() { }

        public InventoryItemRenamed(Guid aggregateId, int aggregateVersion, string oldName, string newName) : base(aggregateId, aggregateVersion)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}