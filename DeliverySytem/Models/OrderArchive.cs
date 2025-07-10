using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class OrderArchive: ITableEntity
    {
        public string OrderKey { get; set; }
        public string ProductName { get; set; }
        public string CustomerKey { get; set; }
        public int Price { get; set; }
        public string ShopKey { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public OrderArchive(string orderKey, string productName, int price, string shopKey,string customerKey)
        {
            OrderKey = orderKey;
            ProductName = productName;
            Price = price;
            ShopKey = shopKey;
            CustomerKey = customerKey;

            PartitionKey = "OrderArchive";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public OrderArchive()
        {
            OrderKey = "";
            ProductName = "";
            Price = 0;
            ShopKey = "";

            PartitionKey = "OrderArchive";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }




    }
}
