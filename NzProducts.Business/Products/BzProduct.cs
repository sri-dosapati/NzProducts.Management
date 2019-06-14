using System.Collections.Generic;
using System.Linq;
using NzProducts.Business.Products.Constants;
using NzProducts.Business.Products.Interfaces;
using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Models;

namespace NzProducts.Business.Products
{
    public class BzProduct : IBzProduct
    {
        private static IReadOnlyRepository<TShirt> _tShirtOnlyRepository;
        private static IReadOnlyRepository<Lawnmower> _lawnOnlyRepository;
        private static IReadOnlyRepository<PhoneCase> _phoneCaseOnlyRepository;
        private static IMapper<TShirt, Product> _tShirtMapper;
        private readonly IMapper<PhoneCase, Product> _phoneCaseMapper;
        private readonly IMapper<Lawnmower, Product> _lawnMoverMapper;

        public BzProduct(IReadOnlyRepository<TShirt> tShirtOnlyRepository
            , IReadOnlyRepository<Lawnmower> lawnOnlyRepository
            , IReadOnlyRepository<PhoneCase> phoneCaseOnlyRepository,
            IMapper<TShirt, Product> tTShirtMapper,
            IMapper<PhoneCase, Product> phoneCaseMapper,
            IMapper<Lawnmower, Product> lawnMoverMapper)

        {
            _tShirtOnlyRepository = tShirtOnlyRepository;
            _lawnOnlyRepository = lawnOnlyRepository;
            _phoneCaseOnlyRepository = phoneCaseOnlyRepository;
            _tShirtMapper = tTShirtMapper;
            _phoneCaseMapper = phoneCaseMapper;
            _lawnMoverMapper = lawnMoverMapper;
        }
        public List<Product> Get()
        {
            var products = GetAllProducts();
            return products;
        }

        public List<Product> ProductsInUsDollars()
        {
            var products = GetAllProducts();
            return ProductPriceConversion(0.76, products);
        }

        public List<Product> ProductsInEuros()
        {
            var products = GetAllProducts();
            return ProductPriceConversion(0.67, products);
        }

        private List<Product> GetAllProducts()
        {
            var ps = new List<Product>();

            var nzItems = ExtractAllItems();
            
            var tShirtProducts = nzItems.Shirts.Select(t => _tShirtMapper.Map(t));
            var phoneCasesProducts = nzItems.PhoneCases.Select(p => _phoneCaseMapper.Map(p));
            var lawnMowerProducts = nzItems.Lawnmowers.Select(c => _lawnMoverMapper.Map(c));

            ps.AddRange(AddProductType(tShirtProducts, ProductType.TShirt));
            ps.AddRange(AddProductType(phoneCasesProducts, ProductType.PhoneCase));
            ps.AddRange(AddProductType(lawnMowerProducts, ProductType.Lawnmower));

            return ps;
        }

        private static List<Product> ProductPriceConversion(double conversionValue, List<Product> products)
        {
            return products.Select(p =>
            {
                p.Price = p.Price * conversionValue;
                return p;
            }).ToList();
        }
        private static List<Product> AddProductType(IEnumerable<Product> products, ProductType type)
        {
            return products.Select(p =>
            {
                p.Type = type.ToString();
                return p;
            }).ToList();
        }
        private NzItem ExtractAllItems()
        {
            NzItem nzProducts = new NzItem()
            {
                Shirts = _tShirtOnlyRepository.GetAll(),
                PhoneCases = _phoneCaseOnlyRepository.GetAll(),
                Lawnmowers = _lawnOnlyRepository.GetAll()
            };
            return nzProducts;
        }
    }
}
