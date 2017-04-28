using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Domain.Customers
{
    internal class RegisterCustomerHandler : Handles<RegisterCustomerCommand>
    {
        private readonly IEventPublisher _eventPublisher;

        public RegisterCustomerHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public CommandResult Handle(RegisterCustomerCommand message)
        {
            // todo save in db
            
            _eventPublisher.Publish(new CustomerRegisteredEvent(10));

            return  CommandResult.Success();
        }
    }
}
