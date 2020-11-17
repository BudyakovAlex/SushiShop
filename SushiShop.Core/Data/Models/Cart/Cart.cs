using System;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Dtos.Products;
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
            Currency?[] currency, 
            CartProduct?[] products, 
            PromoCode?[] promoCode, 
            Guid productUid)
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

        public Currency?[] Currency { get; }

        public CartProduct?[] Products { get; }

        public PromoCode?[] PromoCode { get; }

        public Guid ProductUid { get; }
    }
}
