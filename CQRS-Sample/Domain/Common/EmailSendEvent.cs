using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Domain.Common
{
    internal class EmailSendEvent : IEvent
    {
        public string Message { get; private set; }

        public EmailSendEvent(string message)
        {
            Message = message;
        }
    }
}