using System.Collections.Generic;

namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<TEvent>> GetFor<TEvent>() where TEvent : IDomainEvent;
    }
}