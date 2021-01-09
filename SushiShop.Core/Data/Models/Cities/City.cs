using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Cities
{
    public class City
    {
        public City(
            long id,
            string name,
            int shopsCount,
            double latitude,
            double longitude,
            string timeZone,
            PriceGroup priceGroup,
            string phone,
            string cityGroups,
            Currency currency,
            bool isMetroAvailable)
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
            IsMetroAvailable = isMetroAvailable;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int ShopsCount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeZone { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string Phone { get; set; }
        public string CityGroups { get; set; }
        public bool IsMetroAvailable { get; set; }
        public Currency Currency { get; set; }
    }
}