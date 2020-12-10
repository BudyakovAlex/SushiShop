using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Orders
{
    public class OrderDelivery
    {
        public OrderDelivery(string? address, Coordinates? cordinates)
        {
            Address = address;
            Cordinates = cordinates;
        }

        public string? Address { get; }

        public Coordinates? Cordinates { get; }
    }
}