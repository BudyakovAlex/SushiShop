using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Products
{
    public interface IProductsManager
    {
        Task<Response<Product[]>> GetProductsByCategoryAsync(
            long? categoryId,
            string? city,
            StickerType? stickerType);

        Task<Response<Product?>> GetProductAsync(long id, string? city);

        Task<Response<Product[]>> GetRelatedProductsAsync(
            long id,
            string? city);
    }
}