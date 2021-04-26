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
            string? warningMessage,
            long totalCount, 
            decimal totalSum,
            decimal discount,
            Currency? currency, 
            CartProduct[]? products, 
            Promocode? promocode)
        {
            BasketId = basketId;
            City = city;
            PriceGroup =  priceGroup;
            WarningMessage = warningMessage;
            TotalCount = totalCount;
            TotalSum = totalSum;
            Discount = discount;
            Currency = currency;
            Products = products;
            Promocode = promocode;
        }

        public Guid BasketId { get; }

        public string? City { get; }

        public string? PriceGroup { get; }

        public string? WarningMessage { get; }

        public long TotalCount { get; }

        public decimal TotalSum { get; set; }

        public decimal Discount { get; }

        public Currency? Currency { get; }

        public CartProduct[]? Products { get; }

        public Promocode? Promocode { get; }
    }
}
