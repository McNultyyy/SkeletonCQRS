using Microsoft.Practices.Unity;
using SkeletonCQRS.Infrastructure.Commands;

namespace SkeletonCQRS.Web.Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<TCommand> GetFor<TCommand>() where TCommand : ICommand
        {
            var instance = UnityConfig.GetConfiguredContainer().Resolve<ICommandHandler<TCommand>>();
            return instance;
        }
    }
}