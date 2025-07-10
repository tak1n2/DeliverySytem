using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class Customer : ITableEntity
    {
       

        public string Name { get; set; }
        public string CityKey { get; set; }
        public string CountryKey { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TotalOrdersSum { get; set; }
        public DateTime CreatedAt { get; set; }

        public Country Country { get; set; }
        public City City { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Customer(string name, string cityKey, string countryKey, string email, string password)
        {
            Name = name;
            CityKey = cityKey;
            CountryKey = countryKey;
            Email = email;
            Password = password;
            TotalOrdersSum = 0;
            CreatedAt = DateTime.UtcNow;

            PartitionKey = "Customer";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public Customer()
        {
            Name = "";
            CityKey = "";
            CountryKey = "";
            Email = "";
            Password = "";
            TotalOrdersSum = 0;
            CreatedAt = DateTime.UtcNow;

            PartitionKey = "Customer";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
