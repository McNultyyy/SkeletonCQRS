namespace SkeletonCQRS.Infrastructure.Queries.Handlers
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}