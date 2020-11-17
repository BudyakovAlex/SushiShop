using System;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Cart
    {
        public Cart(int basketId, 
            string? city, 
            string? priceGroup, 
            long totalCount, 
            long totalSum,
            long discount,
            Currency[]? currency, 
            CartProduct[]? products, 
            Promocode[]? promoCode, 
            Guid productUid)
        {
            BasketId = basketId;
            City = city;
            PriceGroup =  priceGroup;
            TotalCount = totalCount;
            TotalSum = totalSum;
            Discount = discount;
            Currency = currency;
            Products = products;
            PromoCode = promoCode;
            ProductUid = productUid;
        }

        public int BasketId { get; }

        public string? City { get; }

        public string? PriceGroup { get; }

        public long TotalCount { get; }

        public long TotalSum { get; }

        public long Discount { get; }

        public Currency[]? Currency { get; }

        public CartProduct[]? Products { get; }

        public Promocode[]? PromoCode { get; }

        public Guid? ProductUid { get; }
    }
}
