using System.Threading.Tasks;

namespace SkeletonCQRS.Infrastructure.Commands.Handlers
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandlerAsync<in TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}