using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Orders
{
    public class OrderDeliveryRequest : OrderDelivery
    {
        public OrderDeliveryRequest(
            string? address,
            Coordinates? cordinates) : base(address, cordinates)
        {
        }

        public string? Flat { get; set; }

        public string? Section { get; set; }

        public string? Floor { get; set; }

        public string? IntercomCode { get; set; }
    }
}