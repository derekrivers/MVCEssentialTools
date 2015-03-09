using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EssentialTools.Models
{
    public class DiscountHelper : IDiscountHelper
    {

        public decimal discountSize { get; set; }

        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (discountSize / 100m * totalParam));
        }

        public DiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
    }
}