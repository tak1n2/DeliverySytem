using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class Order : ITableEntity
    {
        public string CustomerKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalAmount { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Order(string customerKey, int totalAmount)
        {
            CustomerKey = customerKey;
            CreatedAt = DateTime.UtcNow;
            TotalAmount = totalAmount;

            PartitionKey = "Order";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public Order()
        {
            CustomerKey = "";
            CreatedAt = DateTime.UtcNow;
            TotalAmount = 0;

            PartitionKey = "Order";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
