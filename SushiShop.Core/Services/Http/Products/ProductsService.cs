using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpService httpService;

        public ProductsService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<HttpResponse<ResponseDto<ProductDto[]>>> GetProductsByCategoryAsync(
            int categoryId,
            string? city,
            StickerType? stickerType,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                Cid = categoryId,
                City = city,
                Sticker = stickerType?.ToString().ToLower()
            };

            return await httpService.ExecuteAsync<ResponseDto<ProductDto[]>>(
                Method.Post,
                Constants.Rest.CategoryProductsResource,
                body,
                cancellationToken);
        }
    }
}