using System;

namespace EssentialTools.Models
{
    public class MinumumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {

            if (totalParam > 100)
            {
                return (totalParam * 0.9M);
            }

            if (totalParam >= 10 && totalParam <= 100)
            {
                return (totalParam - 5);
            }

            if (totalParam < 0)
            {
                throw new ArgumentOutOfRangeException(); 
            }

            return totalParam;
        }
    }
}