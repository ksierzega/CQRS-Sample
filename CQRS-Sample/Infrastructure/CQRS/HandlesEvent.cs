namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface HandlesEvent<in T> where T : IEvent
    {
        void Handle(T message);
    }
}
