using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using NLog;

namespace CQRS_Sample.Infrastructure.Autofac
{
    public class LoggerModule : Module
    {
        protected ILogger CreateLoggerFor(Type type)
        {
            return LogManager.GetLogger(type.FullName);
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
         {
             var type = registration.Activator.LimitType;
             if (HasPropertyDependencyOnLogger(type))
             {
                 registration.Activated += InjectLoggerViaProperty;
             }

             if (HasConstructorDependencyOnLogger(type))
             {
                 registration.Preparing += InjectLoggerViaConstructor;
             }
         }

         private bool HasPropertyDependencyOnLogger(Type type)
         {
             return type.GetProperties().Any(property => property.CanWrite && property.PropertyType == typeof (ILogger));
         }

         private bool HasConstructorDependencyOnLogger(Type type)
         {
             return type.GetConstructors()
                        .SelectMany(
                            constructor => constructor.GetParameters()
                                                      .Where(parameter => parameter.ParameterType == typeof (ILogger)))
                        .Any();
         }

         private void InjectLoggerViaProperty(object sender, ActivatedEventArgs<object> @event)
         {
             var type = @event.Instance.GetType();
             var propertyInfo = type.GetProperties().First(x => x.CanWrite && x.PropertyType == typeof (ILogger));
             propertyInfo.SetValue(@event.Instance, CreateLoggerFor(type), null);
         }

         private void InjectLoggerViaConstructor(object sender, PreparingEventArgs @event)
         {
             var type = @event.Component.Activator.LimitType;
             @event.Parameters = @event.Parameters.Union(
                 new[]
                 {
                     new ResolvedParameter((parameter, context) => parameter.ParameterType == typeof (ILogger), (p, i) => CreateLoggerFor(type))
                 });
         }
    }
}