using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NzProducts.Business.Products;
using NzProducts.Business.Products.Constants;
using NzProducts.Common.Contracts;
using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Models;
using Xunit;
using Xunit.Abstractions;

namespace RefactorMe.Tests.UnitTests
{
    public class NzProducts
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NzProducts(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static BzProduct GetBusinessLogic(IQueryable<TShirt> allShirts, 
            IQueryable<PhoneCase> allPhoneCases, IQueryable<Lawnmower> allLawnMowers)
        {
            IReadOnlyRepository<TShirt> shirtOnlyRepository = Substitute.For<IReadOnlyRepository<TShirt>>();
            shirtOnlyRepository.GetAll().Returns(allShirts);
            IReadOnlyRepository<Lawnmower> lawnmowerOnlyRepository = Substitute.For<IReadOnlyRepository<Lawnmower>>();
            lawnmowerOnlyRepository.GetAll().Returns(allLawnMowers);
            IReadOnlyRepository<PhoneCase> phoneCaseOnlyRepository = Substitute.For<IReadOnlyRepository<PhoneCase>>();
            phoneCaseOnlyRepository.GetAll().Returns(allPhoneCases);
            IMapper<TShirt, Product> shirtMapper = Substitute.For<IMapper<TShirt, Product>>();
            shirtMapper.Map(Arg.Any<TShirt>()).ReturnsForAnyArgs(ts =>
            {
                var shirt = ts.Arg<TShirt>();
                return new Product
                {
                    Id = shirt.Id,
                    Price = shirt.Price,
                    Name = shirt.Name,
                    Type = ProductType.TShirt.ToString()
                };
            });
            IMapper<Lawnmower, Product> lawnMowerMapper = Substitute.For<IMapper<Lawnmower, Product>>();
            lawnMowerMapper.Map(Arg.Any<Lawnmower>()).ReturnsForAnyArgs(ts =>
            {
                var lawnmower = ts.Arg<Lawnmower>();
                return new Product
                {
                    Id = lawnmower.Id,
                    Price = lawnmower.Price,
                    Name = lawnmower.Name,
                    Type = ProductType.Lawnmower.ToString()
                };
            });
            IMapper<PhoneCase, Product> phoneCaseMapper = Substitute.For<IMapper<PhoneCase, Product>>();
            phoneCaseMapper.Map(Arg.Any<PhoneCase>()).ReturnsForAnyArgs(ts =>
            {
                var phoneCase = ts.Arg<PhoneCase>();
                return new Product
                {
                    Id = phoneCase.Id,
                    Price = phoneCase.Price,
                    Name = phoneCase.Name,
                    Type = ProductType.PhoneCase.ToString()
                };
            });
            return new BzProduct(shirtOnlyRepository, lawnmowerOnlyRepository, phoneCaseOnlyRepository, shirtMapper,
                phoneCaseMapper, lawnMowerMapper);
        }

        [Fact]
        public void GetAllProducts()
        {
            var shirts = GetShirts().AsQueryable();
            var phoneCases = GetPhoneCases().AsQueryable();
            var lawnMowers = GetLawnmowers().AsQueryable();
            BzProduct bzProduct = GetBusinessLogic(shirts, phoneCases, lawnMowers);

            var products = bzProduct.Get();

            Assert.Equal(shirts.Count(), products.Count(c => c.Type == ProductType.TShirt.ToString()));
            Assert.Equal(lawnMowers.Count(), products.Count(c => c.Type == ProductType.Lawnmower.ToString()));
            Assert.Equal(phoneCases.Count(), products.Count(c => c.Type == ProductType.PhoneCase.ToString()));

        }

        [Fact]
        public void ProductsInEuros()
        {
            var shirts = GetShirts().AsQueryable();
            var phoneCases = GetPhoneCases().AsQueryable();
            var lawnMowers = GetLawnmowers().AsQueryable();

            BzProduct bzProduct = GetBusinessLogic(shirts, phoneCases, lawnMowers);

            var products = bzProduct.ProductsInEuros();

            Assert.Equal(shirts.Select(p => p.Price * 0.67), products.Where(p => p.Type == ProductType.TShirt.ToString()).Select( p=> p.Price));
            Assert.Equal(lawnMowers.Select(p => p.Price * 0.67), products.Where(p => p.Type == ProductType.Lawnmower.ToString()).Select(p => p.Price));
            Assert.Equal(phoneCases.Select(p => p.Price * 0.67), products.Where(p => p.Type == ProductType.PhoneCase.ToString()).Select(p => p.Price));

        }
        [Fact]
        public void ProductsInUsDollars()
        {
            var shirts = GetShirts().AsQueryable();
            var phoneCases = GetPhoneCases().AsQueryable();
            var lawnMowers = GetLawnmowers().AsQueryable();

            BzProduct bzProduct = GetBusinessLogic(shirts, phoneCases, lawnMowers);

            var products = bzProduct.ProductsInUsDollars();

            products.ForEach(c => _testOutputHelper.WriteLine($"ID - {c.Id} , Name - {c.Name} , Type - {c.Type} Price - {c.Price}"));

            Assert.Equal(shirts.Select(p => p.Price * 0.76), products.Where(p => p.Type == ProductType.TShirt.ToString()).Select(p => p.Price));
            Assert.Equal(lawnMowers.Select(p => p.Price * 0.76), products.Where(p => p.Type == ProductType.Lawnmower.ToString()).Select(p => p.Price));
            Assert.Equal(phoneCases.Select(p => p.Price * 0.76), products.Where(p => p.Type == ProductType.PhoneCase.ToString()).Select(p => p.Price));

        }

        public List<TShirt> GetShirts()
        {
            var shirts = new List<TShirt>()
            {
                new TShirt()
                {
                    Id = Guid.NewGuid(),
                    Colour = "Blue",
                    Name = "Xamarin C# T-Shirt",
                    Price = 15.0,
                    ShirtText = "C#, Xamarin"
                },
                new TShirt()
                {
                    Id = Guid.NewGuid(),
                    Colour = "Black",
                    Name = "New York Yankees T-Shirt",
                    Price = 8.0,
                    ShirtText = "NY"
                },
                new TShirt()
                {
                    Id = Guid.NewGuid(),
                    Colour = "Green",
                    Name = "Disney Sleeping Beauty T-Shirt",
                    Price = 10.0,
                    ShirtText = "Mirror mirror on the wall..."
                }
            };
            return shirts;
        }

        public List<PhoneCase> GetPhoneCases()
        {
            var phoneCases = new List<PhoneCase>()
            {

                new PhoneCase()
                {
                    Id = Guid.NewGuid(),
                    Name = "Amazon Fire Burgundy Phone Case",
                    Colour = "Burgundy",
                    Material = "PVC",
                    TargetPhone = "Amazon Fire",
                    Price = 14.0
                },
                new PhoneCase()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nokia Lumia 920/930/Icon Crimson Phone Case",
                    Colour = "Red",
                    Material = "Rubber",
                    TargetPhone = "Nokia Lumia 920/930/Icon",
                    Price = 10.0
                }

            };
            return phoneCases;

        }
        public List<Lawnmower> GetLawnmowers()
        {
            var lawnmowers = new List<Lawnmower>()
            {

                new Lawnmower() {
                    Id = Guid.NewGuid(),
                    Name = "Hewlett-Packard Rideable Lawnmower",
                    FuelEfficiency = "Very Low",
                    IsVehicle = true,
                    Price = 3000.0
                },
                new Lawnmower() {
                    Id = Guid.NewGuid(),
                    Name = "Fisher Price's My First Lawnmower",
                    FuelEfficiency = "Ultimate",
                    IsVehicle = false,
                    Price = 45.0
                },
                new Lawnmower() {
                    Id = Guid.NewGuid(),
                    Name = "Volkswagen LawnMaster 39000B Lawnmower",
                    FuelEfficiency = "Moderate",
                    IsVehicle = false,
                    Price = 1020.0
                }

            };
            return lawnmowers;

        }
    }
}
    
