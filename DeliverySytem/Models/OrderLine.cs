using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class OrderLine : ITableEntity
    {
        public string OrderKey { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ShopKey { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public OrderLine(string orderKey, string productName, int price, string shopKey)
        {
            OrderKey = orderKey;
            ProductName = productName;
            Price = price;
            ShopKey = shopKey;

            PartitionKey = "OrderLine";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public OrderLine()
        {
            OrderKey = "";
            ProductName = "";
            Price = 0;
            ShopKey = "";

            PartitionKey = "OrderLine";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
