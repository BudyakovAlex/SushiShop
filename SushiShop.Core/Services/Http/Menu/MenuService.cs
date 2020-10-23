using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IHttpService httpService;

        public MenuService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<HttpResponse<ResponseDto<MenuDto>>> GetMenuAsync(string? city, CancellationToken cancellationToken)
        {
            var body = new { City = city };
            return httpService.ExecuteAsync<ResponseDto<MenuDto>>(
                Method.Post,
                Constants.Rest.MenuResource,
                body,
                cancellationToken);
        }
    }
}
