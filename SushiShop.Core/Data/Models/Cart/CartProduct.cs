using SushiShop.Core.Data.Models.Products;
using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public CartProduct(
            Guid id, 
            int count, 
            int price, 
            int amount, 
            string? pageTitle,
            Guid? uid, 
            bool? readonlyFlag, 
            GetTopping[] toppings)
        {
            Id = id;
            Count = count;
            Price = price;

            Amount = amount;
            PageTitle = pageTitle;
            Uid = uid;
            ReadonlyFlag = readonlyFlag;
            Toppings = toppings;
        }

        public Guid Id { get; }

        public int Count { get; }

        public int Price { get; }

        public int Amount { get; }

        public string? PageTitle { get; }

        public Guid? Uid { get; }

        public bool? ReadonlyFlag { get; }

        public GetTopping[] Toppings { get; } = Array.Empty<GetTopping>();
    }
}
