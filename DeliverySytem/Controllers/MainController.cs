using Azure.Data.Tables;
using DeliverySytem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySytem.Controllers
{
    public class MainController : Controller
    {
        private readonly TableClient tableClient;

        public MainController(TableClient tableClient)
        {
            this.tableClient = tableClient;
        }

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

        public IActionResult Buy(string productName, int price, string shopKey)
        {
            string? loggedId = HttpContext.Session.GetString("LoggedId");
            if (string.IsNullOrEmpty(loggedId))
            {
                return RedirectToAction("Login", "Register");
            }

            Order order = new Order(loggedId, price);
            tableClient.AddEntity(order);

            OrderLine orderLine = new OrderLine(order.RowKey, productName, price, shopKey);
            tableClient.AddEntity(orderLine);

            ViewBag.SuccessMessage = $"Order created for {productName}.";

            return RedirectToAction("Index");
        }
    }
}
