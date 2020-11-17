using SushiShop.Core.Data.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Data.Models.Cart
{
    public class CartProduct
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        public string? Pagetitle { get; set; }

        public Guid? Uid { get; set; }

        public GetToppingDto[] Toppings { get; set; } = Array.Empty<GetToppingDto>();
    }
}
