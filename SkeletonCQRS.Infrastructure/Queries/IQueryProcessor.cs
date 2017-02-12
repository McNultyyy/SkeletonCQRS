namespace SkeletonCQRS.Infrastructure.Queries
{
    public interface IQueryProcessor
    {
        /// <summary>
        ///     Processes the specified query by finding the appropriate handler and invoking it.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to process.</param>
        /// <returns>The query result.</returns>
        TResult Process<TResult>(IQuery<TResult> query);
    }

    public class QueryProcessor : IQueryProcessor
    {
        private readonly IQueryHandlerFactory _factory;

        public QueryProcessor(IQueryHandlerFactory factory)
        {
            _factory = factory;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handler = _factory.GetFor<IQuery<TResult>, TResult>();
            var result = handler.Handle(query);
            return result;
        }
    }
}