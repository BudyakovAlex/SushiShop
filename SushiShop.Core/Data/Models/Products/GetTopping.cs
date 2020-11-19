using System;

namespace SushiShop.Core.Data.Models.Products
{
    public class GetTopping
    {
        public GetTopping(long Id, int count, int price, string? pageTitle)
        {
            this.Id = id;
            this.Count = count;
            this.Price = price;
            this.PageTitle = pageTitle;
        }

        public long Id { get; }
        public int Count { get; }
        public int Price { get; }
        public string? PageTitle { get; }
    }
}
