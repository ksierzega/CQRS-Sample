using CQRS_Sample.Domain.Common;
using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Domain.Customers
{
    internal class  SendWelcomeEmailHandler : HandlesEvent<CustomerRegisteredEvent>
    {
        private readonly IEventPublisher _eventPublisher;

        public SendWelcomeEmailHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(CustomerRegisteredEvent message)
        {
            //todo send email

            _eventPublisher.Publish( new EmailSendEvent("Welcome"));
        }
    }
}