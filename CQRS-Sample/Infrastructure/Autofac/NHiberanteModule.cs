namespace CQRS_Sample.Infrastructure.Autofac
{
  /*  public class NHiberanteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NHibernateSessionProvider>()
                .As<INHibernateSessionProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<INHibernateSessionProvider>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
        }
    }*/
}