using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Toppings;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Cart
{
    public interface ICartManager
    {
        Task<Response<CartProduct?>> UpdateProductInCartAsync(
            string? city,
            int id,
            Guid? uid,
            int count,
            Topping[] toppings);

        Task<Response<Data.Models.Cart.Cart?>> GetCartAsync(int id, string city);

        Task<Response<Promocode?>> GetCartPromoCodeAsync(int id, string city, string promocode);

        Task<Response<Packaging[]>> GetCartPackagingAsync(int id, string city);
    }
}