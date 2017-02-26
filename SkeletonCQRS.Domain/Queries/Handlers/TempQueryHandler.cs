using SkeletonCQRS.Infrastructure.Queries;

namespace SkeletonCQRS.Domain.Queries.Handlers
{
    public class TempQueryHandler : IQueryHandler<TempQuery, int>
    {
        public int Handle(TempQuery query)
        {
            return 0;
        }
    }
}