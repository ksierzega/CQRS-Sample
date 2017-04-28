using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Domain.Customers
{
    internal class CustomerRegisteredEvent : IEvent
    {
        public int Id { get; private set; }

        public CustomerRegisteredEvent(int id)
        {
            Id = id;
        }
    }
}