using System.Collections.Generic;

namespace RefactorMe.DontRefactor.Models
{
    public class NzItem
    {
        public IEnumerable<TShirt>  Shirts { get; set; }
        public IEnumerable<PhoneCase> PhoneCases { get; set; }
        public IEnumerable<Lawnmower> LawnMowers { get; set; }
    }
}
