using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Cart
{
    public interface ICartService
    {
        Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
    }
}