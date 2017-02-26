using System.Collections.Generic;
using Microsoft.Practices.Unity;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Web.Infrastructure
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        public IEnumerable<IEventHandler<TEvent>> GetFor<TEvent>() where TEvent : IDomainEvent
        {
            var instances = UnityConfig.GetConfiguredContainer().ResolveAll<IEventHandler<TEvent>>();
            return instances;
        }
    }
}