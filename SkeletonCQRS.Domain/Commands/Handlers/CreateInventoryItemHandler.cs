using SkeletonCQRS.Domain.Aggregates;
using SkeletonCQRS.Infrastructure.Aggregates;
using SkeletonCQRS.Infrastructure.Commands;

namespace SkeletonCQRS.Domain.Commands.Handlers
{
    public class CreateInventoryItemHandler : ICommandHandler<CreateInventoryItem>
    {
        private readonly IAggregateRepository<InventoryItem> _repository;

        public CreateInventoryItemHandler(IAggregateRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateInventoryItem command)
        {
            var aggregate = new InventoryItem(command.InventoryItemId, command.InventoryItemName);
            _repository.Save(aggregate);
        }
    }
}