using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Products
{
    public interface IProductsManager
    {
        Task<Response<Product[]>> GetProductsByCategoryAsync(
            int? categoryId,
            string? city,
            StickerType? stickerType);

        Task<Response<Product?>> GetProductAsync(int id, string? city);

        Task<Response<Product[]>> GetRelatedProductsAsync(
            int id,
            string? city);
    }
}