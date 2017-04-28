using CQRS_Sample.Domain.Customers;
using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Domain.Common
{
    internal class LogEmailHandler : HandlesEvent<EmailSendEvent>
    {
        public void Handle(EmailSendEvent message)
        {
            //todo log email
        }
    }
}