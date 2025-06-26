using Azure;
using Azure.Data.Tables;

namespace DeliverySytem.Models
{
    public class City : ITableEntity
    {
        public string CityName { get; set; }
        public string CountryKey { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public City(string cityName, string countryKey)
        {
            CityName = cityName;
            CountryKey = countryKey;

            PartitionKey = "City";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }

        public City()
        {
            CityName = "";
            CountryKey = "";

            PartitionKey = "City";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            ETag = new ETag();
        }
    }
}
