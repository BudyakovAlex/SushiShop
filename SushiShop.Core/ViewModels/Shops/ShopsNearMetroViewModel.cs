using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Shops.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Shops
{
    public class ShopsNearMetroViewModel : BaseItemsPageViewModel<ShopItemViewModel>
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;
        private readonly IUserDialogs userDialogs;

        private Dictionary<string, MetroShop[]>? metroShopsMappings;

        public ShopsNearMetroViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;
            this.userDialogs = UserDialogs.Instance;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var city = userSession.GetCity();
            var response = await shopsManager.GetMetroShopsAsync(city?.Name);
            if (!response.IsSuccessful)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNotNullNorEmpty())
                {
                    await userDialogs.AlertAsync(error);
                }

                return;
            }

            metroShopsMappings = response.Data;
        }
    }
}