using Azure.Data.Tables;
using Azure.Storage.Blobs;
using DeliverySytem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySytem.Controllers
{
    public class ShopController : Controller
    {
        private readonly TableClient tableClient;
        private readonly BlobContainerClient blobContainer;

        public ShopController(TableClient tableClient, BlobContainerClient blobContainer)
        {
            this.tableClient = tableClient;
            this.blobContainer = blobContainer;
        }
        public IActionResult Index()
        {
            var countries = tableClient.Query<Country>(filter: "PartitionKey eq 'Country'").ToList();
            var cities = tableClient.Query<City>(filter: "PartitionKey eq 'City'").ToList();
            var shops = tableClient.Query<Shop>(filter: "PartitionKey eq 'Shop'").ToList();

            foreach (var shop in shops)
            {
                shop.Country = countries.FirstOrDefault(c => c.RowKey == shop.CountryRowKey);
                shop.City = cities.FirstOrDefault(c => c.RowKey == shop.CityRowKey);
            }
            return View(shops);
        }

        [HttpGet]
		public IActionResult AddShop()
		{
            List<Country> countries = tableClient
                .Query<Country>(filter: "PartitionKey eq 'Country'")
                .ToList();
            List<City> cities = tableClient.Query<City>(filter: "PartitionKey eq 'City'").ToList();
            
            ViewBag.Countries = countries;
            ViewBag.Cities = cities;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddShop(Shop shop, IFormFile AvatarUrl)
		{
            //string? userKey = HttpContext.Session.GetString("LoggedId");
            
            //if (string.IsNullOrEmpty(userKey))
            //{
            //    return RedirectToAction("Login");
            //}
            if (AvatarUrl == null)
            {
                ViewBag.ErrorMessage = "You must select a picture for the lot!";
                return View("Index", "Register");
            }
            BlobClient blobClient = blobContainer.GetBlobClient(shop.ShopName + ".jpg");
            await blobClient.UploadAsync(AvatarUrl.OpenReadStream());
            shop.AvatarUrl = blobClient.Uri.ToString();
            shop.CountryRowKey = Request.Form["CountryRowKey"];
            shop.CityRowKey = Request.Form["CityRowKey"];
            await tableClient.AddEntityAsync<Shop>(shop);

            return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult AddProduct()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddProduct(/*Product product*/ IFormFile AvatarUrl)
		{
			return View();
		}




	}
}
