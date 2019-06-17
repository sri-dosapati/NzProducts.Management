using RefactorMe;
using System;

namespace NzProducts.Start
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProductDataConsolidator pdc = new ProductDataConsolidator();
            Console.WriteLine($"========================================================================\n");
            Console.WriteLine("All Products");
            Console.WriteLine($"========================================================================\n");
            pdc.Get().ForEach(c => Console.WriteLine($"ID - {c.Id} , Name - {c.Name} , Type - {c.Type} Price - {c.Price}"));
            Console.WriteLine($"========================================================================\n");
            Console.WriteLine("Products in Euros");
            Console.WriteLine($"========================================================================\n");
            pdc.GetInEuros().ForEach(c => Console.WriteLine($"ID - {c.Id} , Name - {c.Name} , Type - {c.Type} Price - €{c.Price}"));
            Console.WriteLine($"========================================================================\n");
            Console.WriteLine("Products in UsDollars");
            Console.WriteLine($"========================================================================\n");
            pdc.GetInUsDollars().ForEach(c => Console.WriteLine($"ID - {c.Id} , Name - {c.Name} , Type - {c.Type} Price - ${c.Price}"));
            Console.ReadLine();
        }
    }
}
