using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Models;

namespace NzProducts.Business.Products.Mappers
{
    public class ShirtMapper : IMapper<TShirt, Product>
    {
        public Product Map(TShirt source)
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
