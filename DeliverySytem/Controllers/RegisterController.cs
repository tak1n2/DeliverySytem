using Azure.Data.Tables;
using Azure.Storage.Blobs;
using DeliverySytem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySytem.Controllers
{
    
    public class RegisterController : Controller
    {
        private readonly TableClient tableClient;

        public RegisterController(TableClient tableClient)
        {
            this.tableClient = tableClient;
        }

        public IActionResult Index()
        {
            string? loggedId = HttpContext.Session.GetString("LoggedId");
            if (string.IsNullOrEmpty(loggedId))
            {
                return RedirectToAction("Login");
            }
            Customer cust = tableClient.
                Query<Customer>(u => u.RowKey == loggedId).FirstOrDefault();
            return View(cust);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            Customer cust = tableClient.Query<Customer>(u => u.Email == customer.Email).FirstOrDefault();
            if (cust != null)
            {
                ViewBag.ErrorMessage = "This email is already registered!";
                return View();
            }
            await tableClient.AddEntityAsync<Customer>(customer);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            Customer fromTable = tableClient
                .Query<Customer>(u => u.Email == customer.Email && u.Password == customer.Password)
                .FirstOrDefault();
            if (fromTable != null)
            {
                HttpContext.Session.SetString("LoggedId", fromTable.RowKey);
                HttpContext.Session.SetString("LoggedEmail", fromTable.Email);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
