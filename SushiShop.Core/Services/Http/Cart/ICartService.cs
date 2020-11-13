using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Cart;

namespace SushiShop.Core.Services.Http.Cart
{
    public interface ICartService
    {
        Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
        Task<HttpResponse<ResponseDto<CartDto>>> GetProductInCartAsync(GetProductDto getProductDto, CancellationToken cancellationToken);
        Task<HttpResponse<ResponseDto<PromoCodeDto>>> GetCartPromoCodeAsync(GetPromoCodeDto getPromoCodeDto, CancellationToken cancellationToken);
    }
}