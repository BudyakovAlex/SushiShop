using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Cart;

namespace SushiShop.Core.Services.Http.Cart
{
    public class CartService : ICartService
    {
        private readonly IHttpService httpService;

        public CartService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProductDto>>(
               Method.Post,
               Constants.Rest.CartUpdateResource,
               updateProductDto,
               cancellationToken);
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

        public Task<HttpResponse<ResponseDto<PromocodeDto>>> GetCartPromoCodeAsync(string city, string promocode, CancellationToken cancellationToken)
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
    }
}