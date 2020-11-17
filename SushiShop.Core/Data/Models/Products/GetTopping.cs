using System;

namespace SushiShop.Core.Data.Models.Products
{
    public class GetTopping
    {
        public GetTopping(Guid id, int count, int price, string? pageTitle)
        {
            this.Id = id;
            this.Count = count;
            this.Price = price;
            this.PageTitle = pageTitle;
        }

        public Guid Id { get; }
        public int Count { get; }
        public int Price { get; }

        public string? PageTitle { get; }
    }
}
