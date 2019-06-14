using RefactorMe.DontRefactor.Models;
using System.Collections.Generic;

namespace NzProducts.Business.Products.Interfaces
{
    public interface IBzProduct
    {
        List<Product> Get();
        List<Product> ProductsInUsDollars();
        List<Product> ProductsInEuros();
    }
}