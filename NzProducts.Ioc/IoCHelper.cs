using Autofac;
using System;
using System.Reflection;
using log4net;
using NzProducts.Business.Products;
using RefactorMe.DontRefactor.Models;
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
            builder.RegisterAssemblyTypes(typeof(BaseReadOnlyRepository<TShirt>).Assembly)
     .Where(t => t.IsSubclassOf(typeof(BaseReadOnlyRepository<TShirt>)))
     .As<BaseReadOnlyRepository<TShirt>>();
            builder.RegisterAssemblyTypes(typeof(BaseReadOnlyRepository<Lawnmower>).Assembly)
    .Where(t => t.IsSubclassOf(typeof(BaseReadOnlyRepository<Lawnmower>)))
    .As<BaseReadOnlyRepository<Lawnmower>>();
            builder.RegisterAssemblyTypes(typeof(BaseReadOnlyRepository<PhoneCase>).Assembly)
    .Where(t => t.IsSubclassOf(typeof(BaseReadOnlyRepository<PhoneCase>)))
    .As<BaseReadOnlyRepository<PhoneCase>>();
            //       builder.RegisterAssemblyTypes(typeof(Lawnmower).Assembly)
            //.Where(t => t.IsSubclassOf(typeof(Lawnmower)))
            //.As<Lawnmower>();
            //       builder.RegisterAssemblyTypes(typeof(PhoneCase).Assembly)
            //.Where(t => t.IsSubclassOf(typeof(PhoneCase)))
            //.As<PhoneCase>();
            builder.RegisterType<ProductMapper<TShirt>>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<ProductMapper<Lawnmower>>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder.RegisterType<ProductMapper<PhoneCase>>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder.RegisterType<BzProduct>().AsImplementedInterfaces();
            builder = additionalRegistration(builder);
            IContainer container = builder.Build();
            return container;
        }
    }
}
