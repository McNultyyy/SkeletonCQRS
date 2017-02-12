
namespace SkeletonCQRS.Infrastructure.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandProcessor(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public void Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _factory.GetFor<TCommand>();
            handler.Handle(command);
        }

    }
}