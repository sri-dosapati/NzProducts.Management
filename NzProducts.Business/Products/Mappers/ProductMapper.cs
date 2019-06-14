using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Data.Implementation;
using RefactorMe.DontRefactor.Models;

namespace NzProducts.Business.Products.Mappers
{
    public class ProductMapper<T> : IMapper<BaseReadOnlyRepository<T>, Product>
        where T : class
    {
        private readonly IMappingTargetProvider<Product> _targetProvider;
        public ProductMapper(IMappingTargetProvider<Product> targetProvider)
        {
            _targetProvider = targetProvider;
        }

        public Product Map(PhoneCase source)
        {

            Product p = _targetProvider.Create();
            p.Id = source.Id;
            p.Name = source.Name;
            p.Price = source.Price;
            return p;
        }

        public Product Map(BaseReadOnlyRepository<T> source)
        {
            throw new System.NotImplementedException();
        }
    }
}
