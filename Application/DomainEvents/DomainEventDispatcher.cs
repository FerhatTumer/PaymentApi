using Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Common
{
    public class DomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Dispatch(IEnumerable<DomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var eventType = domainEvent.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                var handlers = _serviceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    var handleMethod = handlerType.GetMethod("Handle");
                    await (Task)handleMethod.Invoke(handler, new[] { domainEvent });
                }
            }
        }
    }
}