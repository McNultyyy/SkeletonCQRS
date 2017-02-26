namespace SkeletonCQRS.Infrastructure.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> GetFor<TCommand>() where TCommand : ICommand;
    }
}