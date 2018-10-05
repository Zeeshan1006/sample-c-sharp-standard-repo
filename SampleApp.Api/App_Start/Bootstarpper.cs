using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SampleApp.Connection;
using SampleApp.Connection.Interfaces;
using SampleApp.Repository.Implementations;
using SampleApp.Repository.Interfaces;
using SampleApp.Services.Implementations;
using SampleApp.Services.Interfaces;

namespace SampleApp.Api
{
    public static class Bootstarpper
    {
        public static void Run()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DbConnectionFactory>()
               .As<IDbConnectionFactory>();

            builder.RegisterType<LawAcceptanceRepository>()
                .As<ILawAcceptanceRepository>();

            builder.RegisterType<LawAcceptanceService>()
                .As<ILawAcceptanceService>();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}