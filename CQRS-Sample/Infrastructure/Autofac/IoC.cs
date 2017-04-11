using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using AutoMapper;
using CQRS_Sample.Infrastructure.Extensions;
using Autofac.Integration.WebApi;
using CQRS_Sample.Infrastructure.CQRS;

namespace CQRS_Sample.Infrastructure.Autofac
{
    internal static class IoC
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    throw new ContainerNotInitializedException("Container not configured");
                }

                return _container;
            }
        }

        public static void Configure(HttpConfiguration config, string binDirectory)
        {
            if (_container != null)
            {
                throw new InvalidOperationException("IoC already configured");
            }

            foreach (string filePath in Directory.GetFiles(binDirectory, "*.dll"))
            {
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }

            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            ConfigureModules(builder);
            builder.RegisterAssemblyModules();
            
            Register(builder);

            _container = builder.Build();

            // Set the dependency resolver to be Autofac.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            AutoMapperExtensions.MapperConfiguration = Container.Resolve<MapperConfiguration>();
        }

        private static void ConfigureModules(ContainerBuilder builder)
        {
            foreach (var moduleType in AppDomain.CurrentDomain.GetAppTypes()
                                                .Where(x => x.AssignableTo<IModule>() && x.CanBeInstantiated()))
            {
                IModule instance = Activator.CreateInstance(moduleType) as IModule;
                builder.RegisterModule(instance);
            }
        }

        private static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<EventsCommandsBus>()
                .As<ICommandSender>()
                .As<IEventPublisher>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<QueryExecutor>()
                .As<IQueryExecutor>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAppAssemblies())
                   .AsClosedTypesOf(typeof(Handles<>))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAppAssemblies())
                   .AsClosedTypesOf(typeof(HandlesAsync<>))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAppAssemblies())
                   .AsClosedTypesOf(typeof(HandlesEvent<>))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
        }
    }
}
