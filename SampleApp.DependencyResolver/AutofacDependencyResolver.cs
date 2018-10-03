﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SampleApp.Connection;
using SampleApp.Connection.Interfaces;
using SampleApp.Repository.Interfaces;
using SampleApp.Repository.Implementations;
using SampleApp.Services.Interfaces;
using SampleApp.Services.Implementations;

namespace SampleApp.DependencyResolver
{
    public class AutofacDependencyResolver
    {
        public static IContainer container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(LawAcceptanceRepository))
                .As(typeof(ILawAcceptanceRepository))
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(LawAcceptanceService))
                .As(typeof(ILawAcceptanceService))
                .InstancePerRequest();

            container = builder.Build();

            return container;
        }
    }
}
