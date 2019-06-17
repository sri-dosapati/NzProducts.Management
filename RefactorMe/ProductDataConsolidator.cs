using NzProducts.Business.Products.Interfaces;
using NzProducts.Ioc;
using RefactorMe.DontRefactor.Models;
using System.Collections.Generic;
using NzProducts.Configuration.Management;
using Autofac;
using log4net;
using RefactorMe.DontRefactor.Data;
using NzProducts.Common.Contracts;

namespace RefactorMe
{
    public class ProductDataConsolidator
    {
        private readonly IBzProduct _bzProduct;
        private readonly ILog _log = Logger.GetLogger("NzProduct");

        public ProductDataConsolidator()
        {
            var container = IoCHelper.BuildContainer(GetType(), _log);
             container.Resolve<IReadOnlyRepository<TShirt>>();
             container.Resolve<IReadOnlyRepository<Lawnmower>>();
             container.Resolve<IReadOnlyRepository<PhoneCase>>();
             container.Resolve<IMapper<TShirt, Product>>();
             container.Resolve<IMapper<Lawnmower, Product>>();
             container.Resolve<IMapper<PhoneCase, Product>>();
            _bzProduct = container.Resolve<IBzProduct>();
            
        }
        public List<Product> Get()
        {
            return _bzProduct.Get();
        }

        public List<Product> GetInUsDollars()
        {
            return _bzProduct.ProductsInUsDollars();
        }

        public List<Product> GetInEuros()
        {
            return _bzProduct.ProductsInEuros();
        }
    }
}
