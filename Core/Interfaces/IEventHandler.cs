namespace Core.Events
{
    public interface IEventHandler<TEvent> where TEvent : DomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}