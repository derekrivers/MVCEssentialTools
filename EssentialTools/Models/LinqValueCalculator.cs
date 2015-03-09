
using System.Collections.Generic;
using System.Linq;


namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private readonly IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discounter)
        {
            this.discounter = discounter;
            System.Diagnostics.Debug.WriteLine(string.Format("Instance {0} created", ++counter));
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}