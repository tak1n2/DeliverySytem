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

        public IActionResult Order(string productName, int price, string shopKey)
        {
            string? loggedId = HttpContext.Session.GetString("LoggedId");
            if (string.IsNullOrEmpty(loggedId))
            {
                return RedirectToAction("Login", "Register");
            }

            Order? existingOrder = tableClient.Query<Order>(o =>
                o.CustomerKey == loggedId &&
                o.CreatedAt.Date == DateTime.UtcNow.Date
            ).FirstOrDefault();

            if (existingOrder == null)
            {
                existingOrder = new Order(loggedId, 0);
                tableClient.AddEntity(existingOrder);
                existingOrder = tableClient.GetEntity<Order>(existingOrder.PartitionKey, existingOrder.RowKey).Value;
            }
            else
            {
                existingOrder = tableClient.GetEntity<Order>(existingOrder.PartitionKey, existingOrder.RowKey).Value;
            }

            OrderLine orderLine = new OrderLine(existingOrder.RowKey, productName, price, shopKey);
            tableClient.AddEntity(orderLine);

            existingOrder.TotalAmount += price;
            tableClient.UpdateEntity(existingOrder, existingOrder.ETag, TableUpdateMode.Replace);

            return RedirectToAction("OrderDetails");
        }

        public IActionResult OrderDetails()
        {
            string? loggedId = HttpContext.Session.GetString("LoggedId");
            if (string.IsNullOrEmpty(loggedId))
            {
                return RedirectToAction("Login", "Register");
            }

            DateTime today = DateTime.UtcNow.Date;
            DateTime tomorrow = today.AddDays(1);

            Order todayOrder = tableClient.Query<Order>(o =>
                o.CustomerKey == loggedId &&
                o.CreatedAt >= today &&
                o.CreatedAt < tomorrow
            ).FirstOrDefault();

            List<OrderLine> orderLines = new();

            if (todayOrder != null)
            {
                orderLines = tableClient.Query<OrderLine>(l => l.OrderKey == todayOrder.RowKey).ToList();
            }

            ViewBag.Order = todayOrder;
            return View("Order", orderLines);
        }
    }
}
