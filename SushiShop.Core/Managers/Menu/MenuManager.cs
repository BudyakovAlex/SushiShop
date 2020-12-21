using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Mappers.Menu;
using SushiShop.Core.Services.Http.Menu;

namespace SushiShop.Core.Managers.Menu
{
    public class MenuManager : IMenuManager
    {
        private readonly IMenuService menuService;

        public MenuManager(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public async Task<Response<Data.Models.Menu.Menu>> GetMenuAsync(string? city)
        {
            var response = await menuService.GetMenuAsync(city, CancellationToken.None);
            if (response.IsSuccessful)
            {
                var data = response.Data!.SuccessData!.Map();
                return new Response<Data.Models.Menu.Menu>(isSuccessful: true, data);
            }

            return new Response<Data.Models.Menu.Menu>(
                isSuccessful: false,
                new Data.Models.Menu.Menu(new Category[0], new Sticker[0]));
        }
    }
}
