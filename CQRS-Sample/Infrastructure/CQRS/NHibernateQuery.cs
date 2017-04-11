using NLog;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public abstract class NHibernateQuery<TResult> : IQuery<TResult>
    {
//        public ISession Session { get; set; }
        public ILogger Logger { get; set; }

        public abstract TResult Execute();
    }
}

