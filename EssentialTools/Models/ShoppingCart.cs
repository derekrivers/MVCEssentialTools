using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        private readonly IValueCalculator calc;

        public IEnumerable<Product> Products { get; set; } 

        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }


    }
}