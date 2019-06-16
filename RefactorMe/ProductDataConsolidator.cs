using NzProducts.Business.Products.Interfaces;
using NzProducts.Ioc;
using RefactorMe.DontRefactor.Models;
using System.Collections.Generic;
using NzProducts.Configuration.Management;
using Autofac;
using log4net;
using System;

namespace RefactorMe
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProductDataConsolidator pdc = new ProductDataConsolidator();
            pdc.Get().ForEach(c => Console.WriteLine($"ID - {c.Id} , Name - {c.Name} , Type - {c.Type} Price -{c.Price}"));
        }
    }
    public class ProductDataConsolidator
    {
        private readonly IBzProduct _bzProduct;
        private readonly ILog _log = Logger.GetLogger("NzProduct");
        public ProductDataConsolidator()
        {
            var container = IoCHelper.BuildContainer(GetType(), _log);
            _bzProduct = container.Resolve<IBzProduct>();
        }
        public List<Product> Get()
        {
            return _bzProduct.Get();
        }

        public List<Product> GetInUsDollars()
        {
            return _bzProduct.ProductsInEuros();
        }

        public List<Product> GetInEuros()
        {
            return _bzProduct.ProductsInEuros();
        }
    }
}
