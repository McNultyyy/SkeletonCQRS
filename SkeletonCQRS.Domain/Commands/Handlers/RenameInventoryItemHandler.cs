using SkeletonCQRS.Domain.Aggregates;
using SkeletonCQRS.Infrastructure.Aggregates;
using SkeletonCQRS.Infrastructure.Commands;

namespace SkeletonCQRS.Domain.Commands.Handlers
{
    public class RenameInventoryItemHandler : ICommandHandler<RenameInventoryItem>
    {
        private readonly IAggregateRepository<InventoryItem> _repository;

        public RenameInventoryItemHandler(IAggregateRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(RenameInventoryItem command)
        {
            var inventoryItem = _repository.GetById(command.InventoryItemId);
            inventoryItem.ChangeName(command.NewName);
            _repository.Save(inventoryItem);
        }
    }
}