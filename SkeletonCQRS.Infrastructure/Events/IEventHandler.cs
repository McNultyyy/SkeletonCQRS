namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent e);
    }
}