    using Azure.Data.Tables;
    using Azure.Storage.Blobs;
    using DeliverySytem.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    namespace DeliverySytem.Controllers
    {
    
        public class RegisterController : Controller
        {
            private readonly TableClient tableClient;
            private readonly PasswordHasher<Customer> hasher; //Using to hash passwords

		    public RegisterController(TableClient tableClient)
            {
                this.tableClient = tableClient;
                hasher = new PasswordHasher<Customer>();

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
			var countries = tableClient.Query<Country>().ToList();
			var cities = tableClient.Query<City>().ToList(); 

			ViewBag.Countries = new SelectList(countries, "RowKey", "CountryName");
			ViewBag.Cities = new SelectList(cities, "RowKey", "CityName");

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

			    customer.Password = hasher.HashPassword(customer, customer.Password); //used hasher for hashing password
			    await tableClient.AddEntityAsync<Customer>(customer);

			    return RedirectToAction("Login");
            }

		    /*[HttpGet]
		    public IActionResult GetCities(string countryKey)
		    {
			    var cities = tableClient                                                         //Out of order, do  not touch for now:)
				    .Query<City>(c => c.CountryKey == countryKey)
				    .Select(c => new { c.RowKey, c.CityName })
				    .ToList();

			    return Json(cities);
		    }*/

		    [HttpGet]
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Login(Customer customer)
            {
                Customer fromTable = tableClient
		                 .Query<Customer>(c => c.Email == customer.Email)
		                .FirstOrDefault();

			    if (fromTable != null)
			    {
				    var result = hasher.VerifyHashedPassword(fromTable, fromTable.Password, customer.Password); //used hashe verify the entered password against the stored hash

				    if (result == PasswordVerificationResult.Success)
				    {
					    HttpContext.Session.SetString("LoggedId", fromTable.RowKey);
					    HttpContext.Session.SetString("LoggedEmail", fromTable.Email);
					    return RedirectToAction("Index");
				    }
			    }
			    return View();
            }
        }
    }
