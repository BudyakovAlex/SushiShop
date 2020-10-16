using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http.Shop
{
    public class ShopService : IShopService
    {
        private const string MainMenu = "menu";

        private readonly IHttpService httpService;

        public ShopService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        // NOT IMPLEMENTED YET
        public async Task<RawResponse<MenuDto[]>> GetMainMenuAsync(string? city, CancellationToken cancellationToken)
        {
            var response = await httpService.PostAsync(MainMenu, cancellationToken);
            throw new Exception();
        }
    }
}
