using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Services.Http;
using SushiShop.Core.Services.Http.Cities;
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
            Container.RegisterSingleton<ICitiesManager, CitiesManager>();
        }

        protected override void RegisterServices()
        {
            Container.RegisterSingleton<IHttpService, HttpService>();
            Container.RegisterSingleton<IShopService, ShopService>();
            Container.RegisterSingleton<ICitiesService, CitiesService>();
        }
    }
}