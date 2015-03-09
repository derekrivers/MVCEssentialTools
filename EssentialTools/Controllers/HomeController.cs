using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {

        private IValueCalculator calc;


        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "LifeJacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner Flag", Category = "Soccer", Price = 34.95M},

        };

        public HomeController(IValueCalculator calcParam)
        {
            this.calc = calcParam;
        }

        // GET: Home
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc)
            {
                Products = products
            };

            decimal total = cart.CalculateProductTotal();

            return View(total);
        }
    }
}