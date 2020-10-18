using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Menu
{
    public interface IMenuService
    {
        Task<HttpResponse<ResponseDto<MenuDto>>> GetMenuAsync(string? city, CancellationToken cancellationToken);
    }
}
