using System;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Cart
    {
        public Cart(
            Guid basketId, 
            string? city, 
            string? priceGroup, 
            long totalCount, 
            int totalSum,
            long discount,
            Currency[]? currency, 
            CartProduct[]? products, 
            Promocode[]? promocodes)
        {
            BasketId = basketId;
            City = city;
            PriceGroup =  priceGroup;
            TotalCount = totalCount;
            TotalSum = totalSum;
            Discount = discount;
            Currency = currency;
            Products = products;
            Promocodes = promocodes;
        }

        public Guid BasketId { get; }

        public string? City { get; }

        public string? PriceGroup { get; }

        public long TotalCount { get; }

        public int TotalSum { get; }

        public long Discount { get; }

        public Currency[]? Currency { get; }

        public CartProduct[]? Products { get; }

        public Promocode[]? Promocodes { get; }
    }
}
