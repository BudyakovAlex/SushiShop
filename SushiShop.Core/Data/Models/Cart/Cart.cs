using System;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Dtos.Products;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Cart
    {
        public Cart(int basketId, string? city, string? priceGroup, long totalCount, long totalSum, CurrencyDto? currency, CartProductDto? products, PromocodeDto? promoCode, Guid productUid)
        {
            BasketId = basketId;
            City = city;
            PriceGroup =  priceGroup;
            TotalCount = totalCount;
            TotalSum = totalSum;
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

        public CurrencyDto? Currency { get; }

        public CartProductDto? Products { get; }

        public PromocodeDto? PromoCode { get; }

        public Guid ProductUid { get; }
    }
}
