using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Shops
{
    public class DeliveryZone
    {
        public DeliveryZone(
            string? title,
            Coordinates[]? path,
            decimal price,
            bool isActive)
        {
            Title = title;
            Path = path;
            Price = price;
            IsActive = isActive;
        }

        public string? Title { get; }

        public Coordinates[]? Path { get; }

        public decimal Price { get; }

        public bool IsActive { get; }
    }
}