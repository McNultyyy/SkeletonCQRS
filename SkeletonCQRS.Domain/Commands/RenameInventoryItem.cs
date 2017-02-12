using System;
using SkeletonCQRS.Infrastructure.Commands;

namespace SkeletonCQRS.Domain.Commands
{
    public class RenameInventoryItem : ICommand
    {
        public Guid InventoryItemId { get; }
        public string NewName { get; set; }

        public RenameInventoryItem(Guid inventoryItemId, string newName)
        {
            InventoryItemId = inventoryItemId;
            NewName = newName;
        }
    }
}