using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SkeletonCQRS.Domain.Aggregates;
using SkeletonCQRS.Domain.Events;
using SkeletonCQRS.Infrastructure.Events;

namespace SkeletonCQRS.Domain.Test
{
    [TestFixture]
    public class DomainEventTests
    {
        private static IEnumerable<Type> AllDomainEventTypes()
        {
            var domainEventInterfaceType = typeof(DomainEvent);
            var domainEventTypes = Assembly.GetAssembly(typeof(InventoryItem)).GetTypes()
                .Where(x => domainEventInterfaceType.IsAssignableFrom(x));
            return domainEventTypes;
        }

        [Test]
        public void AllDomainEventsCanBeSerializedAndDeserializedCorrectly()
        {
            var serializer = new EventXmlSerializer();
            var domainEventTypes = AllDomainEventTypes();

            foreach (var domainEventType in domainEventTypes)
            {
                var domainInstance = Activator.CreateInstance(domainEventType, true) as IDomainEvent;
                var serializedInstance = serializer.Serialize(domainInstance);
                var deserializedInstance = serializer.Deserialize(serializedInstance, domainEventType);
                Assert.AreEqual(domainInstance, deserializedInstance);
                Console.WriteLine("{0} serialized and deserialized correctly.", domainEventType);
            }
        }

        [Test]
        public void AllDomainEventsAreImmutable()
        {
            var domainEventTypes = AllDomainEventTypes();

            foreach (var domainEventType in domainEventTypes)
            {
                var constructors = domainEventType.GetConstructors();
                Assert.That(constructors.Length, Is.EqualTo(1));
            }
        }
    }
}