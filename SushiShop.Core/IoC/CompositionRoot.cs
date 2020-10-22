using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Managers.Promotions;
using SushiShop.Core.Providers;
using SushiShop.Core.Services.Http;
using SushiShop.Core.Services.Http.Cities;
using SushiShop.Core.Services.Http.CommonInfo;
using SushiShop.Core.Services.Http.Menu;
using SushiShop.Core.Services.Http.Promotions;

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
            Container.RegisterSingleton<IMenuManager, MenuManager>();
            Container.RegisterSingleton<ICommonInfoManager, CommonInfoManager>();
            Container.RegisterSingleton<IPromotionsManager, PromotionsManager>();
        }

        protected override void RegisterServices()
        {
            Container.RegisterSingleton<IHttpService, HttpService>();
            Container.RegisterSingleton<IMenuService, MenuService>();
            Container.RegisterSingleton<ICitiesService, CitiesService>();
            Container.RegisterSingleton<ICommonInfoService, CommonInfoService>();
            Container.RegisterSingleton<IPromotionsService, PromotionsService>();
        }

        protected override void RegisterDependencies()
        {
            base.RegisterDependencies();
            Container.RegisterSingleton<IUserSession, UserSession>();
        }
    }
}