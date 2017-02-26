using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SkeletonCQRS.Data.Repositories;
using SkeletonCQRS.Domain.Commands;
using SkeletonCQRS.Domain.Queries;
using SkeletonCQRS.Infrastructure.Aggregates;
using SkeletonCQRS.Infrastructure.Commands;
using SkeletonCQRS.Infrastructure.Events;
using SkeletonCQRS.Infrastructure.Queries;
using SkeletonCQRS.Web.Extensions;
using SkeletonCQRS.Web.Infrastructure;

namespace SkeletonCQRS.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICommandProcessor, CommandProcessor>();
            container.RegisterType<IQueryProcessor, QueryProcessor>();

            container.RegisterType<ICommandHandlerFactory, CommandHandlerFactory>();
            container.RegisterType<IQueryHandlerFactory, QueryHandlerFactory>();

            container.RegisterType(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));
            container.RegisterType<IEventStore, EventStore>();

            container.RegisterType(typeof(IRepository<,>), typeof(GenericRepository<,>));

            container.RegisterType<IEventSerializer, EventXmlSerializer>();
            container.RegisterType<IEventTypeFactory, EventTypeFactory>();

            container.RegisterType<IEventHandlerFactory, EventHandlerFactory>();
            container.RegisterType<IEventPublisher, EventPublisher>();

            container.RegisterAllTypesForOpenGeneric(
                typeof(ICommandHandler<>),
                AppDomain.CurrentDomain.GetAssemblies()
            );

            container.RegisterAllTypesForOpenGeneric(
                typeof(IQueryHandler<,>),
                AppDomain.CurrentDomain.GetAssemblies()
            );

            container.RegisterAllTypesForOpenGeneric(
                typeof(IEventHandler<>),
                AppDomain.CurrentDomain.GetAssemblies()
            );
        }
    }
}
