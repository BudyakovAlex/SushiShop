using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Shops
{
    public class MetroShop : Metro
    {
        public MetroShop(
            double distance,
            string? line,
            string? name,
            string? measurement,
            Shop? shop) : base(distance, line, name, measurement)
        {
            Shop = shop;
        }

        public Shop? Shop { get; }
    }
}