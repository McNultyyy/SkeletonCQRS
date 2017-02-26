using Microsoft.Practices.Unity;
using SkeletonCQRS.Infrastructure.Queries;

namespace SkeletonCQRS.Web.Infrastructure
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        public IQueryHandler<TQuery, TResult> GetFor<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            var instance = UnityConfig.GetConfiguredContainer().Resolve<IQueryHandler<TQuery, TResult>>();
            return instance;
        }
    }
}