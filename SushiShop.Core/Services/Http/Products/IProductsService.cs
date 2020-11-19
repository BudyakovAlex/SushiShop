using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Products
{
    public interface IProductsService
    {
        Task<HttpResponse<ResponseDto<ProductDto[]>>> GetProductsByCategoryAsync(
            long? categoryId,
            string? city,
            Guid? basketId,
            StickerType? stickerType,
            CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProductDto>>> GetProductAsync(
            long id,
            string? city,
            Guid? basketId,
            CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProductDto[]>>> GetRelatedProductsAsync(
            long id,
            string? city,
            Guid? basketId,
            CancellationToken cancellationToken);
    }
}