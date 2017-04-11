using NLog;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public class QueryExecutor : IQueryExecutor
    {
//        private readonly ISession _session;
        private readonly ILogger _logger;

        public QueryExecutor(
//            ISession session,
            ILogger logger)
        {
//            _session = session;
            _logger = logger;
        }

        public TResult Execute<TResult>(NHibernateQuery<TResult> query)
        {
//            query.Session = _session;
            query.Logger = _logger;
            _logger.Info("Executing query: {0}", query.GetType().Name);
            return query.Execute();
        }
    }
}
