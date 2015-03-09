using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {

        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "LifeJacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner Flag", Category = "Soccer", Price = 34.95M},

        };

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            //arrange
            var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            // act
            var result = target.ValueProducts(products);

            // assert
            Assert.AreEqual(products.Sum(r => r.Price), result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            //arrange
            // Note: Mock will run through all setups on an oject in reverse order, so you need to prioritise setups in relevance order - lowest to highest.

            var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total); // Setup to accept any decimal value that returns any decimal value.
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == -1))).Throws<ArgumentOutOfRangeException>(); // Setup when decimal value equals 0, throw OutOfRangeExectopn
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M)); // Setup when decimal value is greater than 100, return decimal at 90% of value.
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).Returns<decimal>(total => total - 5); // Setup when decimal is within 10 & 100, return the decmal value - 5.

            //act
            //Note: Pass the Class object, that takes IDiscountHelper, the Mock Object <IDiscountHelper>.

            var target = new LinqValueCalculator(mock.Object);

            // act out each required scenario, The ValueProducts method ustilises the Mock IDiscountHelper Object

            decimal fiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal tenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal fiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal hundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal fiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            //assert
            Assert.AreEqual(5, fiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, tenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, hundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, fiveHundredDollarDiscount, "$500 Fail");

            // Cause Exception ArgumentOutOfRange.
            target.ValueProducts(createProduct(-1));

        }


    }
}
