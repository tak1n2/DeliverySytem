using DeliverySytem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySytem.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = new List<Product>
            {
                new Product("Laptop", "High performance laptop", 1200, "https://example.com/laptop.jpg", "shop1"),
                new Product("Smartphone", "Latest model smartphone", 800, "https://example.com/smartphone.jpg", "shop2"),
                new Product("Headphones", "Noise-cancelling headphones", 200, "https://example.com/headphones.jpg", "shop3"),
            };
            return View(products);
        }
    }
}
