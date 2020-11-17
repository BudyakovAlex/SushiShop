using SushiShop.Core.Data.Models.Products;
using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public Guid Id { get; }

        public int Count { get; }

        public int Price { get; }

        public int? Amount { get; }

        public string? Pagetitle { get; }

        public Guid? Uid { get; }

        public bool? ReadonlyFlag { get; }

        public GetTopping[] Toppings { get; } = Array.Empty<GetTopping>();
    }
}
