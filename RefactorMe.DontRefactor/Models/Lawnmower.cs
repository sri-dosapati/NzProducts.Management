using System;

namespace RefactorMe.DontRefactor.Models
{
    public class Lawnmower
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string FuelEfficiency { get; set; }

        public bool IsVehicle { get; set; }
    }
}
