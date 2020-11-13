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

        public async Task<Response<Product?>> UpdateProductInCartAsync(string? city,
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


        public async Task<Response<Data.Models.Cart.Cart?>> GetProductInCartAsync(string? city)
        {
            var getProductDto = new GetProductDto()
            {
                BaseketId = userSession.GetCartId(),
                City = city
            };

            var response = await cartService.GetProductInCartAsync(getProductDto, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Data.Models.Cart.Cart?>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Cart.Cart?>(isSuccessful: false, null);
        }
    }
}
