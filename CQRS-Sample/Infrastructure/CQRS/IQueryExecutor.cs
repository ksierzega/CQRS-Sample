namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(NHibernateQuery<TResult> query);
    }
}