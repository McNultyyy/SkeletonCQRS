using Microsoft.Practices.Unity;
using SkeletonCQRS.Infrastructure.Commands.Handlers;

namespace SkeletonCQRS.Infrastructure.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> GetFor<TCommand>() where TCommand : ICommand;
    }
}