using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Mappers;
using SushiShop.Core.Services.Http.Products;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Managers.Products
{
    public class ProductsManager : IProductsManager
    {
        private readonly IProductsService productsService;

        public ProductsManager(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<Response<Product?>> GetProductAsync(int id, string? city)
        {
            var response = await productsService.GetProductAsync(id, city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Product?>(isSuccessful: true, data);
            }

            return new Response<Product?>(isSuccessful: false, null);
        }

        public async Task<Response<Product[]>> GetProductsByCategoryAsync(
            int categoryId,
            string? city,
            StickerType? stickerType)
        {
            var response = await productsService.GetProductsByCategoryAsync(categoryId, city, stickerType, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(product => product.Map()).ToArray();
                return new Response<Product[]>(isSuccessful: true, data);
            }

            return new Response<Product[]>(isSuccessful: false, Array.Empty<Product>());
        }
    }
}