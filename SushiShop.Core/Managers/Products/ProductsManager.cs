using SushiShop.Core.Services.Http.Products;

namespace SushiShop.Core.Managers.Products
{
    public class ProductsManager : IProductsManager
    {
        private readonly IProductsService productsService;

        public ProductsManager(IProductsService productsService)
        {
            this.productsService = productsService;
        }
    }
}
