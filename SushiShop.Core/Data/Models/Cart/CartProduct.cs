using SushiShop.Core.Data.Models.Products;
using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public CartProduct(
            Guid id,
            int count,
            decimal price,
            int? amount,
            string? pageTitle,
            Guid? uid,
            bool? isReadonly,
            GetTopping[] toppings)
        {
            Id = id;
            Count = count;
            Price = price;
            Amount = amount;
            PageTitle = pageTitle;
            Uid = uid;
            IsReadonly = isReadonly;
            Toppings = toppings;
        }

        public Guid Id { get; }

        public int Count { get; }

        public decimal Price { get; }

        public int? Amount { get; }

        public string? PageTitle { get; }

        public Guid? Uid { get; }

        public bool? IsReadonly { get; }

        public GetTopping[] Toppings { get; } = Array.Empty<GetTopping>();
    }
}