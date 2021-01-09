using SushiShop.Core.Data.Dtos.Shops;
using SushiShop.Core.Data.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http.Shops
{
    public interface IShopsService
    {
        Task<HttpResponse<ResponseDto<ShopDto[]>>> GetShopsAsync(string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<Dictionary<string, MetroShopDto[]>>>> GetMetroShopsAsync(string? city, CancellationToken cancellationToken);

        Task<HttpResponse<ResponseDto<ShopDto>>> GetShopAsync(
            long id,
            string? city,
            CancellationToken cancellationToken);
    }
}
