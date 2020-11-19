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

        public decimal TotalPrice { get; set; }

        public string? PageTitle { get; }

        public Guid? Uid { get; }

        public bool IsReadOnly { get; }

        public ProductType Type { get; }

        public CartTopping[] Toppings { get; } = Array.Empty<CartTopping>();
    }
}