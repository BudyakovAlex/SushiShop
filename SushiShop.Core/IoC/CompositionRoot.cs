using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.CommonInfo;
using SushiShop.Core.Managers.Menu;
using SushiShop.Core.Services.Http;
using SushiShop.Core.Services.Http.Cities;
using SushiShop.Core.Services.Http.CommonInfo;
using SushiShop.Core.Services.Http.Menu;

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
        }

        protected override void RegisterServices()
        {
            Container.RegisterSingleton<IHttpService, HttpService>();
            Container.RegisterSingleton<IMenuService, MenuService>();
            Container.RegisterSingleton<ICitiesService, CitiesService>();
            Container.RegisterSingleton<ICommonInfoService, CommonInfoService>();
        }
    }
}