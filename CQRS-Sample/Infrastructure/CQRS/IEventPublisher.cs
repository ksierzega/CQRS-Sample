namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}