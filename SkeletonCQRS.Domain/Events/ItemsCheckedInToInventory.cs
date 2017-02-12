using System;
using System.ComponentModel;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Events
{
    [Serializable]
    public class ItemsCheckedInToInventory : DomainEvent
    {
        public int ItemsCheckedIn { get; set; }

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private ItemsCheckedInToInventory() { }

        public ItemsCheckedInToInventory(Guid aggregateId, int aggregateVersion, int itemsCheckedIn) : base(aggregateId, aggregateVersion)
        {
            ItemsCheckedIn = itemsCheckedIn;
        }

    }
}