using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public CartProduct(
            long id,
            int count,
            decimal price,
            decimal oldPrice,
            string? weight,
            string? volume,
            decimal totalPrice,
            string? pageTitle,
            Guid? uid,
            bool isReadOnly,
            ProductType type,
            CartTopping[] toppings,
            ImageInfo? imageInfo)
        {
            Id = id;
            Count = count;
            Price = price;
            OldPrice = oldPrice;
            Weight = weight;
            Volume = volume;
            TotalPrice = totalPrice;
            PageTitle = pageTitle;
            Uid = uid;
            IsReadOnly = isReadOnly;
            Type = type;
            Toppings = toppings;
            ImageInfo = imageInfo;
        }

        public long Id { get; }

        public int Count { get; set; }

        public decimal Price { get; }

        public decimal TotalPrice { get; set; }

        public string? PageTitle { get; }

        public decimal OldPrice { get; }

        public string? Weight { get; }

        public string? Volume { get; }

        public Guid? Uid { get; set; }

        public bool IsReadOnly { get; }

        public ProductType Type { get; }

        public CartTopping[] Toppings { get; } = Array.Empty<CartTopping>();

        public ImageInfo? ImageInfo { get; }
    }
}