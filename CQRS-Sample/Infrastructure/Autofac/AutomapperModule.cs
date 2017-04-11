using System;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using CQRS_Sample.Infrastructure.Extensions;


namespace CQRS_Sample.Infrastructure.Autofac
{
    public class AutomapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAppAssemblies())
                .AssignableTo(typeof (Profile))
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}