using System;
using System.ComponentModel;
using SkeletonCQRS.Domain.Events;
using SkeletonCQRS.Infrastructure.Aggregates;

namespace SkeletonCQRS.Domain.Aggregates
{
    public class InventoryItem : AggregateRoot
    {
        private bool _activated;
        private string _name;
        private int _itemsCheckedIn;

        [Obsolete]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private InventoryItem() { }

        public InventoryItem(Guid id, string name)
        {
            ApplyChange(new InventoryItemCreated(id, Version, name));
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException(nameof(newName));
            var oldName = _name;
            ApplyChange(new InventoryItemRenamed(Id, Version, oldName, newName));
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            ApplyChange(new ItemsRemovedFromInventory(Id, Version, count));
        }

        public void CheckIn(int count)
        {
            if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");
            ApplyChange(new ItemsCheckedInToInventory(Id, Version, count));
        }

        public void Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("already deactivated");
            ApplyChange(new InventoryItemDeactivated(Id, Version));
        }

        #region Apply
        private void Apply(InventoryItemCreated e)
        {
            Id = e.AggregateId;
            Version = 0;

            _activated = true;
            _name = e.InventoryItemName;
            _itemsCheckedIn = 0;
        }

        private void Apply(InventoryItemDeactivated e)
        {
            _activated = false;
        }

        private void Apply(ItemsCheckedInToInventory e)
        {
            _itemsCheckedIn += e.ItemsCheckedIn;
        }

        private void Apply(ItemsRemovedFromInventory e)
        {
            _itemsCheckedIn -= e.Count;
        }


        private void Apply(InventoryItemRenamed e)
        {
            _name = e.NewName;
        }
        #endregion
    }
}