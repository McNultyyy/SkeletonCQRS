using System;
using SkeletonCQRS.Infrastructure.Commands;

namespace SkeletonCQRS.Domain.Commands
{
    public class CreateInventoryItem : ICommand
    {
        public Guid InventoryItemId { get; }
        public string InventoryItemName { get; }
        public CreateInventoryItem(Guid inventoryItemId, string inventoryItemName)
        {
            InventoryItemId = inventoryItemId;
            InventoryItemName = inventoryItemName;
        }
    }
}