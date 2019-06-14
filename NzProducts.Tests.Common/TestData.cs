using System;
using Bogus;
using NzProducts.Business.Products.Constants;

namespace NzProducts.Tests.Common
{
    public class TestData
    {
        private static Faker _faker;
        static TestData()
        {
            _faker = new Faker();
        }

        public static string Type => _faker.PickRandom<ProductType>().ToString();
        public static string Id => Guid.NewGuid().ToString();

        public static double Price => _faker.Random.Double();
        public static string Name => _faker.Name.FullName();

    }
}
