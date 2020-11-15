using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using System;
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

        public Task<HttpResponse<ResponseDto<ProductDto>>> GetProductAsync(
            int id,
            string? city,
            Guid? basketId,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                id,
                basketId,
                city
            };

            return httpService.ExecuteAsync<ResponseDto<ProductDto>>(
                Method.Post,
                Constants.Rest.ProductResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProductDto[]>>> GetProductsByCategoryAsync(
            int? categoryId,
            string? city,
            Guid? basketId,
            StickerType? stickerType,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                cid = categoryId,
                city,
                basketId,
                sticker = stickerType?.ToString().ToLower() ?? null
            };

            return httpService.ExecuteAsync<ResponseDto<ProductDto[]>>(
                Method.Post,
                Constants.Rest.CategoryProductsResource,
                body,
                cancellationToken);
        }

        public Task<HttpResponse<ResponseDto<ProductDto[]>>> GetRelatedProductsAsync(
            int id,
            string? city,
            Guid? basketId,
            CancellationToken cancellationToken)
        {
            var body = new
            {
                id,
                city,
                basketId,
            };

            return httpService.ExecuteAsync<ResponseDto<ProductDto[]>>(
                Method.Post,
                Constants.Rest.RelatedResource,
                body,
                cancellationToken);
        }
    }
}