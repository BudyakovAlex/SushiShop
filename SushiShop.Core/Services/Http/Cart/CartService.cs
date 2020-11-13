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

        public Task<HttpResponse<ResponseDto<CartDto>>> GetProductInCartAsync(GetProductDto getProductDto,
            CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<CartDto>>(
                Method.Post,
                Constants.Rest.CartGetResource,
                getProductDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PromoCodeDto>>> GetCartPromoCodeAsync(GetPromoCodeDto getPromoCodeDto,
                CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PromoCodeDto>>(
                Method.Post,
                Constants.Rest.CartPromocodeResource,
                getPromoCodeDto,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<PackagingDto>>> GetCartPackagingAsync(string city,
            CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<PackagingDto>>(
                Method.Post,
                Constants.Rest.CartPackagingResource,
                city,
                cancellationToken);
        }
    }
}