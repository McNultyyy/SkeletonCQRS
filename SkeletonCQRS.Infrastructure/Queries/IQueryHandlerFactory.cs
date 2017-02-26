using Microsoft.Practices.Unity;

namespace SkeletonCQRS.Infrastructure.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResult> GetFor<TQuery, TResult>() where TQuery : IQuery<TResult>;
    }
}