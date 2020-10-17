using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models
{
    public class City
    {
        public City(int id,
            string? name,
            int shopsCount,
            double latitude,
            double longitude,
            string? timeZone,
            PriceGroup priceGroup,
            string? phone,
            string? cityGroups,
            Currency? currency)
        {
            Id = id;
            Name = name;
            ShopsCount = shopsCount;
            Latitude = latitude;
            Longitude = longitude;
            TimeZone = timeZone;
            PriceGroup = priceGroup;
            Phone = phone;
            CityGroups = cityGroups;
            Currency = currency;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int ShopsCount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? TimeZone { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string? Phone { get; set; }
        public string? CityGroups { get; set; }
        public Currency? Currency { get; set; }
    }
}