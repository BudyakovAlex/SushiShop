using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Products
{
    public interface IProductsService
    {
        Task<HttpResponse<ResponseDto<ProductDto[]>>> GetProductsByCategoryAsync(
            int categoryId,
            string? city,
            StickerType? stickerType,
            CancellationToken cancellationToken);
    }
}