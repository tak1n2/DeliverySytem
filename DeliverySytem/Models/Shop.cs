using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
	public class Shop : ITableEntity
	{
		public string ShopName { get; set; }
		public string AvatarUrl { get; set; }
		public string CityRowKey { get; set; }
		public string CountryRowKey { get; set; }

		//
		public string PartitionKey { get; set; }
		public string RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public ETag ETag { get; set; }

		public Shop(string shopName, string avatarUrl, string cityRowKey, string countryRowKey)
		{
			ShopName = shopName;
			AvatarUrl = avatarUrl;
			CityRowKey = cityRowKey;
			CountryRowKey = countryRowKey;


			PartitionKey = "Shop";
			RowKey = Guid.NewGuid().ToString();
			Timestamp = DateTimeOffset.UtcNow;
			ETag = new ETag();
		}

		public Shop()
		{
			ShopName = "";
			AvatarUrl = "";
			CityRowKey = "";
			CountryRowKey = "";

			PartitionKey = "Shop";
			RowKey = Guid.NewGuid().ToString();
			Timestamp = DateTimeOffset.UtcNow;
			ETag = new ETag();
		}

	}
}
