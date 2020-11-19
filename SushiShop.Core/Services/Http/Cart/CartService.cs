using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Dtos.Toppings;
using SushiShop.Core.Data.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Cart
{
    public class CartService : ICartService
    {
        private readonly IHttpService httpService;

        public CartService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var a = await httpService.ExecuteAsync<ResponseDto<ProductDto>>(
               Method.Post,
               Constants.Rest.CartUpdateResource,
               updateProductDto,
               cancellationToken);
            return a;
        }

        public Task<HttpResponse<ResponseDto<CartDto>>> GetCartAsync(Guid basketId, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<CartDto>>(
                Method.Post,
                Constants.Rest.CartGetResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PromocodeDto>>> GetCartPromocodeAsync(Guid basketId, string? city, string promocode, CancellationToken cancellationToken)
        {
            var body = new
            {
                basketId,
                city,
                promocode
            };

            return httpService.ExecuteAsync<ResponseDto<PromocodeDto>>(
                Method.Post,
                Constants.Rest.CartPromocodeResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProductDto[]>>> GetCartPackagingAsync(Guid basketId, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<ProductDto[]>>(
                Method.Post,
                Constants.Rest.CartPackagingResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ToppingDto[]>>> GetSaucesAsync(Guid basketId, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<ToppingDto[]>>(
                Method.Post,
                Constants.Rest.CartSaucesResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<CartDto>>> ClearCartAsync(Guid basketId, string? city, CancellationToken cancellationToken)
        {
            var body = new
            {
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<CartDto>>(
                Method.Post,
                Constants.Rest.CartClearResource,
                body,
                cancellationToken);
        }
    }
}