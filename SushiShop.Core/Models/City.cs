namespace SushiShop.Core.Models
{
    public class City
    {
        public City(int id,
                    string name,
                    int shopsCount,
                    double latitude,
                    double longitude,
                    string phoneNumber,
                    string timeZone)
        {
            Id = id;
            Name = name;
            ShopsCount = shopsCount;
            Latitude = latitude;
            Longitude = longitude;
            PhoneNumber = phoneNumber;
            TimeZone = timeZone;
        }

        public int Id { get; }

        public string Name { get; }

        public int ShopsCount { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string PhoneNumber { get; }

        public string TimeZone { get; }
    }
}