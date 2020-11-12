using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Cart
{
    public interface ICartManager
    {
        Task<Response<Product?>> UpdateProductInCartAsync(
            string? city,
            int id,
            Guid? uid,
            int count,
            Topping[] toppings);
    }
}