using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Mappers;
using SushiShop.Core.Providers;
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
        private readonly IUserSession userSession;

        public ProductsManager(IProductsService productsService, IUserSession userSession)
        {
            this.productsService = productsService;
            this.userSession = userSession;
        }

        public async Task<Response<Product?>> GetProductAsync(int id, string? city)
        {
            var response = await productsService.GetProductAsync(id, city, userSession.GetCartId(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData?.Map();
                return new Response<Product?>(isSuccessful: true, data);
            }

            return new Response<Product?>(isSuccessful: false, null);
        }

        public async Task<Response<Product[]>> GetProductsByCategoryAsync(
            int? categoryId,
            string? city,
            StickerType? stickerType)
        {
            var response = await productsService.GetProductsByCategoryAsync(categoryId, city, userSession.GetCartId(), stickerType, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(product => product.Map()).ToArray();
                return new Response<Product[]>(isSuccessful: true, data);
            }

            return new Response<Product[]>(isSuccessful: false, Array.Empty<Product>());
        }

        public async Task<Response<Product[]>> GetRelatedProductsAsync(int id, string? city)
        {
            var response = await productsService.GetRelatedProductsAsync(id, city, userSession.GetCartId(), CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Select(product => product.Map()).ToArray();
                return new Response<Product[]>(isSuccessful: true, data);
            }

            return new Response<Product[]>(isSuccessful: false, Array.Empty<Product>());
        }
    }
}