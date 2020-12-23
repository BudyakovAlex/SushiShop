namespace SushiShop.Core.Data.Models.Common
{
    public class Coordinates
    {
        public Coordinates(double? longitude, double? latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double? Longitude { get; }

        public double? Latitude { get; }
    }
}