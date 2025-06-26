using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class Country : ITableEntity
    {
        public string CountryName { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Country(string countryName)
        {
            CountryName = countryName;
            PartitionKey = "Country";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public Country()
        {
            CountryName = "";
            PartitionKey = "Country";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
