using SushiShop.Core.Data.Enums;
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
            decimal? weight,
            decimal? volume,
            decimal totalPrice,
            string? pageTitle,
            Guid? uid,
            bool isReadOnly,
            ProductType type,
            CartTopping[] toppings)
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
        }

        public long Id { get; }

        public int Count { get; set; }

        public decimal Price { get; }

        public decimal TotalPrice { get; }

        public string? PageTitle { get; }

        public decimal OldPrice { get; }

        public decimal? Weight { get; }

        public decimal? Volume { get; }

        public Guid? Uid { get; }

        public bool IsReadOnly { get; }

        public ProductType Type { get; }

        public CartTopping[] Toppings { get; } = Array.Empty<CartTopping>();
    }
}