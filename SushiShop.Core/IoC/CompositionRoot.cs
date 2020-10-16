using SushiShop.Core.Services.Http;
using SushiShop.Core.Services.Http.Shop;

namespace SushiShop.Core.IoC
{
    public class CompositionRoot : BuildApps.Core.Mobile.MvvmCross.IoC.CompositionRoot
    {
        public CompositionRoot()
        {
        }

        protected override void RegisterManagers()
        {
        }

        protected override void RegisterServices()
        {
            Container.RegisterSingleton<IHttpService, HttpService>();
            Container.RegisterSingleton<IShopService, ShopService>();
        }
    }
}