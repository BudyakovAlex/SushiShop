using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Shops
{
    public class DeliveryZone
    {
        public DeliveryZone(
            string? title,
            Coordinates[]? polygon,
            string? path,
            decimal price,
            bool isActive)
        {
            Title = title;
            Polygon = polygon;
            Path = path;
            Price = price;
            IsActive = isActive;
        }

        public string? Title { get; }

        public Coordinates[]? Polygon { get; }

        public string? Path { get; }

        public decimal Price { get; }

        public bool IsActive { get; }
    }
}