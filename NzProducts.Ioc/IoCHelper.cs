using Autofac;
using System;
using System.Reflection;
using log4net;
using NzProducts.Business.Products;

namespace NzProducts.Ioc
{
    public class IoCHelper
    {
        public static IContainer BuildContainer(Type type, ILog logger)
        {
            return BuildContainer(type, builder =>
            {
                builder.RegisterInstance(logger).As<ILog>();
                return builder;
            });
        }

        public static IContainer BuildContainer(Type type,
            Func<ContainerBuilder, ContainerBuilder> additionalRegistration)
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly assembly = Assembly.GetAssembly(type);
            builder.RegisterAssemblyTypes(assembly)
                .Where(a => a.FullName?.StartsWith("NzProducts") ?? false)
                .AsImplementedInterfaces();
            builder.RegisterType<BzProduct>().AsImplementedInterfaces();
            builder = additionalRegistration(builder);
            IContainer container = builder.Build();
            return container;
        }
    }
}
