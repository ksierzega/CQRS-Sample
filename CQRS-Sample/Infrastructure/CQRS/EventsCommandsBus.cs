using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public class EventsCommandsBus : ICommandSender, IEventPublisher
    {
        private const string NoHandlerErrorMessage = "No handler registered for command {0}";
        private const string TooManyHandlersErrorMessage = "Too many ({0}) handlers registered for command {1}, only one handler is allowed";

        private readonly IComponentContext _container;
        private readonly ILogger _logger;

        public EventsCommandsBus(IComponentContext container, ILogger logger)
        {
            _container = container;
            _logger = logger;
        }

        public CommandResult Send<T>(T command) where T : class
        {
            _logger.Info("Received command: {0}", command.GetType().Name);

            var handlers = _container.Resolve<IEnumerable<Handles<T>>>()
                .ToList();

            switch (handlers.Count)
            {
                case 1:
                    var handlerName = handlers[0].GetType().Name;
                    _logger.Info("Executing handler: {0}", handlerName);

                    CommandResult result = handlers[0].Handle(command);
                    
                    _logger.Info("Executed handler: {0}", handlerName);

                    return result;
                case 0:
                    throw new InvalidOperationException(string.Format(NoHandlerErrorMessage, command.GetType().Name));
                default:
                    throw new InvalidOperationException(string.Format(TooManyHandlersErrorMessage, handlers.Count, command.GetType().Name));
            }
        }

        public async Task<CommandResult> SendAsync<T>(T command) where T : class
        {
            _logger.Info("Received command: {0}", command.GetType().Name);

            var handlers = _container.Resolve<IEnumerable<HandlesAsync<T>>>()
                .ToList();

            switch (handlers.Count)
            {
                case 1:
                    var handlerName = handlers[0].GetType().Name;
                    _logger.Info("Executing handler: {0}", handlerName);

                    CommandResult result = await handlers[0].HandleAsync(command);

                    _logger.Info("Executed handler: {0}", handlerName);

                    return result;
                case 0:
                    throw new InvalidOperationException(string.Format(NoHandlerErrorMessage, command.GetType().Name));
                default:
                    throw new InvalidOperationException(string.Format(TooManyHandlersErrorMessage, handlers.Count, command.GetType().Name));
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            _logger.Info("Received event: {0}", @event.GetType().Name);
            var handlers = _container.Resolve<IEnumerable<HandlesEvent<T>>>()
                .ToList();

            Parallel.ForEach(handlers,
                h =>
                {
                    var handlerName = h.GetType().Name;
                    _logger.Info("Executing handler: {0}", handlerName);

                    h.Handle(@event);

                    _logger.Info("Executed handler: {0}", handlerName);
                });
        }
    }
}