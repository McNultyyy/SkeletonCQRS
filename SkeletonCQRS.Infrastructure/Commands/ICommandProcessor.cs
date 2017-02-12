namespace SkeletonCQRS.Infrastructure.Commands
{
    public interface ICommandProcessor
    {
        void Process<TCommand>(TCommand command) where TCommand : ICommand;
    }
}