using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Shop
{
    public interface IShopService
    {
        Task<RawResponse<MenuDto[]>> GetMainMenuAsync(string? city, CancellationToken cancellationToken);
    }
}
