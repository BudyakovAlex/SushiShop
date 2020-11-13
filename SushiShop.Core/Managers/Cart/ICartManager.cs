using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using System;
using System.Threading.Tasks;
using SushiShop.Core.Data.Models.Cart;

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

        Task<Response<Data.Models.Cart.Cart?>> GetProductInCartAsync(string? city);

        Task<Response<PromoCode>> GetCartPromoCodeAsync(string? city, string promocode);
    }
}