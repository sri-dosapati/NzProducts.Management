using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Models;

namespace NzProducts.Business.Products.Mappers
{
    public class PhoneCaseMapper : IMapper<PhoneCase, Product>
    {
       public Product Map(PhoneCase source)
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
