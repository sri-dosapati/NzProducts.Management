using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Models;

namespace NzProducts.Business.Products.Mappers
{
    public class LawnMowerMapper : IMapper<Lawnmower, Product>
    {
        public Product Map(Lawnmower source)
        {
            Product p = new Product()
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price
            };
            return p;
        }
    }
}

