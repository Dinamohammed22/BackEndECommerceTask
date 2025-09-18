using Autofac;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Middlewares;

namespace KOG.ECommerce.Configurations;
public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<>)).InstancePerLifetimeScope();
        builder.RegisterType(typeof(Entities)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(EndpointBaseParameters<>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(RequestHandlerBaseParameters<>)).InstancePerLifetimeScope();
        builder.RegisterType<TransactionMiddleware>().InstancePerLifetimeScope();
        builder.RegisterType<UserState>().InstancePerLifetimeScope();
    }
}