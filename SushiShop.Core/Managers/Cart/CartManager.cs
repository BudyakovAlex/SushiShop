using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Mappers.Cart;
using SushiShop.Core.Mappers.Products;
using SushiShop.Core.Mappers.Topping;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Cart;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Cart
{
    public class CartManager : ICartManager
    {
        private readonly IUserSession userSession;
        private readonly ICartService cartService;

        public CartManager(IUserSession userSession, ICartService cartService)
        {
            this.userSession = userSession;
            this.cartService = cartService;
        }

        public async Task<Response<Product?>> UpdateProductInCartAsync(
            string? city,
            long id,
            Guid? uid,
            int count,
            Topping[] toppings)
        {
            var updateProductDto = new UpdateProductDto
            {
                City = city,
                Id = id,
                Uid = uid,
                BasketId = userSession.GetCartId(),
                Count = count,
                Toppings = toppings.Map()
            };

            var response = await cartService.UpdateProductInCartAsync(updateProductDto, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Product?>(isSuccessful: true, data);
            }

            return new Response<Product?>(isSuccessful: false, null);
        }

        public async Task<Response<Data.Models.Cart.Cart?>> GetCartAsync(string? city)
        {
            var response = await cartService.GetCartAsync(userSession.GetCartId(), city, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Data.Models.Cart.Cart?>(isSuccessful: false, null);
            }

            var data = response.Data!.SuccessData?.Map();
            return new Response<Data.Models.Cart.Cart?>(isSuccessful: true, data);
        }

        public async Task<Response<Promocode?>> GetCartPromocodeAsync(string? city, string promocode)
        {
            var response = await cartService.GetCartPromocodeAsync(userSession.GetCartId(), city, promocode, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Promocode?>(isSuccessful: false, null!, response.Data?.Errors ?? Array.Empty<string>());
            }

            var data = response.Data!.SuccessData?.Map();
            return new Response<Promocode?>(isSuccessful: true, data, response.Data?.Errors ?? Array.Empty<string>());
        }

        public async Task<Response<Product[]?>> GetCartPackagingAsync(string? city)
        {
            var response = await cartService.GetCartPackagingAsync(userSession.GetCartId(), city, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Product[]?>(isSuccessful: false, Array.Empty<Product>());
            }

            var data = response.Data!.SuccessData!.Select(package => package.Map()).ToArray();
            return new Response<Product[]?>(isSuccessful: true, data);
        }

        public async Task<Response<Topping[]?>> GetSaucesAsync(string? city)
        {
            var response = await cartService.GetSaucesAsync(userSession.GetCartId(), city, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Topping[]?>(isSuccessful: false, Array.Empty<Topping>());
            }

            var data = response.Data!.SuccessData!.Select(sauces => sauces.Map()).ToArray();
            return new Response<Topping[]?>(isSuccessful: true, data!);
        }

        public async Task<Response<Data.Models.Cart.Cart?>> ClearCartAsync(string? city)
        {
            var response = await cartService.ClearCartAsync(userSession.GetCartId(), city, CancellationToken.None);
            if (!response.IsSuccessful)
            {
                return new Response<Data.Models.Cart.Cart?>(isSuccessful: false, null!);
            }

            var data = response.Data!.SuccessData?.Map();
            return new Response<Data.Models.Cart.Cart?>(isSuccessful: true, data!);
        }
    }
}