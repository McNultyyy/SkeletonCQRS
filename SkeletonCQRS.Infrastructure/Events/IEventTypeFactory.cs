using System;
using System.Linq;

namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventTypeFactory
    {
        Type GetFor(string eventName);
    }

    public class EventTypeFactory : IEventTypeFactory
    {
        public Type GetFor(string eventName)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());
            var result = types.FirstOrDefault(x => x.Name == eventName);
            return result;
        }
    }
}