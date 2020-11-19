using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Data.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Dtos.Toppings;
using System;

namespace SushiShop.Core.Services.Http.Cart
{
    public interface ICartService
    {
        Task<HttpResponse<ResponseDto<ProductDto>>> UpdateProductInCartAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CartDto>>> GetCartAsync(Guid basketId, string? сity, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<PromocodeDto>>> GetCartPromocodeAsync(Guid basketId, string? city, string promocode, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ProductDto[]>>> GetCartPackagingAsync(Guid basketId, string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ToppingDto[]>>> GetSaucesAsync(Guid basketId, string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<CartDto>>> ClearCartAsync(Guid basketId, string? city, CancellationToken cancellationToken);
    }
}