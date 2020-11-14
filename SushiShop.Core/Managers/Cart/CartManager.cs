using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http.Cart;
using System;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Models.Cart;

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
            int id,
            Guid? uid,
            int count,
            Topping[] toppings)
        {
            var updateProductDto = new UpdateProductDto
            {
                City = city,
                Id = id,
                Uid = uid,
                BaseketId = userSession.GetCartId(),
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


        public async Task<Response<Data.Models.Cart.Cart?>> GetCartAsync(
            int id, 
            string city)
        {
            var response = await cartService.GetCartAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Cart.Cart?>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Cart.Cart?>(isSuccessful: false, null);
        }

        public async Task<Response<PromoCode>> GetCartPromoCodeAsync(
            int id, 
            string city, 
            string promocode)
        {
            var response = await cartService.GetCartPromoCodeAsync(city, promocode, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                if (data != null) 
                    return new Response<PromoCode>(isSuccessful: true, data);
            }

            return new Response<PromoCode>(isSuccessful: false, null!);
        }

        public async Task<Response<Packaging>> GetCartPackagingAsync(
            int id,
            string city)
        {
            var response = await cartService.GetCartPackagingAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                if (data != null)
                    return new Response<Packaging>(isSuccessful: true, data);
            }

            return new Response<Packaging>(isSuccessful: false, null!);
        }
    }
}
