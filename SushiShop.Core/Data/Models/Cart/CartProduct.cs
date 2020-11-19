using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public CartProduct(
            long id,
            int count,
            decimal price,
            decimal? totalPrice,
            string? pageTitle,
            Guid? uid,
            bool? isReadonly,
            CartTopping[] toppings)
        {
            Id = id;
            Count = count;
            Price = price;
            TotalPrice = totalPrice;
            PageTitle = pageTitle;
            Uid = uid;
            IsReadonly = isReadonly;
            Toppings = toppings;
        }

        public long Id { get; }

        public int Count { get; }

        public decimal Price { get; }

        public decimal? TotalPrice { get; }

        public string? PageTitle { get; }

        public Guid? Uid { get; }

        public bool? IsReadonly { get; }

        public CartTopping[] Toppings { get; } = Array.Empty<CartTopping>();
    }
}