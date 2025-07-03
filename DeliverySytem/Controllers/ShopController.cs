using DeliverySytem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySytem.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            var shops = new List<Shop>
            {
               new Shop("Zara", "", "Barcelona", "Spain"),
               new Shop("Fc Barcelona Fan shop", "", "Barcelona", "Spain"),
               new Shop("Lidl", "", "Valencia", "Spain"),
               new Shop("Alcampo", "", "Valencia", "Spain"),
			};

            return View(shops);
        }

        [HttpGet]
		public IActionResult AddShop()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddShop(Shop shop, IFormFile AvatarUrl)
		{
			return View();
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
