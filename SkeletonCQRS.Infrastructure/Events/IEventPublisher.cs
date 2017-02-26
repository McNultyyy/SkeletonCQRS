namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent e) where TEvent : IDomainEvent;
    }

    public class EventPublisher : IEventPublisher
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public EventPublisher(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Publish<TEvent>(TEvent e) where TEvent : IDomainEvent
        {
            var handlers = _eventHandlerFactory.GetFor<TEvent>();
            foreach (var handler in handlers)
            {
                handler.Handle(e);
            }
        }
    }
}