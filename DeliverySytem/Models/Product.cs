using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class Product : ITableEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public string PictureUrl { get; set; }
        public string ShopKey { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Product(string productName, string productDescription, int price, string pictureUrl, string shopKey)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            PictureUrl = pictureUrl;
            ShopKey = shopKey;

            PartitionKey = "Product";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public Product()
        {
            ProductName = "";
            ProductDescription = "";
            Price = 0;
            PictureUrl = "";
            ShopKey = "";

            PartitionKey = "Product";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
