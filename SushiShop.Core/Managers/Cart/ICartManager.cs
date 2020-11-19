using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cart;
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
            long id,
            Guid? uid,
            int count,
            Topping[] toppings);

        Task<Response<Data.Models.Cart.Cart?>> GetCartAsync(string? city);

        Task<Response<Promocode?>> GetCartPromocodeAsync(string? city, string promocode);

        Task<Response<Product[]?>> GetCartPackagingAsync(string? city);

        Task<Response<Topping[]?>> GetSaucesAsync(string? city);

        Task<Response<Data.Models.Cart.Cart?>> ClearCartAsync(string? city);
    }
}