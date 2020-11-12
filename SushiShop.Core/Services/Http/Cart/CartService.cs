using SushiShop.Core.Common;
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

        public Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            return httpService.ExecuteAsync<ResponseDto<ProductDto>>(
               Method.Post,
               Constants.Rest.CartUpdateResource,
               updateProductDto,
               cancellationToken);
        }
    }
}