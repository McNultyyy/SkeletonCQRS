using Microsoft.Practices.Unity;
using SkeletonCQRS.Infrastructure.Queries.Handlers;

namespace SkeletonCQRS.Infrastructure.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResult> GetFor<TQuery, TResult>() where TQuery : IQuery<TResult>;
    }
}