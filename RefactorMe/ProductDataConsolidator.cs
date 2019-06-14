using NzProducts.Business.Products.Interfaces;
using NzProducts.Ioc;
using RefactorMe.DontRefactor.Models;
using System.Collections.Generic;
using NzProducts.Configuration.Management;
using Autofac;
using log4net;


namespace RefactorMe
{
    public class ProductDataConsolidator 
    {
        private readonly IBzProduct _bzProduct;
        private readonly ILog _log = Logger.GetLogger("NzProduct");
        public ProductDataConsolidator()
        {
            var container = IoCHelper.BuildContainer(GetType(), _log);
            _bzProduct = container.Resolve<IBzProduct>();
        }
        public  List<Product> Get()
        {
            return _bzProduct.Get();
        }

        public  List<Product> GetInUsDollars()
        {
            return _bzProduct.ProductsInEuros();
        }

        public  List<Product> GetInEuros()
        {
            return _bzProduct.ProductsInEuros();
        }
    }
}
