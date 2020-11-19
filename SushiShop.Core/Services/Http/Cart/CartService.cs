using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
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

        public async Task<HttpResponse<ResponseDto<CartProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var a = await httpService.ExecuteAsync<ResponseDto<object>>(
               Method.Post,
               Constants.Rest.CartUpdateResource,
               updateProductDto,
               cancellationToken);
            return null;
        }

        public Task<HttpResponse<ResponseDto<CartDto>>> GetCartAsync(string city, CancellationToken cancellationToken)
        {
            var body = new
            {
                city
            };

            return httpService.ExecuteAsync<ResponseDto<CartDto>>(
                Method.Post,
                Constants.Rest.CartGetResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PromocodeDto>>> GetCartPromocodeAsync(string city, string promocode, CancellationToken cancellationToken)
        {
            var body = new
            {
                city,
                promocode
            };

            return httpService.ExecuteAsync<ResponseDto<PromocodeDto>>(
                Method.Post,
                Constants.Rest.CartPromocodeResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PackagingDto[]>>> GetCartPackagingAsync(string city, CancellationToken cancellationToken)
        {
            var body = new
            {
                city
            };

            return httpService.ExecuteAsync<ResponseDto<PackagingDto[]>>(
                Method.Post,
                Constants.Rest.CartPackagingResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<SaucesDto[]>>> GetSaucesAsync(string city, CancellationToken cancellationToken)
        {
            var body = new
            {
                city
            };

            return httpService.ExecuteAsync<ResponseDto<SaucesDto[]>>(
                Method.Post,
                Constants.Rest.CartSaucesResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<CartDto>>> ClearCartAsync(string city, CancellationToken cancellationToken)
        {
            var body = new
            {
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