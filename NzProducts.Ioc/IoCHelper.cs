using Autofac;
using System;
using System.Reflection;
using log4net;
using NzProducts.Business.Products;
using NzProducts.Business.Products.Mappers;
using RefactorMe.DontRefactor.Data.Implementation;

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
            builder.RegisterAssemblyTypes(assembly)
                .Where(a => a.FullName?.StartsWith("RefactorMe") ?? false)
                .AsImplementedInterfaces();

            builder.RegisterType<LawnMowerMapper>()
                .AsImplementedInterfaces();
            builder.RegisterType<PhoneCaseMapper>()
                  .AsImplementedInterfaces();
            builder.RegisterType<ShirtMapper>()
                .AsImplementedInterfaces();

            builder.RegisterType<BzProduct>().AsImplementedInterfaces();
            builder.RegisterType<TShirtRepository>().AsImplementedInterfaces();
            builder.RegisterType<LawnmowerRepository>().AsImplementedInterfaces();
            builder.RegisterType<PhoneCaseRepository>().AsImplementedInterfaces();

            builder = additionalRegistration(builder);
            IContainer container = builder.Build();
            return container;
        }
    }
}
